using CoreApi.Models;
using MovieRepository;
using Newtonsoft.Json;

namespace CoreApi.Services;

public class MovieService : IMovieService
{
    private static readonly IMovieRepo MovieRepo = new MovieRepo();
    
    public Movie Search(string query)
    {
        if (query == null)
            return null;

        //Kanske snabbare med json query och sånt istället för DeserializeObject på allt
        var movies = JsonConvert.DeserializeObject<Movie[]>(MovieRepo.Get()).ToList();
        var movie = movies.FirstOrDefault(q => q.Title?.ToLower().Contains(query.ToLower()) ?? false);
        return movie;
    }

    public Movie[] SearchByGenre(string[] query)
    {
        var movies = JsonConvert.DeserializeObject<Movie[]>(MovieRepo.Get());
        movies = movies.Where(m =>
            query.All(q => m.Genre?.Contains(q) ?? false)).ToArray();
        return movies;
    }
}