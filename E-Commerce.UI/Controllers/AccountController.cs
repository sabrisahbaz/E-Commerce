using E_Commerce.Data.Models.Identity;
using E_Commerce.Data.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AspNetUserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(AspNetUserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            if (User.Identity.Name != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            var user = new AppUser()
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.Username,
                Email = register.Email,

            };
            var result = await _userManager.CreateAsync(user, register.Password);

            //ROle oluştur veya varsa al
            var roleExists = await _roleManager.RoleExistsAsync("Admin");
            AppRole role;

            if (!roleExists)
            {
                //rolü oluştur
                role = new AppRole("Admin");
            }
            else
            {
                //Role Al
                role = await _roleManager.FindByNameAsync("Admin");
            }
            //kullanıcıya rolü ata

            await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
                return RedirectToAction("Login");
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(register);
        }

    }
}
