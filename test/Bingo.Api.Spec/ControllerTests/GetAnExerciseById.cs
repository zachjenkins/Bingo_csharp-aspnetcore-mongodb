using System.Threading.Tasks;
using Bingo.Api.Controllers;
using Bingo.Domain.Entities;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Xunit;

namespace Bingo.Specification.ControllerTests
{
    [Trait("Controller", "Getting an exercise by Id")]
    public class GetAnExerciseById
    {
        private readonly Mock<IExercisesService> _mockService;
        private readonly ExercisesController _controller;
        private readonly Exercise _exercise;

        public GetAnExerciseById()
        {
            _mockService = new Mock<IExercisesService>();
            
            _controller = new ExercisesController(_mockService.Object);

            _exercise = new Exercise
            {
                Id = "129232",
                LongName = "This is an exercise"
            };
        }

        [Fact(DisplayName = "Returns a 200 response code when service returns an exercise object")]
        public void Returns200_WhenServicesReturnsExerciseObject()
        {
            //Arrange
            _mockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).ReturnsAsync(_exercise);

            // Act
            var response = _controller.GetExerciseById("InvalidId");

            // Assert
            response.ShouldBeOfType(typeof(OkObjectResult));
        }

        [Fact(DisplayName = "Returns a 404 response code when service returns a null value")]
        public void Returns404_WhenServicesReturnsNullObject()
        {
            //Arrange
            _mockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).Returns<object>(null);

            // Act
            var response = _controller.GetExerciseById("InvalidId");

            // Assert
            response.ShouldBeOfType(typeof(NotFoundResult));
        }

        [Fact(DisplayName = "Returns an exercise object when service returns an exercise object")]
        public void ReturnsExerciseObject_WhenServiceReturnsExerciseObject()
        {
            //Arrange
            _mockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).ReturnsAsync(_exercise);

            // Act
            var response = _controller.GetExerciseById("ValidId").Result as ObjectResult;
            var returnedObject = response.Value;

            // Assert
            returnedObject.ShouldBe(_exercise);
        }

        [Fact(DisplayName = "Returns no data when service returns a null value")]
        public void ReturnsNoData_WhenServiceReturnsNullValue()
        {
            //Arrange
            _mockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).Returns<object>(null);

            // Act
            var response = _controller.GetExerciseById("InvalidId").Result as ObjectResult;

            // Assert
            response.ShouldBeNull();
        }
    }
}
