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
    }

    public class ReadAllAsync : ExercisesControllerTests
    {
        [Fact]
        public async void Should_Return_OkObjectResult_And_Expected_Exercises_When_Service_Returns_Exercises()
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
        public async void Should_Return_OkObjectResult_And_Empty_Array_When_Service_Return_Empty_Array()
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
    }

    public class ReadOneAsync : ExercisesControllerTests
    {
        [Fact]
        public async void Should_Return_OkObjectResult_And_Expected_Exercise_When_Service_Returns_Exercise()
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
        public async void Should_Return_NotFoundResult_When_Service_Returns_Null()
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
    }

    public class CreateOneAsync : ExercisesControllerTests
    {
        [Fact]
        public async void Should_Return_CreatedAtActionResult_And_Created_Exercise_When_Service_Returns_Exercise()
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
        public async void Should_Return_BadRequestObjectResult_When_Service_Returns_Null()
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
        public async void Should_Return_BadRequestObjectResult_When_ModelState_Is_Invalid()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            ExercisesController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await ExercisesController.CreateOneAsync(exercisePostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

    public class DeleteOneAsync : ExercisesControllerTests
    {
        [Fact]
        public async void Should_Return_NoContentResult_When_Service_Returns_Deleted_Exercise()
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
        public async void Should_Return_NoContentResult_When_Service_Returns_Null()
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
