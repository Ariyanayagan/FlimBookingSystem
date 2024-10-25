using Flim.API.Common;
using Flim.API.Controllers;
using Flim.Application.Interfaces;
using Flim.Application.Records;
using Flim.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace Film.Tests
{
    public class FIlmControllerTests
    {
        private readonly Mock<IFilmService> _filmServiceMock;
        private readonly FlimController _filmController;

        public FIlmControllerTests()
        {
            _filmServiceMock = new Mock<IFilmService>();
            _filmController = new FlimController(_filmServiceMock.Object);
        }

        [Fact]
        public async void GetFilmByName_ShouldReturnFilm_WhenFilmExists()
        {
            // arrange
            var filmName = "Leo";
            var films = new List<Flim.Domain.Entities.Film>
            {
                new Flim.Domain.Entities.Film { Name = "sdcjds", Genre = "Action", Amount = 100, Description = "Vdfcscdscdscdscsdijay", Duration = 100 }
            };
            _filmServiceMock.Setup(service => service.GetFilmByNameAsync(filmName))
                .ReturnsAsync(films);

            // Act
            var result = await _filmController.GetFlimByName(filmName);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

            var response = okResult.Value as ApiResponse<IEnumerable<ShowFilmRecord>>;
            response.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async void GetFilmByName_ShouldReturnNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            var filmName = "Leo"; // Requesting "Leo"
            var films = new List<Flim.Domain.Entities.Film>(); 

            _filmServiceMock.Setup(service => service.GetFilmByNameAsync(filmName))
                .ReturnsAsync(films);

            // Act
            var result = await _filmController.GetFlimByName(filmName);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

            var response = notFoundResult.Value as ApiResponse<string>;
            response.IsSuccess.Should().BeFalse();
            response.Message.Should().Be($"{filmName} not found!");
        }



    }
}
