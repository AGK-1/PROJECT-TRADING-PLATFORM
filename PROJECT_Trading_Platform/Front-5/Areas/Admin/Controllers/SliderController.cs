//using front_2.Models;
using Front_5.Controllers;
using Front_5.Models;
using Microsoft.AspNetCore.Mvc;
using Front_5.Extensions;
using front_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace Front_5.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly Appdbcontext appdbcontext;
        private readonly IWebHostEnvironment _env;
        

        public SliderController(Appdbcontext _appdbcontext, IWebHostEnvironment env)
        {
            appdbcontext = _appdbcontext;
            _env = env; 
        }

        public IActionResult Index()
        {
            var model = new Twomodel
            {

                Card = appdbcontext.Cards.ToList(),
                Stat = appdbcontext.States.ToList(),
                Slider_L = appdbcontext.Sliders.ToList(),
                Slider_T = appdbcontext.Sliders.FirstOrDefault(),
                Card_t = appdbcontext.Cards.FirstOrDefault(),
                Stat_t = appdbcontext.States.FirstOrDefault()
            };

            return View(model);
        }


        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Add(Slider slider, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(slider); // Return the view with the current slider object to display validation messages
            }

            if (!file.IsImage())
            {
                ModelState.AddModelError("Add", "At least one of the uploaded files is not an image");
                return View("Index"); // Return to the Index view if the file is not an image
            }

            // Save the image file to the specified path
            string filename1 = await file.SaveFileAsync(_env.WebRootPath, "assets/img/sliders_swipe/");
            slider.picture = filename1;

            appdbcontext.Sliders.Add(slider);
            appdbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var s = appdbcontext.Sliders.Find(id);
            if (s != null)
            {
                appdbcontext.Sliders.Remove(s);
                appdbcontext.SaveChanges();
            }
            return RedirectToAction("Index", "dash");
        }
        public IActionResult Delete2(int id)
        {
            var s = appdbcontext.Sliders.Find(id);
            if (s != null)
            {
                appdbcontext.Sliders.Remove(s);
                appdbcontext.SaveChanges();
            }
            return RedirectToAction("Index", "Slider");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            if (id == 0)
            {
                return NotFound();
            }
            var model = appdbcontext.Sliders.FirstOrDefault(s => s.Id == id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Slider slider, IFormFile file)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return View(slider); // Return the view with the current slider object to display validation messages
            }

            // Check if the uploaded file is an image
            if (!file.IsImage())
            {
                ModelState.AddModelError("Add", "At least one of the uploaded files is not an image");
                return View("Index"); // Return to the Index view if the file is not an image
            }

            // Save the image file to the specified path
            string filename1 = await file.SaveFileAsync(_env.WebRootPath, "assets/img/sliders_swipe/");
            slider.picture = filename1; // Update the slider object with the new image path

            // Update the slider object in the database
            appdbcontext.Sliders.Update(slider);
            await appdbcontext.SaveChangesAsync(); // Save changes asynchronously to the database

            // Redirect to the Index action
            return RedirectToAction("Index");
        }
       
    }
}
