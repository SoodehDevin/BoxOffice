using CoreApi.Models;
using MovieRepository;
using Newtonsoft.Json;

namespace CoreApi.Services;

public class MovieService : IMovieService
{
    private static readonly IMovieRepo MovieRepo = new MovieRepo();
    
    public Movie Search(string query)
    {
        var movies = JsonConvert.DeserializeObject<Movie[]>(MovieRepo.Get()).ToList();
        var movie = movies.First(q => q.Title.Contains(query));
        return movie;
    }

    public Movie[] SearchByGenre(string[] query)
    {
        var movies = JsonConvert.DeserializeObject<Movie[]>(MovieRepo.Get());
        movies = movies.Where(q => q.Genre.Contains(query[0])).ToArray();
        return movies;
    }
}