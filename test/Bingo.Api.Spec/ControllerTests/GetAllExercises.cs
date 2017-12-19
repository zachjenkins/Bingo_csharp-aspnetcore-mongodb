using Bingo.Api.Controllers;
using Bingo.Domain.Entities;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ControllerTests
{
    [Trait("Controller", "Getting all exercises")]
    public class GetAllExercises
    {
        private readonly Mock<IExercisesService> _mockService;
        private readonly ExercisesController _controller;
        private readonly List<Exercise> _exercises;

        public GetAllExercises()
        {
            _mockService = new Mock<IExercisesService>();
            
            _controller = new ExercisesController(_mockService.Object);

            _exercises = new List<Exercise>
            {
                new Exercise
                {
                    Id = "129232",
                    LongName = "This is an exercise"
                },
                new Exercise
                {
                    Id = "1293232",
                    LongName = "This is an exercise too!"
                }
            };
        }

        [Fact(DisplayName = "Returns a 200 response code when service returns a list of exercise objects")]
        public void Returns200_WhenServiceReturnsListOfExerciseObjects()
        {
            //Arrange
            _mockService.Setup(x => x.FindAllExercises()).ReturnsAsync(_exercises);

            // Act
            var response = _controller.GetAllExercises();

            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
        }

        [Fact(DisplayName = "Returns a 200 response code when service returns a null value")]
        public void Returns200_WhenServicesReturnsNullObject()
        {
            //Arrange
            _mockService.Setup(x => x.FindAllExercises()).Returns<object>(null);

            // Act
            var response = _controller.GetAllExercises();

            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
        }

        [Fact(DisplayName = "Returns a list of exercise objects when service returns a list of exercise objects")]
        public void ReturnsListOfExerciseObjects_WhenServiceReturnsListExerciseObjects()
        {
            //Arrange
            _mockService.Setup(x => x.FindAllExercises()).ReturnsAsync(_exercises);

            // Act
            var response = _controller.GetAllExercises().Result as ObjectResult;

            // Assert
            response.Value.ShouldBe(_exercises);
        }

        [Fact(DisplayName = "Returns an empty list when service returns a null value")]
        public void ReturnsEmptyList_WhenServiceReturnsNullValue()
        {
            //Arrange
            _mockService.Setup(x => x.FindAllExercises()).Returns<object>(null);

            // Act
            var response = _controller.GetAllExercises().Result as ObjectResult;
            var returnedExercises = response.Value as List<Exercise>;

            // Assert
            this.ShouldSatisfyAllConditions(
                    () => returnedExercises.Count.ShouldBe(0),
                    () => response.Value.ShouldBeOfType(typeof(List<Exercise>))
                );
        }
    }
}
