using choco_lab.Data;
using choco_lab.Data.Models;
using choco_lab.Data.Static;
using choco_lab.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
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
        [HttpGet]
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
        [HttpGet]
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
                City = registerVM.City,
                Address = registerVM.Address,
                PhoneNumber = registerVM.PhoneNumber

            };
            var newUserRepsonse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserRepsonse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");

        }

        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Chocolates");
        }

        [HttpGet]
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
                appUser.City = user.City;
                appUser.Address = user.Address;
                appUser.PhoneNumber = user.PhoneNumber;
                //if (user.Password != null)
                //{
                //    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, user.Password);
                //}

                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {             
                    TempData["Success"] = "Успешно сте променили податке!";
                    //return RedirectToAction("Logout", "Account");
                } 
            }
            return View("EditCompleted");
        }

        [HttpGet]
        public IActionResult EditPass()
        {   
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> EditPass(PasswordEditVM user)
        {
            ApplicationUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {

                if (user.Password != null)
                {
                    appUser.PasswordHash = _passwordHasher.HashPassword(appUser, user.Password);
                }

                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    TempData["Success"] = "Успешно сте променили шифру!";
                    //return RedirectToAction("Logout", "Account");
                    return View("EditPassCompleted");
                }
            }
            return View(user);        
        }

        [HttpGet]
        public async Task<IActionResult> userDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }


        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} cannot be found";
                return View("NotFound");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Users");
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} cannot be found";
                return View("NotFound");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Logout", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return RedirectToAction("Logout", "Account");

        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"Корисник са Id = {id} није пронађен";
                return View("NotFound");
            }

            var model = new UserEditVM
            {
                Email = user.Email,
                UserName = user.UserName,
                FullName = user.FullName,
                City = user.City,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditVM user)
        {
            ApplicationUser appUser = await _userManager.FindByIdAsync(user.Id);

            if (ModelState.IsValid)
            {
                appUser.Email = user.Email;
                appUser.FullName = user.FullName;
                appUser.UserName = user.UserName;
                appUser.City = user.City;
                appUser.Address = user.Address;
                appUser.PhoneNumber = user.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users"); 
                    //return RedirectToAction("Logout", "Account");
                }
            }
            return View(user);
        }
    }
}
