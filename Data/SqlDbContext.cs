using System;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Models;


namespace P1WebMVC.Data;

public class SqlDbContext : DbContext
{

    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

    // entities

    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }


    // fluent api so that we will have fine control on relationships  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
        .HasOne(p => p.User)    // post ka ek user hai jis se woh belong kerta hai
        .WithMany(u => u.Posts)  // user kay bahiut sarey posts hai 
        .HasForeignKey(p => p.UserId)   // post ke ander userid forign key hai 
        .OnDelete(DeleteBehavior.Cascade);   // user delete kerdiya /toh sarein ppost delete hojayegye 

    }

    


}
