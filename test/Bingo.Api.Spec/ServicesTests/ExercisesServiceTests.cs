using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ServicesTests
{
    [Trait("Service", nameof(ExercisesServiceTests))]
    public class ExercisesServiceTests
    {
        protected ExercisesService ExercisesService { get; }
        protected Mock<IExercisesRepository> ExercisesRepositoryMock { get; }

        public ExercisesServiceTests()
        {
            ExercisesRepositoryMock = new Mock<IExercisesRepository>();
            ExercisesService = new ExercisesService(ExercisesRepositoryMock.Object);
        }

        #region Task<IEnumerable<Exercise>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_ReturnsExercises_WhenRepositoryReturnsExercises()
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
        public async void ReadAllAsync_ReturnsEmptyArray_WhenRepositoryReturnsEmptyArray()
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

        #endregion

        #region Task<Exercise> ReadOneAsync(string id)
        
        [Fact]
        public async void ReadOneAsync_ReturnsExercise_WhenRepositoryReturnsExercise()
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
        public async void ReadOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
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

        #endregion

        #region Task<Exercise> CreateOneAsync(Exercise exerciseToCreate)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedExercise_WhenServiceReturnsExercise()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
            var createdExercise = TestData.Exercises.ContractExercisePostDtoResponseMock;
            ExercisesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync(createdExercise);

            // Act
            var result = await ExercisesService.CreateOneAsync(exerciseToCreate);

            // Assert
            Assert.Same(createdExercise, result);
        }

        [Fact]
        public async void CreateOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
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

        #endregion

        #region Task<Exercise> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ReturnsExercise_WhenRepositoryReturnsExercise()
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
        public async void DeleteOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
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

        #endregion
    }
}
