// container 

using Microsoft.EntityFrameworkCore;
using P1WebMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// adding services to the conatiner  

builder.Services.AddControllersWithViews();


// Di // Automated Di 
builder.Services.AddDbContext<SqlDbContext>((options) => options.UseSqlServer(builder.Configuration.GetConnectionString("cloud")));





var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }



// middlewares 


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
