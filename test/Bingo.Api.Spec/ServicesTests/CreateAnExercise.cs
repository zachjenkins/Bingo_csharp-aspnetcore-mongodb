using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Contracts;
using Bingo.Services.Services;
using Moq;
using Shouldly;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    [Trait("Service", "Creating an exercise")]
    public class CreateAnExercise
    {
        private static readonly Mock<IExercisesRepository> MockRepository = new Mock<IExercisesRepository>();
        private readonly IExercisesService _service = new ExercisesService(MockRepository.Object);

        private readonly Exercise _exercise = TestData.Exercises.ContractExercisePostDtoResponse;
        private readonly Exercise _exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
        private readonly Exercise _nullResponse = null;
        
        [Fact(DisplayName = "Returns created object when repository returns an exercise object")]
        public void Returns200_WhenServicesReturnsExerciseObject()
        {
            //Arrange
            MockRepository.Setup(x => x.InsertExercise(It.IsAny<Exercise>())).ReturnsAsync(_exercise);

            // Act
            var response = _service.CreateExercise(_exerciseToCreate).Result;

            // Assert
            response.ShouldBe(_exercise);
        }

        [Fact(DisplayName = "Returns a null value when repository returns a null value")]
        public void Returns400_WhenServicesReturnsNullValue()
        {
            //Arrange
            MockRepository.Setup(x => x.InsertExercise(It.IsAny<Exercise>())).ReturnsAsync(_nullResponse);

            // Act
            var response = _service.CreateExercise(_exerciseToCreate).Result;

            // Assert
            response.ShouldBeNull();
        }
    }
}