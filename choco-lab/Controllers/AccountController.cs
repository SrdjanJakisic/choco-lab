using choco_lab.Data;
using choco_lab.Data.Models;
using choco_lab.Data.Static;
using choco_lab.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace choco_lab.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Users()
        {
            var users =await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login() => View(new LoginVM());
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Chocolates");
                    }
                }
                TempData["Error"] = "Подаци нису добри. Пробајте поново!";
                return View(loginVM);
            }

            TempData["Error"] = "Подаци нису добри. Пробајте поново!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);

            if (user != null)
            {
                TempData["Error"] = "Унета Е-пошта је већ искоришћена";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
                Address = registerVM.Address

            };
            var newUserRepsonse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserRepsonse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Chocolates");
        }

        //Get
        public async Task<IActionResult> Edit()
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM user = new UserEditVM(appUser);

            return View(user);
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditVM user)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                appUser.Email = user.Email;
                appUser.FullName = user.FullName;
                appUser.UserName = user.UserName;
                appUser.Address = user.Address;
                if (user.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, user.Password);
                }

                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                    TempData["Success"] = "Успешно сте променили податке!";    
            }
            return View("EditCompleted");
        }
    }
}
