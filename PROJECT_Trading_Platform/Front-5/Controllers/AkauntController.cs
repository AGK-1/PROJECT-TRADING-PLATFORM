//using front_2.Models;
using Front_5.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Front_5.Models.Extensions;
using Front_5.Extensions;
using Front_5.Services;
using Microsoft.AspNetCore.Authorization;
using front_5.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using static System.Net.WebRequestMethods;
using Stripe;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System;
using Stripe.FinancialConnections;
using System.Reflection.Metadata;

//using front_5.Models;


namespace Front_5.Controllers
{
    public class AkauntController : Controller
    {
        private readonly Appdbcontext appdbcontext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailservice;
        public AkauntController(Appdbcontext _appdbcontext,UserManager<User> userManager, SignInManager<User> signInManager,IEmailService emailservice, RoleManager<IdentityRole> roleManager)
        {
            appdbcontext = _appdbcontext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailservice = emailservice;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
      
            User Users = new User
            {
                Name = model.Name,  
                Surname = model.Surname,
                Age = model.Age,
                Email = model.Email,
                UserName = model.Name
            };
            var result = await _userManager.CreateAsync(Users, model.Password);
            if (result.Succeeded)
            {

              var token = await _userManager.GenerateEmailConfirmationTokenAsync(Users);
               var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = Users.Id, token = token }, Request.Scheme);
                // await _emailservice.SendEmailAsync(model.Email,"Confirm please", subject);

                var subject = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Confirmation</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">
    <div style=""max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);"">
        <h2 style=""color: #333333;"">Confirm Your Email Address</h2>
        <p style=""color: #666666;"">Hello,</p>
        <p style=""color: #666666;"">Thank you for signing up. Please confirm your email address by clicking the button below:</p>
        <a href=""{confirmationLink}"" style=""display: inline-block; background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; margin-top: 20px;"">Confirm Email</a>
        <p style=""color: #666666; margin-top: 20px;"">If you did not create an account, no further action is required.</p>
        <p style=""color: #666666;"">Regards,<br>Your Name</p>
    </div>
</body>
</html>";

                await _emailservice.SendEmailAsync(model.Email, "Confirm your email", subject);
                //await _signInManager.SignInAsync(Users, true);
                await _userManager.AddToRoleAsync(Users,"User");
                return RedirectToAction("Index", "Home");

            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "Something incorrent");
            //}
            var user = await _userManager.FindByEmailAsync(model.Email);
           
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Isremember, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Password or email incorrent");
                }
            }
            _signInManager.SignInAsync(user, isPersistent: true);
            //_emailService.SendEmailAsync()
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(roleName: "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "Admin"));
            }
            if (!await _roleManager.RoleExistsAsync(roleName: "User"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "User"));
            }
        }
        public async Task SeedAdmin()
        {
            if (await _userManager.FindByEmailAsync("kermiv@gmail.com") == null)
            {
                //User admin = new User
                //{
                //    Email = "kermiv@gmail.com",
                //    UserName = "kermiv@gmail.com"
                //};
                User admin = new User
                {
                    Name = "Admin",
                    Surname = "Admin",
                    Age = 100,
                    Email = "kermiv@gmail.com",
                    UserName = "kermiv@gmail.com"
                };

                IdentityResult result = await _userManager.CreateAsync(admin, "123456AGK");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                    await _signInManager.SignInAsync(admin, isPersistent: true);

                   RedirectToAction("Index", "Home");
                }
               
            }
           
        }

   
    }
}
