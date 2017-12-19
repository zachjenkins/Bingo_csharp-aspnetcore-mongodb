using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
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
            var result = await _exercisesRepository.SelectExerciseById(id);
            return result;
        }

        public async Task<IEnumerable<Exercise>> FindAllExercises()
        {
            var results = await _exercisesRepository.SelectAllExercises();
            return results;
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
