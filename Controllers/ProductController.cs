using Microsoft.AspNetCore.Mvc;

namespace P1WebMVC.Controllers
{
    public class ProductController : Controller
    {
      
        public ActionResult CreateProduct()
        {
            return View();
        }

    }
}
