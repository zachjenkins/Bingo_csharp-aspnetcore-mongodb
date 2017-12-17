using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using Bingo.Services.Contracts;
using System.Collections.Generic;

namespace Bingo.Services.Services
{
    public class ExercisesService : IExercisesService
    {
        private readonly IExercisesRepository _exercisesRepository;

        public ExercisesService(IExercisesRepository exercisesRepository)
        {
            _exercisesRepository = exercisesRepository;
        }

        public Exercise FindExerciseById(string id)
        {
            return _exercisesRepository.SelectExerciseById(id);
        }

        public IEnumerable<Exercise> FindAllExercises()
        {
            return _exercisesRepository.SelectAllExercises();
        }

        public Exercise CreateExercise(Exercise exerciseToCreate)
        {
            var createdExercise = _exercisesRepository.InsertExercise(exerciseToCreate);

            return createdExercise;
        }

        public bool RemoveExercise(string id)
        {
            return _exercisesRepository.DeleteExercise(id);
        }
    }
}
