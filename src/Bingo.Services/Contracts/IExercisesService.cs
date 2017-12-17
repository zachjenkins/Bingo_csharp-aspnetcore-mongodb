using Bingo.Domain.Entities;
using System.Collections.Generic;

namespace Bingo.Services.Contracts
{
    public interface IExercisesService
    {
        Exercise FindExerciseById(string id);
        IEnumerable<Exercise> FindAllExercises();
        Exercise CreateExercise(Exercise exerciseToCreate);
        bool RemoveExercise(string id);
    }
}
