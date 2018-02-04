using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Services
{
    public class MusclesService : IMusclesService
    {
        private readonly IMusclesRepository _musclesRepository;

        public MusclesService(IMusclesRepository musclesRepository)
        {
            _musclesRepository = musclesRepository;
        }

        public async Task<Muscle> ReadOneAsync(string id)
        {
            var result = await _musclesRepository.ReadOneAsync(id);
            return result;
        }

        public async Task<IEnumerable<Muscle>> ReadAllAsync()
        {
            var results = await _musclesRepository.ReadAllAsync();
            return results;
        }

        public async Task<Muscle> CreateOneAsync(Muscle muscleToCreate)
        {
            var createdExercise = await _musclesRepository.CreateOneAsync(muscleToCreate);
            
            return createdExercise;
        }

        public async Task<Muscle> DeleteOneAsync(string id)
        {
            return await _musclesRepository.DeleteOneAsync(id);
        }
    }
}
