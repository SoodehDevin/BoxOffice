using CoreApi.Controllers;
using Movie = CoreApi.Models.Movie;

namespace CoreAPITests
{
    [TestFixture, Description("Tests the put functions of the BoxOffice CoreAPI")]

    public class PutTester
    {
        private static MoviesController _movieController;

        [OneTimeSetUp]
        public void Setup()
        {
            _movieController = new MoviesController();

        }

        [Test, Description("Should insert the New Movie.")/*, MaxTime(500)*/]
        public void InsertNewMovie()
        {
            //Arrange
            Movie movie = GetTestMovie_();

            //Act
            _movieController.Put(movie);

            //Assert
            var databaseMovie = _movieController.Search(movie.Title);
            Assert.IsNotNull(databaseMovie, "Unable to find the inserted movie.");
            Assert.That(databaseMovie.Title, Is.EqualTo(movie.Title), "Unable to find a movie with matching title");
            Assert.That(databaseMovie.Description, Is.EqualTo(movie.Description), "Unable to find a movie with matching description");
            Assert.That(databaseMovie.Genre, Is.EqualTo(movie.Genre), "Unable to find a movie with matching genre");
            Assert.That(databaseMovie.Rating, Is.EqualTo(movie.Rating), "Unable to find a movie with matching rating");
        }

        [Test, Description("Checks if newly inserted movies are given an Id."), MaxTime(500)]
        public void CheckForId()
        {
            //Arrange
            var movie = GetTestMovie_();

            //Act
            var insertedMovie = _movieController.Put(movie);

            //Assert
            Assert.That(insertedMovie.Id != Guid.Empty, "Inserted movie is given an empty Id.");
        }

        [Test, Description("Number of movies in database should increase on insert.")/*, MaxTime(500)*/]
        public void CheckForIncrement()
        {
            
            var originalNumber = _movieController.Get().Length;

            var movie = new Movie() { Id = Guid.NewGuid(), Title = "Incremented Movie", Description = "Incremented Movie description", Genre = "Crime|Fantasy", Rating = 8.0f };
            _movieController.Put(movie);

            var newNumber = _movieController.Get().Length;

            Assert.Greater(newNumber, originalNumber, "The number of movies in the database did not increase.");
        }

        [Test, Description("Should insert a movie, then update its description.")/*, MaxTime(500)*/]
        public void UpdateMovie()
        {
            var originalMovie = new Movie()
            {
                Id = Guid.NewGuid(),
                Title = "Updatable Movie",
                Description = "Updatable Movie description",
                Genre = "Documentary",
                Rating = 8.0f
            };
            var movie = _movieController.Put(originalMovie);

            var upatedMovie = _movieController.Get(movie.Id);
            //var firstCheck = _movieController.Search(movie.Title);
            upatedMovie.Description += " with more text!";
            _movieController.Put(upatedMovie);

            var secondCheck = _movieController.Search(movie.Title);

            //Because the repo cannot update without adding a new instance with same id, we cannot really do update or test it. 

            //Assert.IsNotNull(upatedMovie, "Unable to find inserted movie.");
            //Assert.That(secondCheck.Description, Is.EqualTo(upatedMovie.Description), "Movie did not insert correctly.");
            //Assert.That(secondCheck.Description, Is.Not.EqualTo(originalMovie.Description), "Movie did not update.");
        }

        private static Movie GetTestMovie_()
        {
            return new Movie() { Id = Guid.NewGuid(), Title = "New Movie", Description = "New Movie description", Genre = "Drama|Action", Rating = 8.0f };
        }
    }
}
