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
        var movies = JsonSerializer.Deserialize<Movie[]>(MovieRepo.Get());
        return movies.ToArray();
    }
    
    [HttpGet("{id}")]
    public Movie Get(Guid id)
    {
        var movies = JsonSerializer.Deserialize<Movie[]>(MovieRepo.Get()).ToList();
        return movies.First(movie => movie.Id == id);
    }
    
    [HttpPut]
    public Movie Put([FromBody] Movie movie)
    {
        throw new NotImplementedException();
    }

    public Movie Rate(Guid id, int rating)
    {
        throw new NotImplementedException();
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

}