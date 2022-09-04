using CoreApi.Models;

namespace CoreApi.Services;

public interface IMovieService
{
    public Movie Search(string query);
}