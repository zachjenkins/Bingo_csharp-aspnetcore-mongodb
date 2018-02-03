using Bingo.Api.Models;
using Bingo.Specification.Helpers;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Bingo.Specification.ModelTests
{
    [Trait("Model", nameof(ExerciseDtoTests))]
    public class ExerciseDtoTests
    {
        [Fact]
        public void ExercisePostDtoValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Exercises.ContractExercisePostDto;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
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
            results.ShouldBeEmpty();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresName_WhenNameIsNullOrWhitespace(string name)
        {
            // Arrange
            var postDto = new PostExerciseDto { Name = name };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Name));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.Name)} field is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresShortName_WhenShortNameIsNullOrWhitespace(string shortName)
        {
            // Arrange
            var postDto = new PostExerciseDto { ShortName = shortName };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ShortName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.ShortName)} field is required");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void ExercisePostDtoValidation_RequiresLongName_WhenLongNameIsNullOrWhitespace(string longName)
        {
            // Arrange
            var postDto = new PostExerciseDto { LongName = longName };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LongName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.LongName)} field is required");
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf30_ForNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { Name = new string('a', 31) };
          
            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.Name));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'30'");
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf20_ForShortNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { ShortName = new string('a', 21) };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.ShortName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'20'");
        }

        [Fact]
        public void ExercisePostDtoValidation_RequiresMaxLenghtOf30_ForLongNameProperty()
        {
            // Arrange
            var postDto = new PostExerciseDto { LongName = new string('a', 61) };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LongName));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.Count.ShouldBe(1);
            modelValidation.Results.First().ErrorMessage.ShouldContain("'60'");
        }
    }
}
