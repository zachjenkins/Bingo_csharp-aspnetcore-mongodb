using Bingo.Api.Controllers;
using Bingo.Repository.Entities;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Bingo.Specification.ControllerTests
{
    public class ExercisesControllerTests
    {
        protected ExercisesController ExercisesController { get; }
        protected Mock<IExercisesService> ExercisesServiceMock { get; }

        public ExercisesControllerTests()
        {
            ExercisesServiceMock = new Mock<IExercisesService>();
            ExercisesController = new ExercisesController(ExercisesServiceMock.Object);
        }

        [Fact]
        public async void ReadAllAsync_ReturnsOkObjectResult_WhenServiceReturnsExercises()
        {
            // Arrange
            var expectedExercises = TestData.Exercises.ContractExercises;
            ExercisesServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesController.ReadAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercises, okResult.Value);
        }

        [Fact]
        public async void ReadAllAsync_ReturnsOkObjectResult_WhenServiceReturnsEmptyList()
        {
            // Arrange
            var expectedExercises = new List<Exercise>();
            ExercisesServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesController.ReadAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercises, okResult.Value);
        }
   
        [Fact]
        public async void ReadOneAsync_ReturnsOkObjectResult_WhenServiceReturnsExercise()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            ExercisesServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesController.ReadOneAsync("123021");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercise, okResult.Value);
        }

        [Fact]
        public async void ReadOneAsync_ReturnsNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.ReadOneAsync("123021");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
   
        [Fact]
        public async void CreateOneAsync_ReturnsCreatedAtActionResult_WhenServiceReturnsExercise()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            var expectedExercise = TestData.Exercises.ContractExercisePostDtoResponse;
            ExercisesServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesController.CreateOneAsync(exercisePostDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.True(createdResult.StatusCode == 201);
        }

        [Fact]
        public async void CreateOneAsync_ReturnsBadRequestObjectResult_WhenServiceReturnsNull()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            ExercisesServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.CreateOneAsync(exercisePostDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void CreateOneAsync_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            ExercisesController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await ExercisesController.CreateOneAsync(exercisePostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    
        [Fact]
        public async void DeleteOneAsync_ReturnsNoContentResult_WhenServiceReturnsDeletedExercise()
        {
            // Arrange
            var deletedExercise = TestData.Exercises.ContractExercise;
            ExercisesServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedExercise);

            // Act
            var result = await ExercisesController.DeleteOneAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteOneAsync_ReturnsNoContentResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.DeleteOneAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
