using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using Bingo.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Services
{
    public class ExercisesService : IExercisesService
    {
        private readonly IExercisesRepository _exercisesRepository;

        public ExercisesService(IExercisesRepository exercisesRepository)
        {
            _exercisesRepository = exercisesRepository;
        }

        public async Task<Exercise> FindExerciseById(string id)
        {
            return await _exercisesRepository.SelectExerciseById(id);
        }

        public async Task<IEnumerable<Exercise>> FindAllExercises()
        {
            return await _exercisesRepository.SelectAllExercises();
        }

        public async Task<Exercise> CreateExercise(Exercise exerciseToCreate)
        {
            var createdExercise = await _exercisesRepository.InsertExercise(exerciseToCreate);

            return createdExercise;
        }

        public async Task<bool> RemoveExercise(string id)
        {
            return await _exercisesRepository.DeleteExercise(id);
        }
    }
}
