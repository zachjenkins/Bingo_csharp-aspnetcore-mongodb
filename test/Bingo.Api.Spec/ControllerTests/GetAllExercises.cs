using Bingo.Api.Controllers;
using Bingo.Repository.Entities;
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
        private static readonly Mock<IExercisesService> MockService = new Mock<IExercisesService>();
        private readonly ExercisesController _controller = new ExercisesController(MockService.Object);

        private readonly IEnumerable<Exercise> _exercises = TestData.Exercises.ContractExercises;
        private readonly IEnumerable<Exercise> _nullResponse = null;

        [Fact(DisplayName = "Returns a 200 with expected exercises when service returns a list of exercise objects")]
        public void Returns200_WhenServiceReturnsListOfExerciseObjects()
        {
            //Arrange
            MockService.Setup(x => x.FindAllExercises()).ReturnsAsync(_exercises);

            // Act
            var response = _controller.GetAllExercises().Result as ObjectResult;

            // Assert
            this.ShouldSatisfyAllConditions(
                    () => response.ShouldBeOfType(typeof(OkObjectResult)),
                    () => response.Value.ShouldBe(_exercises)
                );
        }

        [Fact(DisplayName = "Returns a 200 with empty list when service returns a null value")]
        public void Returns200_WhenServicesReturnsNullObject()
        {
            //Arrange
            MockService.Setup(x => x.FindAllExercises()).ReturnsAsync(_nullResponse);

            // Act
            var response = _controller.GetAllExercises().Result as ObjectResult;
            var returnedExercises = response.Value as List<Exercise>;

            // Assert
            this.ShouldSatisfyAllConditions(
                () => returnedExercises.Count.ShouldBe(0),
                () => response.Value.ShouldBeOfType(typeof(List<Exercise>)),
                () => response.ShouldBeOfType(typeof(OkObjectResult))
            );
            
        }
    }
}
