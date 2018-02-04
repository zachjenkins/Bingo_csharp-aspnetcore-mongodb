using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Contracts
{
    public interface IActivationsService
    {
        Task<Activation> ReadOneAsync(string id);
        Task<IEnumerable<Activation>> ReadAllAsync();
        Task<Activation> CreateOneAsync(Activation ActivationToCreate);
        Task<Activation> DeleteOneAsync(string id);
    }
}
