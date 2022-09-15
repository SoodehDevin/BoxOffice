namespace CoreApi.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; } 
    public float Rating { get; set; } 
    public float Ratings { get; set; }

}