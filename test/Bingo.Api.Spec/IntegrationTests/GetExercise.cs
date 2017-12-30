using System.Collections.Generic;
using System.Linq;
using System.Net;
using Bingo.Repository.Entities;
using Bingo.Specification.IntegrationTests.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RestEase;
using Shouldly;
using Xunit;
using Xunit.Extensions;

namespace Bingo.Specification.IntegrationTests
{
    public class ExercisesControllerTest: BaseHttpTest
    {
        private readonly IExercisesApi _exercisesApi;
        private readonly List<Exercise> _expectedExercises;

        public ExercisesControllerTest()
        {
            _expectedExercises = TestData.Exercises.ContractExercises;
            ExercisesCollection.InsertMany(_expectedExercises);
            _exercisesApi = RestClient.For<IExercisesApi>(Client);
        }
        /*
        [Fact]
        public void Get_Exercises_Returns_Exercises_And_200()
        {
            var response = _exercisesApi.GetExercises().Result;
            var actualExercises = response.GetContent();

            this.ShouldSatisfyAllConditions(
                    () => response.ResponseMessage.StatusCode.ShouldBe(HttpStatusCode.OK),
                    () => actualExercises.ShouldBe(_expectedExercises)
                );

            Dispose();
        }*/
    }
}
