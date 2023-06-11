using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using TP_LC4.Controllers;
using TP_LC4.Models;
using Xunit;

namespace TP_LC4.Tests
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task Create_ValidMovie_RedirectsToIndex()
        {
            
            var movie = new Movie { MovieName = "Test Movie", MovieGenre = "Test Genre", MovieDuration = 120, MovieBudget = 1000000 };
            var mockContext = new Mock<TpLc4Context>();
            mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(0));
            var controller = new MoviesController(mockContext.Object);

            
            var result = await controller.Create(movie);

            
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
