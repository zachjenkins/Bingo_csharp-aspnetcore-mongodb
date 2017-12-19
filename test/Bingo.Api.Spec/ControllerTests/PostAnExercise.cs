using Bingo.Api.Controllers;
using Bingo.Api.Models;
using Bingo.Repository.Entities;
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
        private static readonly Mock<IExercisesService> MockService = new Mock<IExercisesService>();
        private readonly ExercisesController _controller = new ExercisesController(MockService.Object);

        private readonly Exercise _exercise = TestData.Exercises.ContractExercisePostDtoResponse;
        private readonly PostExerciseDto _exerciseDto = TestData.Exercises.ContractExercisePostDto;
        private readonly Exercise _nullResponse = null;
        
        [Fact(DisplayName = "Returns a 201 with posted object when service returns an exercise object")]
        public void Returns200_WhenServicesReturnsExerciseObject()
        {
            //Arrange
            MockService.Setup(x => x.CreateExercise(It.IsAny<Exercise>())).ReturnsAsync(_exercise);

            // Act
            var response = _controller.PostExercise(_exerciseDto).Result as ObjectResult;

            // Assert
            this.ShouldSatisfyAllConditions(
                    () => response.StatusCode.Value.ShouldBe(201),
                    () => response.Value.ShouldBe(_exercise)
                );
        }

        [Fact(DisplayName = "Returns a 400 with no data when service returns a null value")]
        public void Returns400_WhenServicesReturnsNullValue()
        {
            //Arrange
            MockService.Setup(x => x.CreateExercise(It.IsAny<Exercise>())).ReturnsAsync(_nullResponse);

            // Act
            var response = _controller.PostExercise(_exerciseDto);

            // Assert
            response.Result.ShouldBeOfType(typeof(BadRequestResult));
        }

        [Fact(DisplayName = "Returns a 400 when model state is invalid")]
        public void Returns400_WhenModelStateIsInvalid()
        {
            //Arrange
            MockService.Setup(x => x.CreateExercise(It.IsAny<Exercise>())).ReturnsAsync(_nullResponse);
            _controller.ModelState.AddModelError("Test Error", "NOOOO!");

            // Act
            var response = _controller.PostExercise(_exerciseDto).Result as ObjectResult;

            // Assert
            response.StatusCode.Value.ShouldBe(400);
        }
    }
}