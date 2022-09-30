using CoreApi.Controllers;
using CoreApi.Models;

namespace CoreAPITests
{
    [TestFixture, Description("Tests the search functions of the BoxOffice CoreAPI")]
    public class GenreSearchTester
    {
        private static MoviesController _movieController;


        [OneTimeSetUp]
        public void Setup()
        {
            _movieController = new MoviesController();
        }

        [Test, Description("Should find a list of movies with the genre 'Drama'"), MaxTime(500)]
        public void SearchByGenre_WhenMoviesWithGenreDramaExists_ReturnsAllMoviesForTheDrama()
        { 
            //Arrange
            var genre = new string[] { "Drama" };

            _movieController.Put(new Movie());

            //Act
            var movies = _movieController.SearchByGenre(genre);

            //Assert
            Assert.Greater(movies.Length, 0, $"Unable to find any movies with the {genre} genre");
        }

        [Test, Description("Should find at least 20 movies with the Action and Crime genres."), MaxTime(500)]
        public void SearchForActionAndCrime()
        {
            var genres = new string[] { "Crime", "Action" };

            var movies = _movieController.SearchByGenre(genres);
            Assert.Greater(movies.Length, 20, $"Unable to find at least 20 movies with the {genres[0]} and {genres[1]} genres");
            foreach (var movie in movies)
            {
                StringAssert.Contains(genres[0], movie.Genre, $"At least one of the movies found does not have the {genres[0]} genre");
                StringAssert.Contains(genres[1], movie.Genre, $"At least one of the movies found does not have the {genres[1]} genre");
            }
        }

    }
}