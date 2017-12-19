﻿using Bingo.Repository.Entities;
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
        private static readonly Mock<IExercisesRepository> MockRepository = new Mock<IExercisesRepository>();
        private readonly ExercisesService _service = new ExercisesService(MockRepository.Object);
        private readonly Exercise _exercise = TestData.Exercises.ContractExercise;
        private readonly Exercise _nullResponse = null;

        [Fact(DisplayName = "Returns expected exercise object when repository returns exercise object")]
        public void ReturnsExerciseObject_WhenRepositoryReturnsExerciseObject()
        {
            // Arrange
            MockRepository.Setup(x => x.SelectExerciseById(It.IsAny<string>())).ReturnsAsync(_exercise);

            //Act
            var response = _service.FindExerciseById("ValidId");

            //Assert
            response.Result.ShouldBe(_exercise);
        }

        [Fact(DisplayName = "Returns null value when repository returns null value")]
        public void ReturnsNullValue_WhenRepositoryReturnsNullValue()
        {
            // Arrange
            MockRepository.Setup(x => x.SelectExerciseById(It.IsAny<string>())).ReturnsAsync(_nullResponse);

            //Act
            var response = _service.FindExerciseById("InvalidId");

            //Assert
            response.Result.ShouldBeNull();
        }
    }
}
