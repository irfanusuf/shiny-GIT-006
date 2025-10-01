using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Data;
using P1WebMVC.Interfaces;
using P1WebMVC.Middlewares;
using P1WebMVC.Models;
using P1WebMVC.Models.ViewModels;

namespace P1WebMVC.Controllers
{
    public class ExploreController : Controller
    {
        // GET: ExploreController

        private readonly SqlDbContext dbContext;


        public ExploreController(SqlDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        // attribute flag 

        [Authorize]
        public async Task<ActionResult> Index()
        {
            try
            {

                var posts = await dbContext.Posts
                .Include(posts => posts.Comments)
                .Include(posts => posts.Likes)
                .Where(posts => posts.PostCaption != null)
                .ToListAsync();

                var exploreViewModel = new ExploreViewModel
                {
                    Posts = posts,
                    LoggedInUser = HttpContext.Items["user"] as User
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
