using System;
using System.Diagnostics;
//using front_2.Models;
using Front_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using System.Linq;
using System.Drawing.Printing;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Security.Cryptography;
using front_5.Models;
using Microsoft.Extensions.Options;
using System.Net.NetworkInformation;

using X.PagedList;
namespace Front_5.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly StripeSettings _stripeSettings;

        private Appdbcontext appdbcontext;
		private object _appdbcontext;
        private object _context;

        public object DataAccessLayer { get; private set; }

        public HomeController(ILogger<HomeController> logger, Appdbcontext _appdbcontext)	
		{
			_logger = logger;
			appdbcontext = _appdbcontext;
       
        }





		// New Index method



		public IActionResult Index()
		{
            var mensCategory = appdbcontext.Category_two.FirstOrDefault(c => c.Name == "Mens");
            var mensSportPros = appdbcontext.Cards
                                             .Where(sp => sp.CategorytwoId == mensCategory.Id && sp.Ischeck)
                                             .ToList();
            ViewBag.MensSportPros = mensSportPros;

            var womensCategory = appdbcontext.Category_two.FirstOrDefault(c => c.Name == "Womens");
            var womensSportPros = appdbcontext.Cards
                                               .Where(sp => sp.CategorytwoId == womensCategory.Id && sp.Ischeck)
                                               .ToList();
            ViewBag.WomensSportPros = womensSportPros;

            var kidsCategory = appdbcontext.Category_two.FirstOrDefault(c => c.Name == "Kids");
            var kidsSportPros = appdbcontext.Cards
                                             .Where(sp => sp.CategorytwoId == kidsCategory.Id && sp.Ischeck)
                                             .ToList();
            ViewBag.KidsSportPros = kidsSportPros;

            ViewBag.Categories_two = appdbcontext.Category_two.ToList();

            var model = new Twomodel
            {
                Card = appdbcontext.Cards.Where(c => c.Ischeck).ToList(),
                Stat = appdbcontext.States.ToList(),
                Slider_L = appdbcontext.Sliders.ToList(),
                Card_t = appdbcontext.Cards.FirstOrDefault(c => c.Ischeck),
                Stat_t = appdbcontext.States.FirstOrDefault()
            };

            return View(model);
            // return View(appdbcontext.Cards.ToList());
        }

		public IActionResult Shop(int? i)
		{
            var pageNumber = i ?? 1;
            var pageSize = 9;

            var cards = appdbcontext.Cards.ToList();
            var categories = appdbcontext.Category.Where(y=> y.ischeck == true).ToList();
            // var categories = appdbcontext.Category.ToList();
          
            var categoryCounts = new Dictionary<int, int>();
            foreach (var category in categories)
            {
                var count = appdbcontext.Cards
                        .Where(c => c.CategoryId == category.Id && c.Ischeck)
                        .Count();
                // var count = appdbcontext.Cards.Count(c => c.CategoryId == category.Id);
                categoryCounts.Add(category.Id, count);
            }

            ViewBag.CategoryCounts = categoryCounts;
            ViewBag.Category_two = appdbcontext.Category_two.ToList();
            ViewBag.Categories = categories;
            ViewBag.Size = appdbcontext.Size;
            ViewBag.Color = appdbcontext.Colors;
            ViewBag.Slider = appdbcontext.Sliders.ToList();
            //var products = appdbcontext.Cards.Include(prd => prd.Category).Include(prd => prd.Category_two).ToList();
            var products = appdbcontext.Cards
         .Include(prd => prd.Category)
         .Include(prd => prd.Category_two)
         .Include(prd => prd.Size)
         .Where(prd => prd.Ischeck)
         .ToPagedList(pageNumber, pageSize);
            return View(products);
            //  return View(appdbcontext.Cards.ToList());
        }

        public IActionResult Select_category(int id)
        {
           
            var category = appdbcontext.Category.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound(); 
            }

            var products = appdbcontext.Cards.Include(prd => prd.Category)
                                              .Where(prd => prd.CategoryId == id)
                                              .Where(prd => prd.Ischeck)
                                              .ToList();
            if (products == null || !products.Any())
            {
                return RedirectToAction("Notresult");
            }
            else
            {
                return View(products);
            }
          
        }

        public IActionResult Select_color(int id)
        {

            var color = appdbcontext.Colors.FirstOrDefault(c => c.Id == id);
            if (color== null)
            {
                return NotFound("<h1>Nothing found</h1>");
            }

            var products = appdbcontext.Cards.Include(prd => prd.Color)
                                              .Where(prd => prd.ColorId == id)
                                              .Where(prd => prd.Ischeck)
                                              .ToList();

            if (products == null || !products.Any())
            {
                return RedirectToAction("Notresult");
            }
            else
            {
                return View(products);
            }
        }


        public IActionResult Filter2(int categoryId, int colorId, int categorytwoId,int sizeId)
        {
            //var products = appdbcontext.Cards
            //                    .Include(prd => prd.Category)
            //                    .Include(prd => prd.Color)
            //                    .Include(prd => prd.Category_two)
            //                    .Where(prd => prd.IsCheck == true
            //                                  && prd.CategoryId == categoryId
            //                                  && prd.ColorId == colorId
            //                                  && prd.CategorytwoId == categorytwoId)
            //                    .ToList();

            var products = appdbcontext.Cards
                                .Include(prd => prd.Category)
                                .Include(prd => prd.Color)
                                .Include(prd => prd.Size)
                                .Include(prd => prd.Category_two)
                                .Where(prd => prd.CategoryId == categoryId
                                              && prd.ColorId == colorId
                                              && prd.SizeId == sizeId
                                              && prd.CategorytwoId == categorytwoId)
                                .Where(prd => prd.Ischeck)
                                .ToList();
            if (products == null || !products.Any())
            {
                return RedirectToAction("Notresult");
            }
            else
            {
                return View(products);
            }
        }


        public IActionResult Features()
		{
           
            //         List<slider> sliderList = new List<slider>();
            //         slider image1 =new slider()
            //         {
            //             id = 1,
            //	imageur = "1.jpg"
            //         };

            //slider image2 = new slider()
            //{
            //	id = 2,
            //	imageur = "10.jpg"
            //};

            //slider image3 = new slider()
            //{
            //	id = 3,
            //	imageur = "10-1.jpg"
            //};
            //sliderList.Add(image1);
            //         sliderList.Add(image2);
            //         sliderList.Add(image3);
            
                return View(appdbcontext.Size.ToList());
            
        }

		public IActionResult Contact()
		{
			//List<slider> sliderList = new List<slider>();
			//slider image1 = new slider()
			//{
			//	text2 = "luybyo",
			//	picture = "1.jpg"
			//};
			return View();
		}

		public IActionResult Portfolio()
		{
			return View();
		}

        public IActionResult Blog(int? i )
		{
            var pageNumber = i ?? 1;
            var pageSize = 5;

            var cards = appdbcontext.Cards.ToList();
            var states = appdbcontext.States.ToList();
            var model = new Twomodel
            {
                Card = appdbcontext.Cards.ToList(),
                Stat = appdbcontext.States.ToList(),
                Card_t = appdbcontext.Cards.FirstOrDefault(),
                Stat_t = appdbcontext.States.FirstOrDefault(),
                PagedStates = states.ToPagedList(pageNumber, pageSize) 
            };
          


            return View(model);
        }

		public IActionResult Whislist()
		{
			return View();
		}

		//public IActionResult Login()
		//{
		//	return View();
		//}

		public IActionResult Acount()
		{
			return View();
		}

		public IActionResult Product_detail(int id)
		{

            //var product = appdbcontext.Cards
            //                 .Include(p => p.Category)
            //                  .Include(prd => prd.Color)
            //                 .FirstOrDefault(p => p.Id == id);
            var product = appdbcontext.Cards
                    .Include(p => p.Category)
                    .Include(p => p.Color)
                    .Include(p => p.Size)
                    .Include(p => p.Category_two)
                    .Include(p => p.Images) // Include the Images related to Sport_pro
                    .FirstOrDefault(p => p.Id == id);

            //var products = appdbcontext.Cards
            //               .Include(prd => prd.Category)
            //               .Include(prd => prd.Color)
            //               .ToList();
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Blog_detail(int id)
        {
            //var model = new Twomodel
            //{
            //    Card = appdbcontext.Cards.ToList(),
            //    Stat = appdbcontext.States.ToList(),
            //    Card_t = appdbcontext.Cards.FirstOrDefault(),
            //    Stat_t = appdbcontext.States.FirstOrDefault()
            //};

            var statya = appdbcontext.States.Find(id);

            if (statya == null)
            {
                return NotFound();
            }

            var viewModel = new BlogDetailViewModel
            {
                Stat= appdbcontext.States.ToList(),
                Statya = statya
            };

            return View(viewModel);
        }


		public IActionResult Get_help()
		{
			return View();
		}

        public IActionResult Search_r()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Successs()
        {
            return View();
        }

        public IActionResult Notresult()
        {
            return View();
        }
        public async Task<IActionResult> PostTableData(Orders model)
        {
            if (!ModelState.IsValid)
            {
                // Model state is not valid, return to the client indicating validation errors
                return BadRequest(ModelState);
            }

            // Ensure 'Name' property is not null or empty
            if (string.IsNullOrEmpty(model.Name))
            {
                // Handle the case where 'Name' is missing or empty
                return BadRequest("Name is required.");
            }

            try
            {
                // Add the model to the database context and save changes
                appdbcontext.Orders.Add(model);
                appdbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate
                Console.Error.WriteLine($"Error saving data: {ex.Message}");
                // Return an appropriate response to the client
                return StatusCode(500, "An error occurred while saving data.");
            }

            // Redirect to the "Shop" action upon successful saving
            return RedirectToAction("Successs");
            ViewBag.Script = "<script>window.localStorage.clear(); console.log('localStorage cleared');</script>";
        }
        //     public async Task <IActionResult> Search_r(string searchString)
        //     {
        //         var Cards = await appdbcontext.GetAllAsync();
        //if(!String.IsNullOrEmpty(searchString))
        //{
        //             Cards.Where(e => e.name.Contains(searchString) 
        //	|| e.price.Contains(searchString)).ToList();
        //         }
        //         return View(Cards);
        //     }

        public IActionResult Search(string query)
        {

            var results = appdbcontext.Cards.Where(s => s.name.Contains(query)).Where(s => s.Ischeck).ToList();
            
            if (results.Any())
            {
                return View("Search_r", results);
            }
            else
            {
                //return Content("Not found");
                return RedirectToAction("Notresult");
            }
        }

        [HttpGet]
        public IActionResult Filterprice(int min, int max)
        {
            ViewBag.Category= appdbcontext.Category.ToList();
            var filteredItems = appdbcontext.Cards
       .Where(s => s.price != null)
       .AsEnumerable() // Switch to LINQ to Objects
       .Where(s => int.TryParse(s.price, out int price) && price >= min && price <= max)
       .Where(s => s.Ischeck)
       .ToList();
            if (filteredItems == null || !filteredItems.Any())
            {
                return RedirectToAction("Notresult");
            }
            else
            {
                return View(filteredItems);
            }

        }

        //      public IActionResult Privacy()
        //{
        //	return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        //public async Task<IActionResult> Index(string searchString, string sortOrder, int pageNumber, string currentFilter)
        //{
        //	if (!string.IsNullOrEmpty(searchString))
        //	{
        //		employees = employees.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
        //	}
        //	return View((employees));
        //}
    }
}
