using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaASPNet.Data;
using PruebaASPNet.Models;

namespace PruebaASPNet.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Si ya tiene sesión, redirigir al perfil
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Perfil", "Home");
            }

            // Si la cuenta está bloqueada en sesión, redirigir
            if (HttpContext.Session.GetString("CuentaBloqueada") == "true")
            {
                return RedirectToAction("CuentaBloqueada");
            }

            return View(new LoginViewModel { TipoDocumento = "DNI" });
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == model.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                return View(model);
            }

            // Verificar si la cuenta ya está bloqueada
            if (user.IntentosFallidos >= 3 || user.Estado == "Bloqueado")
            {
                HttpContext.Session.SetString("CuentaBloqueada", "true");
                HttpContext.Session.SetString("BlockedUsername", user.Username);
                return RedirectToAction("CuentaBloqueada");
            }

            // Validar contraseña (texto plano para esta prueba)
            if (user.Password != model.Password)
            {
                // Incrementar intentos fallidos
                user.IntentosFallidos++;
                await _context.SaveChangesAsync();

                if (user.IntentosFallidos >= 3)
                {
                    user.Estado = "Bloqueado";
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("CuentaBloqueada", "true");
                    HttpContext.Session.SetString("BlockedUsername", user.Username);
                    return RedirectToAction("CuentaBloqueada");
                }

                int intentosRestantes = 3 - user.IntentosFallidos;
                ModelState.AddModelError("", $"Usuario o contraseña incorrectos. Le quedan {intentosRestantes} intento(s).");
                return View(model);
            }

            // Login exitoso - reiniciar intentos fallidos
            user.IntentosFallidos = 0;
            user.Estado = "Activo";
            await _context.SaveChangesAsync();

            // Guardar datos en sesión
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("NombreCompleto", user.NombreCompleto);
            HttpContext.Session.SetString("Rol", user.Rol);

            return RedirectToAction("Bienvenida", "Home");
        }

        // GET: /Account/CuentaBloqueada
        [HttpGet]
        public IActionResult CuentaBloqueada()
        {
            return View();
        }

        // POST: /Account/DesbloquearCuenta
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesbloquearCuenta()
        {
            var username = HttpContext.Session.GetString("BlockedUsername");
            if (!string.IsNullOrEmpty(username))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user != null)
                {
                    user.IntentosFallidos = 0;
                    user.Estado = "Activo";
                    await _context.SaveChangesAsync();
                }
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}