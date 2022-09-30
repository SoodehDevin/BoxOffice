using System.Text.Json;
using CoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using MovieRepository;
using Newtonsoft.Json.Linq;

namespace CoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private static List<Movie> Movies;

    private static readonly IMovieRepo MovieRepo;

    static MoviesController()
    {
        MovieRepo = new MovieRepo(); 
        Movies = LoadAllRepoMovies_();
    }

    [HttpGet]
    public Movie[] Get()
    {
        return Movies.ToArray();
    }

    [HttpGet("{id}")]
    public Movie Get(Guid id)
    {
        return Get_(id);
    }

    [HttpPut]
    public Movie Put([FromBody] Movie movie)
    {
        var item = JsonSerializer.Serialize<Movie>(movie);
        MovieRepo.Put(item);
        Movies.Add(movie);

        return movie;
    }

    [HttpPost]
    public Movie Rate(Guid id, float rating)
    {
        if (id == Guid.Empty)
        { return null; }
        else if (rating < 0 || rating > 10)
        { return null; }
        else
        {
            var item = Get_(id);
            if (item == null)
                return null;
            item.Rating = rating;
            return item;
        }
    }

    [HttpGet("[action]/{query}")]
    public Movie Search(string query)
    {
        if (query == null)
            return null;

        var movie = Movies.FirstOrDefault(q => q.Title?.ToLower().Contains(query.ToLower()) ?? false);
        return movie;
 
    }

    [HttpGet("[action]/{query}")]
    public Movie[] SearchByGenre(string[] query)
    {
        var movies = Movies.Where(m =>
            query.All(q => m.Genre?.Contains(q) ?? false)).ToArray();
        return movies;
    }

    private static List<Movie> LoadAllRepoMovies_()
    {
        return JsonSerializer.Deserialize<List<Movie>>(MovieRepo.Get());
    }

    private static Movie Get_(Guid id)
    {
        var movie = Movies.FirstOrDefault(movie => movie.Id == id);
        return movie;
    }
}