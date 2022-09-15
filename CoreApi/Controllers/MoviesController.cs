using System.Text.Json;
using CoreApi.Models;
using CoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using MovieRepository;

namespace CoreApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private static readonly IMovieRepo MovieRepo = new MovieRepo();
    
    [HttpGet]
    public Movie[] Get()
    {
        return JsonSerializer.Deserialize<Movie[]>(MovieRepo.Get());
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
        return movie;
    }

    [HttpPost]
    public Movie Rate(Guid id, int rating)
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
        var moviesService = new MovieService();
        return moviesService.Search(query);
    }

    [HttpGet("[action]/{query}")]
    public Movie[] SearchByGenre(string[] query)
    {
        var moviesService = new MovieService();
        return moviesService.SearchByGenre(query);
    }

    private static Movie Get_(Guid id)
    {
        var movies = JsonSerializer.Deserialize<Movie[]>(MovieRepo.Get()).ToList();
        var movie = movies.FirstOrDefault(movie => movie.Id == id);
        return movie;
    }

}