using System.Collections.Generic;
using Bingo.Repository.Entities;
using RestEase;
using System.Threading.Tasks;

namespace Bingo.Specification.IntegrationTests.Interfaces
{
    [AllowAnyStatusCode]
    public interface IExercisesApi
    {
        [Get("api/exercises")]
        Task<Response<List<Exercise>>> GetExercises();
    }
}
