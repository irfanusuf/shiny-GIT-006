using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Data;
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
        public async Task<ActionResult> Index()
        {
            try
            {
                var posts = await dbContext.Posts.Include(posts => posts.Comments).Where(posts => posts.PostCaption != null).ToListAsync();

                var exploreViewModel = new ExploreViewModel
                {
                     Posts = posts
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
