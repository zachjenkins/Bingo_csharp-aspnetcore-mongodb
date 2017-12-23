using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Services.Contracts
{
    public interface IExercisesService
    {
        Task<Exercise> ReadOneAsync(string id);
        Task<IEnumerable<Exercise>> ReadAllAsync();
        Task<Exercise> CreateOneAsync(Exercise exerciseToCreate);
        Task<Exercise> DeleteOneAsync(string id);
    }
}
