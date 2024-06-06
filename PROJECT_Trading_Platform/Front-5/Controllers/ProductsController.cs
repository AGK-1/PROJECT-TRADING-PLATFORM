using front_5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Front_5.Controllers
{
	
	public class ProductsController : Controller
    {

        private readonly Appdbcontext appdbcontext;

        public ProductsController(Appdbcontext _appdbcontext)
        {
            appdbcontext = _appdbcontext;
        }
        public IActionResult Products7()
        {
            var prd = appdbcontext.Products.Include(prd=>prd.Category).ToList();
            return View(prd);
        }
    }
}
