using Microsoft.AspNetCore.Mvc;

namespace P1WebMVC.Controllers
{
    public class User : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

    }
}
