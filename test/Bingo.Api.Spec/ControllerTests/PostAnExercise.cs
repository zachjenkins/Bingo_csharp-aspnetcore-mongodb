using System.Net;
using Bingo.Api.Controllers;
using Bingo.Api.Models;
using Bingo.Domain.Entities;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Bingo.Specification.ControllerTests
{
    [Trait("Controller", "Posting an exercise")]
    public class PostAnExercise
    {
        private readonly Mock<IExercisesService> _mockService;
        private readonly ExercisesController _controller;
        private readonly PostExerciseDto _exerciseDto;
        private readonly Exercise _exercise;

        public PostAnExercise()
        {
            _mockService = new Mock<IExercisesService>();
            
            _controller = new ExercisesController(_mockService.Object);

            _exerciseDto = new PostExerciseDto
            {
                Name = "This is an exercise"
            };

            _exercise = new Exercise
            {
                Id = "129232",
                Name = "This is an exercise"
            };
        }

        [Fact(DisplayName = "Returns a 201 response code when service returns an exercise object")]
        public void Returns200_WhenServicesReturnsExerciseObject()
        {
            //Arrange
            _mockService.Setup(x => x.CreateExercise(It.IsAny<Exercise>())).Returns(_exercise);

            // Act
            var response = _controller.PostExercise(_exerciseDto) as ObjectResult;

            // Assert
            response.StatusCode.Value.ShouldBe(201);
        }

        [Fact(DisplayName = "Returns an exercise object when service returns an exercise object")]
        public void ReturnsExerciseObject_WhenServiceReturnsExerciseObject()
        {
            //Arrange
            _mockService.Setup(x => x.CreateExercise(It.IsAny<Exercise>())).Returns(_exercise);

            // Act
            var response = _controller.PostExercise(_exerciseDto) as ObjectResult;
            var returnedObject = response.Value;

            // Assert
            returnedObject.ShouldBe(_exercise);
        }
    }
}
