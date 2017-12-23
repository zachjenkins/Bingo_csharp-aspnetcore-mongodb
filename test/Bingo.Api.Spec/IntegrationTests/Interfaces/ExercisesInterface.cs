using Bingo.Repository.Entities;
using RestEase;
using System.Threading.Tasks;

namespace Bingo.Specification.IntegrationTests.Interfaces
{
    [AllowAnyStatusCode]
    public interface IExercisesInterface
    {
        [Get("api/exercises")]
        Task<Response<Exercise>> GetExercises();
    }
}
