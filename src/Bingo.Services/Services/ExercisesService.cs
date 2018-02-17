using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Services
{
    public class ExercisesService : IExercisesService
    {
        private readonly IActivationsRepository _activationsRepository;
        private readonly IExercisesRepository _exercisesRepository;

        public ExercisesService(IActivationsRepository activationsRepository, IExercisesRepository exercisesRepository)
        {
            _activationsRepository = activationsRepository;
            _exercisesRepository = exercisesRepository;
        }

        #region Find
        
        public async Task<Exercise> FindExercise(string id)
        {
            var result = await _exercisesRepository.ReadOneAsync(id);
            return result;
        }

        public async Task<IEnumerable<Exercise>> FindExercises()
        {
            var results = await _exercisesRepository.ReadAllAsync();
            return results;
        }

        public async Task<Activation> FindActivation(string exerciseId, string activationId)
        {
            var exercise = await _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            return await _activationsRepository.ReadOneAsync(activationId);
        }
        
        public async Task<IEnumerable<Activation>> FindActivations(string exerciseId)
        {
            var exercise = _exercisesRepository.ReadOneAsync(exerciseId);

            if (exercise == null)
                return null;

            var results = await _activationsRepository.ReadManyAsync(exerciseId);
            return results;
        }

        #endregion
        
        #region Create
        
        public async Task<Exercise> CreateExercise(Exercise exerciseToCreate)
        {
            var createdExercise = await _exercisesRepository.CreateOneAsync(exerciseToCreate);
            
            return createdExercise;
        }

        #endregion
        
        #region Delete
        
        public async Task<Exercise> DeleteExercise(string id)
        {
            return await _exercisesRepository.DeleteOneAsync(id);
        }
        
        #endregion
    }
}
