using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Contracts
{
    public interface IActivationsRepository
    {
        Task<Activation> ReadOneAsync(string id);
        Task<IEnumerable<Activation>> ReadAllAsync();
        Task<Activation> CreateOneAsync(Activation Activation);
        Task<Activation> DeleteOneAsync(string id);
    }
}
