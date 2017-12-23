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

        public async Task<Exercise> ReadOneAsync(string id)
        {
            var result = await _exercisesRepository.ReadOneAsync(id);
            return result;
        }

        public async Task<IEnumerable<Exercise>> ReadAllAsync()
        {
            var results = await _exercisesRepository.ReadAllAsync();
            return results;
        }

        public async Task<Exercise> CreateOneAsync(Exercise exerciseToCreate)
        {
            var createdExercise = await _exercisesRepository.CreateOneAsync(exerciseToCreate);
            
            return createdExercise;
        }

        public async Task<Exercise> DeleteOneAsync(string id)
        {
            return await _exercisesRepository.DeleteOneAsync(id);
        }
    }
}
