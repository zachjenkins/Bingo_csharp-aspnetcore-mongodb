using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using Bingo.Services.Services;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    [Trait("Service", "Finding an exercise by Id")]
    public class FindAllGroups
    {
        private readonly Mock<IExercisesRepository> _mockRepository;
        private readonly ExercisesService _service;
        private readonly List<Exercise> _exercises;

        public FindAllGroups()
        {
            _mockRepository = new Mock<IExercisesRepository>();

            _service = new ExercisesService(_mockRepository.Object);

            _exercises = new List<Exercise>
            {
                new Exercise
                {
                    Id = "129232",
                    LongName = "This is an exercise"
                },
                new Exercise
                {
                    Id = "1292a1243",
                    LongName = "This is an exercise too!"
                }
            };
        }

        [Fact(DisplayName = "Returns list of expected exercise objects when repository returns list of exercise objects")]
        public void ReturnsExerciseObject_WhenRepositoryReturnsExerciseObject()
        {
            // Arrange
            _mockRepository.Setup(x => x.SelectAllExercises()).Returns(_exercises);

            //Act
            var result = _service.FindAllExercises();

            //Assert
            result.ShouldBe(_exercises);
        }

        [Fact(DisplayName = "Returns null value when repository returns null value")]
        public void ReturnsNullValue_WhenRepositoryReturnsNullValue()
        {
            // Arrange
            _mockRepository.Setup(x => x.SelectAllExercises()).Returns<object>(null);

            //Act
            var result = _service.FindAllExercises();

            //Assert
            result.ShouldBeNull();
        }
    }
}
