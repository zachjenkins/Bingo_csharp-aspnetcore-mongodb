using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using Bingo.Services.Services;
using Moq;
using Shouldly;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    [Trait("Service", "Finding an exercise by Id")]
    public class FindGroupById
    {
        private readonly Mock<IExercisesRepository> _mockRepository;
        private readonly ExercisesService _service;
        private readonly Exercise _exercise;

        public FindGroupById()
        {
            _mockRepository = new Mock<IExercisesRepository>();

            _service = new ExercisesService(_mockRepository.Object);

            _exercise = new Exercise
            {
                Id = "129232",
                LongName = "This is an exercise"
            };
        }

        [Fact(DisplayName = "Returns expected exercise object when repository returns exercise object")]
        public void ReturnsExerciseObject_WhenRepositoryReturnsExerciseObject()
        {
            // Arrange
            _mockRepository.Setup(x => x.SelectExerciseById(It.IsAny<string>())).Returns(_exercise);

            //Act
            var result = _service.FindExerciseById("ValidId");

            //Assert
            result.ShouldBe(_exercise);
        }

        [Fact(DisplayName = "Returns null value when repository returns null value")]
        public void ReturnsNullValue_WhenRepositoryReturnsNullValue()
        {
            // Arrange
            _mockRepository.Setup(x => x.SelectExerciseById(It.IsAny<string>())).Returns<object>(null);

            //Act
            var result = _service.FindExerciseById("InvalidId");

            //Assert
            result.ShouldBeNull();
        }
    }
}
