using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Contracts
{
    public interface IExercisesService
    {
        Task<Exercise> FindExercise(string id);
        Task<IEnumerable<Exercise>> FindExercises();
        Task<Exercise> CreateExercise(Exercise exerciseToCreate);
        Task<Exercise> DeleteExercise(string id);

        Task<Activation> FindActivation(string exerciseId, string activationId);
        Task<IEnumerable<Activation>> FindActivations(string exerciseId);
    }
}
