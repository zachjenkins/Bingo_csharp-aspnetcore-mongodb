using Bingo.Domain.Entities;
using System.Collections.Generic;

namespace Bingo.Repository.Contracts
{
    public interface IExercisesRepository
    {
        Exercise SelectExerciseById(string id);
        IEnumerable<Exercise> SelectAllExercises();
        Exercise InsertExercise(Exercise exercise);
        bool DeleteExercise(string id);
    }
}
