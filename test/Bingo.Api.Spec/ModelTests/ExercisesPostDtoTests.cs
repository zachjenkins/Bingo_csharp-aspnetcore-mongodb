using Bingo.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Bingo.Specification.ModelTests
{
    public class ExercisesPostDtoTests
    {

    }

    public class ExerciseDtoValidation : ExercisesPostDtoTests
    {

        [Fact]
        public void Should_Return_No_Errors_When_Model_Is_Valid()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;

            // Act
            var isValid = Validator.TryValidateObject(postDto, new ValidationContext(postDto), new List<ValidationResult>());

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Should_Require_Name_Field_Without_Null_Or_Whitespace(string name)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                Name = name
            };
            var property = nameof(postDto.Name);
            var context = new ValidationContext(postDto) { MemberName = property};
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
        public void Should_Require_ShortName_Field_Without_Null_Or_Whitespace(string shortName)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                ShortName = shortName
            };
            var property = nameof(postDto.ShortName);
            var context = new ValidationContext(postDto) { MemberName = property };
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
        public void Should_Require_LongName_Field_Without_Null_Or_Whitespace(string longName)
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                LongName = longName
            };
            var property = nameof(postDto.LongName);
            var context = new ValidationContext(postDto) { MemberName = property };
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.True(results.First().ErrorMessage.Contains($"{property} field is required"));
        }

        [Fact]
        public void Should_Require_Name_To_Have_30_Character_Maximum()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                Name = new string('a', 31)
            };
            var context = new ValidationContext(postDto) { MemberName = nameof(postDto.Name) };
            var results = new List<ValidationResult>();
                
            // Act
            var isValid = Validator.TryValidateProperty(postDto.Name, context, results);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.True(results.First().ErrorMessage.Contains("'30'"));
        }

        [Fact]
        public void Should_Require_ShortName_To_Have_20_Character_Maximum()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                ShortName = new string('a', 21)
            };
            var context = new ValidationContext(postDto) { MemberName = nameof(postDto.ShortName) };
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateProperty(postDto.ShortName, context, results);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.True(results.First().ErrorMessage.Contains("'20'"));
        }

        [Fact]
        public void Should_Require_LongName_To_Have_60_Character_Maximum()
        {
            // Arrange
            var postDto = new PostExerciseDto
            {
                LongName = new string('a', 61)
            };
            var context = new ValidationContext(postDto) { MemberName = nameof(postDto.LongName) };
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
