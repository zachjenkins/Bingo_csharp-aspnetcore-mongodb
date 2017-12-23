using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    public class ExercisesServiceTests
    {
        protected ExercisesService ExercisesService { get; }
        protected Mock<IExercisesRepository> ExercisesRepositoryMock { get; }

        public ExercisesServiceTests()
        {
            ExercisesRepositoryMock = new Mock<IExercisesRepository>();
            ExercisesService = new ExercisesService(ExercisesRepositoryMock.Object);
        }
    }

    public class ReadAllAsync : ExercisesServiceTests
    {
        [Fact]
        public async void Should_Return_Exercises_When_Repository_Returns_Exercises()
        {
            // Arrange
            var expectedExercises = TestData.Exercises.ContractExercises;
            ExercisesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesService.ReadAllAsync();

            // Assert
            Assert.Same(expectedExercises, result);
        }

        [Fact]
        public async void Should_Return_Empty_Array_When_Repository_Return_Empty_Array()
        {
            // Arrange
            var expectedExercises = new List<Exercise>();
            ExercisesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesService.ReadAllAsync();

            // Assert
            Assert.Same(expectedExercises, result);
        }
    }
 
    public class ReadOneAsync : ExercisesServiceTests
    {
        [Fact]
        public async void Should_Return_Exercise_When_Service_Returns_Exercise()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesService.ReadOneAsync("123021");

            // Assert
            Assert.Same(expectedExercise, result);
        }

        [Fact]
        public async void Should_Return_Null_When_Service_Returns_Null()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.ReadOneAsync("123021");

            // Assert
            Assert.Null(result);
        }
    }
    
    public class CreateOneAsync : ExercisesServiceTests
    {
        [Fact]
        public async void Should_Return_Created_Exercise_When_Repository_Returns_Exercise()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
            var createdExercise = TestData.Exercises.ContractExercisePostDtoResponse;
            ExercisesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync(createdExercise);

            // Act
            var result = await ExercisesService.CreateOneAsync(exerciseToCreate);

            // Assert
            Assert.Same(createdExercise, result);
        }

        [Fact]
        public async void Should_Return_Null_When_Repository_Returns_Null()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
            ExercisesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.CreateOneAsync(exerciseToCreate);

            // Assert
            Assert.Null(result);
        }
    }
    
    public class DeleteOneAsync : ExercisesServiceTests
    {
        [Fact]
        public async void Should_Return_Exercise_When_Repository_Returns_Deleted_Exercise()
        {
            // Arrange
            var deletedExercise = TestData.Exercises.ContractExercise;
            ExercisesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedExercise);

            // Act
            var result = await ExercisesService.DeleteOneAsync("123021");

            // Assert
            Assert.Same(deletedExercise, result);
        }

        [Fact]
        public async void Should_Return_Null_When_Repository_Returns_Null()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.DeleteOneAsync("123021");

            // Assert
            Assert.Null(result);
        }
    }
}
