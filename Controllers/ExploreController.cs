using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Data;
using P1WebMVC.Interfaces;
using P1WebMVC.Models;
using P1WebMVC.Models.ViewModels;

namespace P1WebMVC.Controllers
{
    public class ExploreController : Controller
    {
        // GET: ExploreController

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;

        public ExploreController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }
        public async Task<ActionResult> Index()
        {
            try
            {

                var token = HttpContext.Request.Cookies["authToken"];

                if (string.IsNullOrEmpty(token))
                {
                    TempData["ErrorMessage"] = "Forbidden to access the page";
                    return RedirectToAction("Login", "User");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);

                if (userId == Guid.Empty)
                {
                    TempData["ErrorMessage"] = "Unauthorized to access the page";
                    return RedirectToAction("Login", "User");
                }



                var user = await dbContext.Users.FindAsync(userId);


                var posts = await dbContext.Posts
                .Include(posts => posts.Comments)
                .Include(posts => posts.Likes)
                .Where(posts => posts.PostCaption != null)
                .ToListAsync();

                var exploreViewModel = new ExploreViewModel
                {
                    Posts = posts,
                    LoggedInUser = user
                };


                
                // DTO
                return View(exploreViewModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
