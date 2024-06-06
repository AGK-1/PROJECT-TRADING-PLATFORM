//using front_2.Models;
using Front_5.Controllers;
using Front_5.Models;
using Microsoft.AspNetCore.Mvc;
using Front_5.Extensions;
using System;
using NuGet.ContentModel;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using front_5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using X.PagedList;
using Microsoft.CodeAnalysis;
namespace Front_5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class DashController : Controller
    {
        private readonly Appdbcontext appdbcontext;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<DashController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private bool sert = false;
        // private readonly Appdbcontext _context;
        public DashController(Appdbcontext _appdbcontext , ILogger<DashController> logger, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            appdbcontext = _appdbcontext;
            _env = env;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(int? i)
        {
            var pageNumber = i ?? 1;
            var pageSize = 5;
            var cards = appdbcontext.Cards.ToList();
            appdbcontext.Cards.Include(sp => sp.Color).ToList();
            var model = new Twomodel
            {

                Card = appdbcontext.Cards.ToList(),
                Stat = appdbcontext.States.ToList(),
                Slider_L = appdbcontext.Sliders.ToList(),
                Slider_T = appdbcontext.Sliders.FirstOrDefault(),
                Card_t = appdbcontext.Cards.FirstOrDefault(),
                Stat_t = appdbcontext.States.FirstOrDefault(),
                PagedCards = cards.ToPagedList(pageNumber, pageSize)
            };

            return View(model);
           
        }

        public IActionResult Add()
        {
            ViewBag.imagem = appdbcontext.Images.ToList();
            ViewBag.Categories = appdbcontext.Category.ToList();
            ViewBag.Colors = appdbcontext.Colors.ToList();
            ViewBag.Categories_two = appdbcontext.Category_two.ToList();
            ViewBag.Size= appdbcontext.Size.ToList();

            return View();
		}

        public IActionResult Delete(int id)
        {
            var s = appdbcontext.Cards.Find(id);
            if (s != null)
            {
                appdbcontext.Cards.Remove(s);
                appdbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Add(Sport_pro card, IFormFile file1, IFormFile file2, List<IFormFile> Images)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Add");
            }
            // Validate required files
            if (file1 == null || file2 == null || Images == null || !Images.Any())
            {
                ModelState.AddModelError("Add", "Both photos and at least one additional image are required");
                return View("Index");
            }

            // Save the first photo
            string filename1 = await file1.SaveFileAsync(_env.WebRootPath, "assets/img/product");

            // Save the second photo
            string filename2 = await file2.SaveFileAsync(_env.WebRootPath, "assets/img/product");

            // Assign the filenames to the card properties
            card.photo1 = filename1;
            card.photo2 = filename2;

            // Initialize the Images collection if not already done
            card.Images = new List<Images>();

            // Save each additional image and create an Images entity for each
            foreach (var imageFile in Images)
            {
                string imageFilename = await imageFile.SaveFileAsync(_env.WebRootPath, "assets/img/product");
                card.Images.Add(new Images
                {
                    ImaUrl = $"{imageFilename}",
                    CardId = card.Id
                });
            }

            // Add the card to the context and save changes
            appdbcontext.Cards.Add(card);
            await appdbcontext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = appdbcontext.Category.ToList();
            ViewBag.Categories_two = appdbcontext.Category_two.ToList();
            ViewBag.Colors= appdbcontext.Colors.ToList();
            ViewBag.Size = appdbcontext.Size.ToList();
            ViewBag.Images = appdbcontext.Images.ToList();
            if (id == 0)
            {
                return NotFound();
            }
            var model = appdbcontext.Cards.FirstOrDefault(s => s.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Sport_pro card,IFormFile file1, IFormFile file2, List<IFormFile> Images)
        {
            //    if (!ModelState.IsValid)
            //    {
            //        return View(card);
            //    }
            if (file1 == null || file2 == null || Images == null || !Images.Any())
            {
                ModelState.AddModelError("Add", "Both photos and at least one additional image are required");
                return View("Index");
            }

            // Save the first photo
            string filename1 = await file1.SaveFileAsync(_env.WebRootPath, "assets/img/product");

            // Save the second photo
            string filename2 = await file2.SaveFileAsync(_env.WebRootPath, "assets/img/product");

            // Assign the filenames to the card properties
            card.photo1 = filename1;
            card.photo2 = filename2;
            card.Images = new List<Images>();
            foreach (var imageFile in Images)
            {
                string imageFilename = await imageFile.SaveFileAsync(_env.WebRootPath, "assets/img/product");
                card.Images.Add(new Images
                {
                    ImaUrl = $"{imageFilename}",
                    CardId = card.Id
                });
            }
            appdbcontext.Cards.Update(card);
            appdbcontext.SaveChanges();
            return RedirectToAction("Index");
        }


		[HttpPost]
		public async Task<IActionResult> Addtocategory(Category category)
		{

			//if (!ModelState.IsValid)
			//{
			//	return RedirectToAction("Add");
			//}
			appdbcontext.Category.Add(category);
			appdbcontext.SaveChanges();
			return RedirectToAction("Categor");
		}

        public IActionResult Ablog()
        {
            var model = new Twomodel
            {

                Card = appdbcontext.Cards.ToList(),
                Stat = appdbcontext.States.ToList(),
                Card_t = appdbcontext.Cards.FirstOrDefault(),
                Stat_t = appdbcontext.States.FirstOrDefault()
            };
            return View(model);
        }

        public IActionResult delete_blog(int id)
        {
            var s = appdbcontext.States.Find(id);
            if (s != null)
            {
                appdbcontext.States.Remove(s);
                appdbcontext.SaveChanges();
            }
            return RedirectToAction("Ablog");
        }

        public async Task<IActionResult>addtoblog(state card, IFormFile file3)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            if (file3 == null)
            {
                ModelState.AddModelError("Add", "Both photos are required");
                return View("Index");
            }
            string filename1 = await file3.SaveFileAsync(_env.WebRootPath, "assets/img/blog");

            // Assign the filenames to the card properties
            card.sekil = filename1;

            appdbcontext.States.Add(card);
            appdbcontext.SaveChanges();
            return RedirectToAction("Add");
        
        }


        public IActionResult Categor(int id)
        {       
			ViewBag.Categories = appdbcontext.Category.ToList();
            return View();     
		}

        [HttpGet]
        public IActionResult Edit_category(int id)
        {
            ViewBag.Categories = appdbcontext.Category.ToList();
            if (id == 0)
            {
                return NotFound();
            }
            var model = appdbcontext.Category.FirstOrDefault(s => s.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
   
        public IActionResult Edit_cat(int id)
        {  
            return Json(id);
        }

        public IActionResult Edit_category5(int id,string name, bool ischeck)
        {         
            var category = appdbcontext.Category.Find(id);
            if (category == null)
            {
                return NotFound();
            }        
            category.Name = name;
            category.ischeck =ischeck;
            appdbcontext.Category.Update(category);
            appdbcontext.SaveChanges();
            return RedirectToAction("Categor", "dash", new { area = "admin" });
        }


        [HttpPost]
        public IActionResult UpdateCategory([FromBody] UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = appdbcontext.Category.Find(request.Id);
            if (category == null)
            {
                return NotFound();
            }

            category.ischeck = request.IsCheck;
            appdbcontext.Update(category);
            appdbcontext.SaveChanges();

            return Ok();
        }

        public IActionResult Order_list()
        {
           
            return View(appdbcontext.Orders.ToList());
        }
        public async Task<IActionResult> Users_list()
        {

            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault(); // Assuming a user has only one role
            }

            ViewBag.UserRoles = userRoles;
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteUserByAge(int age)
        {
            var user = await appdbcontext.Users.FirstOrDefaultAsync(u => u.Age == age);
            if (user != null)
            {
                appdbcontext.Users.Remove(user);
                await appdbcontext.SaveChangesAsync();
            }
            return RedirectToAction("Users_list", "dash");
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            // Check if user is already in Admin role
            var isInAdminRole = await _userManager.IsInRoleAsync(user, "Admin");
            if (isInAdminRole)
            {
                return BadRequest($"User with ID '{userId}' is already an Admin.");
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                // Handle error if user couldn't be added to Admin role
                return BadRequest($"Failed to promote user with ID '{userId}' to Admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return RedirectToAction("Users_list", "dash");
        }
       
        public IActionResult UpdateIscheckToFalse(int id)
        {
            var sportProInstance = appdbcontext.Cards.Find(id);
            if (sportProInstance == null)
            {
                return NotFound(); // Handle the case where the instance is not found
            }

            // Update the Ischeck property to true
            sportProInstance.Ischeck = false;

            // Save changes to the database
            appdbcontext.SaveChanges();

            // Redirect to the Index action of the dash controller
            return RedirectToAction("Index", "dash");
        }

        public IActionResult UpdateIscheckTotrue(int id)
        {
            // Retrieve the Sport_pro instance from the database using the id
            var sportProInstance = appdbcontext.Cards.Find(id);
            if (sportProInstance == null)
            {
                return NotFound(); // Handle the case where the instance is not found
            }

            // Update the Ischeck property to true
            sportProInstance.Ischeck = true;

            // Save changes to the database
            appdbcontext.SaveChanges();

            // Redirect to the Index action of the dash controller
            return RedirectToAction("Index", "dash");
        }
    }
    // GET: Cards/Edit/5

   
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public bool IsCheck { get; set; }
    }



}

