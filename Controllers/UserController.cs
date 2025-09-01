using Microsoft.AspNetCore.Mvc;

namespace P1WebMVC.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string Username , string Email , string Password)
        {
            
            // validation 

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "All the feilds are required!";
                return View();
            }


            if (Password.Length < 8)
            {
                ViewBag.ErrorMessage = "Password is less than 8 characters";
                return View();
            }

            // if existing user 
            // password encrypt
            // db save 
            // then 

            ViewBag.SuccessMessage = "Account Created Succesfully!";
            return RedirectToAction("Login");
        }





        public ActionResult Login()
        {
            return View();
        }


    }
}
