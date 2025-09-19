using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P1WebMVC.Data;
using P1WebMVC.Interfaces;
using P1WebMVC.Models;

namespace P1WebMVC.Controllers
{
    public class PostController : Controller
    {

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ICloudinaryService cloudinaryService;
        public PostController(SqlDbContext dbContext, ITokenService tokenService, ICloudinaryService cloudinaryService)
        {

            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.cloudinaryService = cloudinaryService;

        }



        [HttpPost]
        public async Task<ActionResult> CreatePost(Post post, IFormFile image, Guid userId)
        {

            try
            {

                if (string.IsNullOrEmpty(post.PostCaption))
                {

                    TempData["ErrorMessage"] = "caption is required !";
                    return RedirectToAction("Dashboard", "User");
                }

                if (image == null || image.Length == 0)
                {
                    TempData["ErrorMessage"] = "Image is required !";
                    return RedirectToAction("Dashboard", "User");

                }

                var SecureUrl = await cloudinaryService.UploadImageAsync(image);


                post.PostpicURL = SecureUrl;
                post.UserId = userId;    // fk 


                var newPost = await dbContext.Posts.AddAsync(post);
                await dbContext.SaveChangesAsync();


                TempData["SuccessMessage"] = "Post uploaded Succesfully !";

                return RedirectToAction("Dashboard", "User");

            }
            catch (System.Exception)
            {

                throw;
            }

        }


        [HttpPost]

        public async Task<ActionResult> AddComment(Guid postId , Comment comment)
        {


            comment.PostId = postId;

            await dbContext.Comments.AddAsync(comment);

            await dbContext.SaveChangesAsync();

            TempData["SuccessMessage"] = "Comment Added";

            return RedirectToAction("Index", "Explore");
        }


    }
}
