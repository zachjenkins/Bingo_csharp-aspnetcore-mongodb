using Bingo.Api.Controllers;
using Bingo.Repository.Entities;
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
        private static readonly Mock<IExercisesService> MockService = new Mock<IExercisesService>();
        private readonly ExercisesController _controller = new ExercisesController(MockService.Object);

        private readonly Exercise _exercise = TestData.Exercises.ContractExercise;
        private readonly Exercise _nullResponse = null;
      
        [Fact(DisplayName = "Returns a 200 with expected exercise when service returns an exercise object")]
        public void Returns200_WhenServicesReturnsExerciseObject()
        {
            //Arrange
            MockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).ReturnsAsync(_exercise);

            // Act
            var response = _controller.GetExerciseById("InvalidId").Result as ObjectResult;

            // Assert
            this.ShouldSatisfyAllConditions(
                    () => response.ShouldBeOfType(typeof(OkObjectResult)),
                    () => response.Value.ShouldBe(_exercise)
                );
            
        }

        [Fact(DisplayName = "Returns a 404 with no data when service returns a null value")]
        public void Returns404_WhenServicesReturnsNullObject()
        {
            //Arrange
            MockService.Setup(x => x.FindExerciseById(It.IsAny<string>())).ReturnsAsync(_nullResponse);

            // Act
            var response = _controller.GetExerciseById("InvalidId");

            // Assert
            response.Result.ShouldBeOfType(typeof(NotFoundResult));
        }
    }
}
