using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Contracts
{
    public interface IMusclesService
    {
        Task<Muscle> ReadOneAsync(string id);
        Task<IEnumerable<Muscle>> ReadAllAsync();
        Task<Muscle> CreateOneAsync(Muscle muscleToCreate);
        Task<Muscle> DeleteOneAsync(string id);
    }
}
