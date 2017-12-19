using Bingo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Contracts
{
    public interface IExercisesService
    {
        Task<Exercise> FindExerciseById(string id);
        Task<IEnumerable<Exercise>> FindAllExercises();
        Task<Exercise> CreateExercise(Exercise exerciseToCreate);
        Task<bool> RemoveExercise(string id);
    }
}
