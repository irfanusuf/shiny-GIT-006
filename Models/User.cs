using System;
using System.ComponentModel.DataAnnotations;

namespace P1WebMVC.Models;

public class User
{

    [Key]
    public Guid UserId { get; set; } = Guid.NewGuid();
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    public string? ProfilePic { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
}
