using front_5.Models;
using Front_5.Models;
using Front_5.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Front_5.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Appdbcontext appdbcontext;
        private readonly IEmailService _emailservice;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(Appdbcontext _appdbcontext, UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailservice, RoleManager<IdentityRole> roleManager)
        {
            appdbcontext = _appdbcontext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailservice = emailservice;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail"); // Ensure this matches the view name
            }
            else
            {
                return View("Error"); // You may want to create an Error view as well
            }
        }

    }
}
