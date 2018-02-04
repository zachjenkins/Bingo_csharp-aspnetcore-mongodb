using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Contracts
{
    public interface IMusclesRepository
    {
        Task<Muscle> ReadOneAsync(string id);
        Task<IEnumerable<Muscle>> ReadAllAsync();
        Task<Muscle> CreateOneAsync(Muscle muscle);
        Task<Muscle> DeleteOneAsync(string id);
    }
}
