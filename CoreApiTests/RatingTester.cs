using System.Globalization;
using System.Text.Json;
using CoreApi.Controllers;
using CoreApi.Models;

namespace CoreAPITests
{
    [TestFixture, Description("Tests the rating functions of the BoxOffice CoreAPI")]

    public class RatingTester
    {
        private const string movieGuid = "3719cc0c-ffca-453c-9734-9e581cf28179";
        private const string movieGuid2 = "4ee04b48-8421-4aae-9000-12356c9e1936";
        private static MoviesController _movieController;

        [OneTimeSetUp]
        public void Setup()
        {
            _movieController = new MoviesController();
        }

        [Test, Description("Checks if movie implements ratings.") /*, MaxTime(500)*/]
        public void MoviesHasRating()
        {
            var movies = _movieController.Get().ToList();

            movies.ForEach(movie =>
            {
                var t = movie.GetType();
                var rating = t.GetProperty("Rating");
                var ratings = t.GetProperty("Ratings");

                Assert.IsNotNull(rating, "Property Rating is missing on object Movie");
                Assert.IsNotNull(ratings, "Property Ratings is missing on object Movie");
            });
        }

        [Test, Description("Checks that wrongly inputed guids for ratings are handled."), /*MaxTime(500)*/]
        public void EmptyGuidCheck()
        {
            Assert.DoesNotThrow(() => _movieController.Rate(Guid.Empty, 5), "Is unable to handle empty guids.");            
        }

        [Test, Description("Checks that only ratings between 0 and 5 can be added.")]
        public void RatingValueCheck()
        {
            Assert.IsNull(_movieController.Rate(new Guid(movieGuid2), -1), "Accepts negative values.");
            Assert.IsNull(_movieController.Rate(new Guid(movieGuid2), 11), "Accepts too high values.");
        }

        [Test, Description("Checks that ratings are increased correctly."), /*MaxTime(500)*/]
        public void RatingIsIncreasedCorrectly()
        {
            //Arrange
            var movieBeforeRating = _movieController.Get(new Guid(movieGuid2));

            //Act
            var movieAfterRating = _movieController.Rate(new Guid(movieGuid2), 10);

            //Assert
            Assert.GreaterOrEqual(movieAfterRating.Rating, movieBeforeRating.Rating);
        }

        [Test, Description("Checks that ratings are decreased correctly."), /*MaxTime(500)*/]
        public void Rate_WhenRatingIsDecreased_DecreasedRatingIsStored()
        {
            //Arrange
            var movieBeforeRating = _movieController.Get(new Guid(movieGuid));

            //Act
            var movieAfterRating = _movieController.Rate(new Guid(movieGuid), 0);

            //Assert
            Assert.Less(movieAfterRating.Rating, movieBeforeRating.Rating);
        }
    }
}
