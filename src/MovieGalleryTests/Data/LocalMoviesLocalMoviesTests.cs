using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieGallery.Data;

namespace MovieGalleryTests.Data
{
    [TestClass()]
    public class LocalMoviesLocalMoviesTests
    {
        [TestMethod()]
        public void GetMoviesfromDirGetMoviesFromDirTest()
        {
           LocalMovies lm = new LocalMovies();
         var result =  lm.GetMoviesfromDirList();
         Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CleanReleaseName()
        {
            LocalMovies lm = new LocalMovies();
            var result = lm.CleanReleaseNames(lm.GetMoviesfromDirList());
            Assert.IsNotNull(result);
        }
    }
}