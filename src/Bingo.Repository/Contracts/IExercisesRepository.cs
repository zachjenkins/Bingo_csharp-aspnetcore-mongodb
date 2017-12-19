using Bingo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Contracts
{
    public interface IExercisesRepository
    {
        Task<Exercise> SelectExerciseById(string id);
        Task<IEnumerable<Exercise>> SelectAllExercises();
        Task<Exercise> InsertExercise(Exercise exercise);
        Task<bool> DeleteExercise(string id);
    }
}
