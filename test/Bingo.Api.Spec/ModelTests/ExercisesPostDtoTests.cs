using Bingo.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Bingo.Specification.ModelTests
{
    public class ExerciseDtoTests
    {
        [Fact]
        public void ExercisePostDtoValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;

            // Act
            var isValid = Validator.TryValidateObject(postDto, new ValidationContext(postDto), new List<ValidationResult>());

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ExercisePostDtoValidation_ReturnsNoErrors_WhenValidateMethodIsCalledWithValidObject()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;
            var context = new ValidationContext(postDto);

            // Act
            var results = postDto.Validate(context);

            // Assert
            Assert.Empty(results);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresName_WhenNameIsNullOrWhitespace(string name)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                Name = name
            };
            var property = nameof(postDto.Name);
            var context = new ValidationContext(postDto) {MemberName = property};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.True(results.First().ErrorMessage.Contains($"{property} field is required"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresShortName_WhenShortNameIsNullOrWhitespace(string shortName)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                ShortName = shortName
            };
            var property = nameof(postDto.ShortName);
            var context = new ValidationContext(postDto) {MemberName = property};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.True(results.First().ErrorMessage.Contains($"{property} field is required"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresLongName_WhenLongNameIsNullOrWhitespace(string longName)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                LongName = longName
            };
            var property = nameof(postDto.LongName);
            var context = new ValidationContext(postDto) {MemberName = property};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.True(results.First().ErrorMessage.Contains($"{property} field is required"));
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf30_ForNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                Name = new string('a', 31)
            };
            var context = new ValidationContext(postDto) {MemberName = nameof(postDto.Name)};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.True(results.First().ErrorMessage.Contains("'30'"));
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf20_ForShortNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                ShortName = new string('a', 21)
            };
            var context = new ValidationContext(postDto) {MemberName = nameof(postDto.ShortName)};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.ShortName, context, results);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.True(results.First().ErrorMessage.Contains("'20'"));
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf30_ForLongNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                LongName = new string('a', 61)
            };
            var context = new ValidationContext(postDto) {MemberName = nameof(postDto.LongName)};
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.LongName, context, results);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.True(results.First().ErrorMessage.Contains("'60'"));
        }
    }
}
