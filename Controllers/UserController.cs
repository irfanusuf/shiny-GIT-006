using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using P1WebMVC.Data;
using P1WebMVC.Interfaces;
using P1WebMVC.Models;

namespace P1WebMVC.Controllers
{
    public class UserController : Controller
    {

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public UserController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }




        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {

            // validation 
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.ErrorMessage = "All the feilds are required!";
                return View();
            }

            if (user.Password.Length < 8)
            {
                ViewBag.ErrorMessage = "Password is less than 8 characters";
                return View();
            }


            // if existing user     LINQ query 
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);



            if (existingUser != null)
            {
                ViewBag.ErrorMessage = "User with this email already exists";
                return View();
            }

            // password encrypt
            var passEncrypt = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passEncrypt;


            var newUser = await dbContext.Users.AddAsync(user);

            // db save 
            await dbContext.SaveChangesAsync();


            if (newUser != null)
            {
                ViewBag.SuccessMessage = "Account Created Succesfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "Something Went wrong Try again ";
            }
            return View();
        }





        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Login(string Email, string Password)
        {



                // validation 
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.ErrorMessage = "Email and password both required";
                return View();
            }

                // check user

            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email);   // single user return 




            if (existingUser == null)
            {
                ViewBag.ErrorMessage = "No User Found with this Email";
                return View();
            }


                    // pass verify
            var passVerify = BCrypt.Net.BCrypt.Verify(Password , existingUser.Password);

            if (passVerify)
            {
                // session mangement // jwt token then send that to cookies 

               var token =  tokenService.CreateToken(existingUser.UserId , Email , existingUser.Username , 7);


                // send the token in cookies // tommorow


                ViewBag.SuccessMessage = "User Login Succesfull !";   
                return View(); 
            }
            else
            {
                ViewBag.ErrorMessage = "Incorrect Password !";   
                return View(); 
            }
  
        }
    }
}

