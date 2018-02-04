using Bingo.Specification.Helpers;
using Bingo.Specification.IntegrationTests.Support;
using Bingo.Specification.TestData;
using Shouldly;
using System.Net;
using Xunit;

namespace Bingo.Specification.IntegrationTests
{
    [Trait("Integration", nameof(ExercisesControllerTest))]
    public class ExercisesControllerTest : TestBase
    {
        private readonly ServiceFixture _service;

        public ExercisesControllerTest(ServiceFixture service)
        {
            _service = service;
        }
        
        [Fact]
        public async void GetExercises_WhenDataExists_ReturnsListContainingExpectedExercise200()
        {
            var expectedExercises = Exercises.ContractExercises;

            var response = await _service.Api.GetExercises();
            var actualExercises = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                    () => expectedExercises.ForEach(exercise =>
                            actualExercises.ShouldContain(exercise))
                );
        }

        [Fact]
        public async void GetExercise_ByExerciseId_ReturnsExpectedExercise200()
        {
            var expectedExercise = Exercises.ContractExercise;

            var response = await _service.Api.GetExerciseById(expectedExercise.Id);
            var actualExercise = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                    () => actualExercise.ShouldBe(expectedExercise),
                    () => _service.ExercisesCollection.ShouldContain(actualExercise)
                );
        }

        [Fact]
        public async void PostExercise_ByValidDto_ReturnsPostedExercise201()
        {
            var postDto = Exercises.ContractExercisePostDto;

            var response = await _service.Api.PostExercise(postDto);
            var postedExercise = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.Created),
                    () => postedExercise.Id.ShouldNotBeNull(),
                    () => postedExercise.ShouldBe(postDto.ToExercise()),
                    () => _service.ExercisesCollection.ShouldContain(postedExercise)
                );
        }

        [Fact]
        public async void DeleteExercise_ByExerciseId_ReturnsNoData204()
        {
            var exerciseToDelete = Exercises.RandomizedExercise;
            _service.ExercisesCollection.InsertOne(exerciseToDelete);

            var response = await _service.Api.DeleteExerciseById(exerciseToDelete.Id);

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.NoContent),
                    () => response.StringContent.ShouldBeEmpty(),
                    () => _service.ExercisesCollection.ShouldNotContain(exerciseToDelete),
                    () => _service.ExercisesCollection.ShouldNotBeEmpty()
                );
        }
    }
}
