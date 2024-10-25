using Flim.API.Common;
using Flim.API.Controllers;
using Flim.Application.DTOs;
using Flim.Application.Interfaces;
using Flim.Application.Records;
using Flim.Domain.ValueObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Film.Tests
{
    public class BookingControllerTests
    {
        private readonly Mock<IBookingService> _bookingServiceMock;
        private readonly BookingController _bookingController;

        public BookingControllerTests()
        {
            _bookingServiceMock = new Mock<IBookingService>();
            _bookingController = new BookingController(_bookingServiceMock.Object);
        }

        [Fact]
        public async Task GetAvailableBookings_ShouldReturnBadRequest_WhenIdIsZeroOrNegative()
        {
            // Arrange
            int invalidId = 0;
            var date = DateOnly.FromDateTime(DateTime.Now);

            // Act
            var result = await _bookingController.GetAvailableBookings(invalidId, date);

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            badRequestResult.Should().NotBeNull();
            badRequestResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var response = badRequestResult.Value as ApiResponse<string>;
            response.Should().NotBeNull();
            response.Message.Should().Be("Id should be greater than 0");
        }

        [Fact]
        public async Task GetAvailableBookings_ShouldReturnNotFound_WhenNoAvailableSeatsFound()
        {
            // Arrange
            int filmId = 1;
            var date = DateOnly.FromDateTime(DateTime.Now);
            _bookingServiceMock.Setup(service => service.GetAvailableSeats(filmId, date))
                .ReturnsAsync(new List<FilmRecord>()); // No available seats

            // Act
            var result = await _bookingController.GetAvailableBookings(filmId, date);

            // Assert
            var notFoundResult = result as NotFoundObjectResult;
            notFoundResult.Should().NotBeNull();
            notFoundResult.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
            var response = notFoundResult.Value as ApiResponse<string>;
            response.Should().NotBeNull();
            response.Message.Should().Be($"{filmId} not found in this date {date}");

        }

        [Fact]
        public async Task GetAvailableBookings_ShouldReturnOk_WhenAvailableSeatsFound()
        {
            // Arrange
            int filmId = 1;
            var date = DateOnly.FromDateTime(DateTime.Now);
            var availableSeats = new List<FilmRecord>
            {
                new FilmRecord(filmId, "Film Name", "Description", "Genre", 120, new List<SlotRecord>())
            };
            _bookingServiceMock.Setup(service => service.GetAvailableSeats(filmId, date))
                .ReturnsAsync(availableSeats);

            // Act
            var result = await _bookingController.GetAvailableBookings(filmId, date);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var response = okResult.Value as List<FilmRecord>;
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(availableSeats);
        }
    }
}
