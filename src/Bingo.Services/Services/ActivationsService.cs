using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Services
{
    public class ActivationsService : IActivationsService
    {
        private readonly IActivationsRepository _musclesRepository;

        public ActivationsService(IActivationsRepository musclesRepository)
        {
            _musclesRepository = musclesRepository;
        }

        public async Task<Activation> ReadOneAsync(string id)
        {
            var result = await _musclesRepository.ReadOneAsync(id);
            return result;
        }

        public async Task<IEnumerable<Activation>> ReadAllAsync()
        {
            var results = await _musclesRepository.ReadAllAsync();
            return results;
        }

        public async Task<Activation> CreateOneAsync(Activation muscleToCreate)
        {
            var createdExercise = await _musclesRepository.CreateOneAsync(muscleToCreate);
            
            return createdExercise;
        }

        public async Task<Activation> DeleteOneAsync(string id)
        {
            return await _musclesRepository.DeleteOneAsync(id);
        }
    }
}
