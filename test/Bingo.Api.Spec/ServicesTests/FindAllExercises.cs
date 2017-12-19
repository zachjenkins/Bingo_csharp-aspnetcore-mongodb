using Bingo.Repository.Entities;
using Bingo.Repository.Contracts;
using Bingo.Services.Services;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    [Trait("Service", "Finding all exercises")]
    public class FindAllExercises
    {
        private static readonly Mock<IExercisesRepository> MockRepository = new Mock<IExercisesRepository>();
        private readonly ExercisesService _service = new ExercisesService(MockRepository.Object);
        private readonly IEnumerable<Exercise> _exercises = TestData.Exercises.ContractExercises;
        private readonly IEnumerable<Exercise> _nullResponse = null;

        [Fact(DisplayName = "Returns list of expected exercise objects when repository returns list of exercise objects")]
        public void ReturnsExerciseObject_WhenRepositoryReturnsExerciseObject()
        {
            // Arrange
            MockRepository.Setup(x => x.SelectAllExercises()).ReturnsAsync(_exercises);

            //Act
            var response = _service.FindAllExercises();

            //Assert
            response.Result.ShouldBe(_exercises);
        }

        [Fact(DisplayName = "Returns null value when repository returns null value")]
        public void ReturnsNullValue_WhenRepositoryReturnsNullValue()
        {
            // Arrange
            MockRepository.Setup(x => x.SelectAllExercises()).ReturnsAsync(_nullResponse);

            //Act
            var response = _service.FindAllExercises();

            //Assert
            response.Result.ShouldBeNull();
        }
    }
}
