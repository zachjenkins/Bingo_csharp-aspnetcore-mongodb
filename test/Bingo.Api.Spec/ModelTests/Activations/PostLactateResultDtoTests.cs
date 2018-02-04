using Bingo.Api.Models;
using Bingo.Repository.Entities;
using Bingo.Specification.Helpers;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Bingo.Specification.ModelTests
{
    [Trait("Api", nameof(PostLactateResultDtoTests))]
    public class PostLactateResultDtoTests
    {
        #region LactateResult ToLactateResult()

        [Fact]
        public void ToLactateResult_ConvertsDtoToLactateResultEntity_WhenValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.LactateProduction;
            postDto.ShouldNotHaveNullDataMembers<PostLactateResultDto>();

            // Act
            var lactateResult = postDto.ToLactateResult();

            lactateResult.LactateProduction = (double)postDto.LactateProduction;
            lactateResult.AerobicRespiration = (double)postDto.AerobicRespiration;
            lactateResult.AnaerobicRespiration = (double)postDto.AnaerobicRespiration;

            lactateResult.ShouldNotHaveNullDataMembers<LactateResult>();
        }

        #endregion

        #region ASP.NET ComponentModel DataAnnotation Validation

        [Fact]
        public void ModelValidation_ReturnsNoErrors_WhenModelStateIsValid()
        {
            // Arrange
            var postDto = TestData.Activations.ContractActivationPostDto.LactateProduction;

            // Act
            var modelValidation = AspHelpers.ValidateDto(postDto);

            // Assert
            modelValidation.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void ModelValidation_LactateProduction_CannotBeNull()
        {
            // Arrange
            var postDto = new PostLactateResultDto();

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LactateProduction));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.LactateProduction)} field is required");
        }

        [Fact]
        public void ModelValidation_LactateProduction_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostLactateResultDto { LactateProduction = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.LactateProduction));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.LactateProduction)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_AerobicRespiration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostLactateResultDto { AerobicRespiration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.AerobicRespiration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.AerobicRespiration)} must be between 0 and");
        }

        [Fact]
        public void ModelValidation_AnaerobicRespiration_CannotBeLessThanZero()
        {
            // Arrange
            var postDto = new PostLactateResultDto { AnaerobicRespiration = -1 };

            // Act
            var modelValidation = AspHelpers.ValidateDtoProperty(postDto, nameof(postDto.AnaerobicRespiration));

            // Assert
            modelValidation.IsValid.ShouldBeFalse();
            modelValidation.Results.First().ErrorMessage.ShouldContain($"{nameof(postDto.AnaerobicRespiration)} must be between 0 and");
        }

        #endregion
    }
}
