using System;

namespace P1WebMVC.Models.ViewModels;

public class ExploreViewModel
{

    public ICollection<Post> Posts { get; set; } = [];

    public ICollection<User> Users { get; set; } = [];


}
