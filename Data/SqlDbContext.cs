using System;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Models;


namespace P1WebMVC.Data;

public class SqlDbContext : DbContext
{


    public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) { }

    // entities
    
     public   DbSet<User> Users { get; set; }


}
