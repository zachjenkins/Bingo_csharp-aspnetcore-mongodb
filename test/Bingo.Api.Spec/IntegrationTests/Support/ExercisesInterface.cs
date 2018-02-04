using System.Collections.Generic;
using Bingo.Repository.Entities;
using RestEase;
using System.Threading.Tasks;
using Bingo.Api.Models;

namespace Bingo.Specification.IntegrationTests.Support
{
    [AllowAnyStatusCode]
    public interface IExercisesApi
    {
        [Get("api/exercises")]
        Task<Response<List<Exercise>>> GetExercises();

        [Get("api/exercises/{id}")]
        Task<Response<Exercise>> GetExerciseById([Path] string id);

        [Post("api/exercises")]
        Task<Response<Exercise>> PostExercise([Body] PostExerciseDto postDto);

        [Delete("api/exercises/{id}")]
        Task<Response<string>> DeleteExerciseById([Path] string id);
    }
}
