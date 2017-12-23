using Bingo.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Contracts
{
    public interface IExercisesRepository
    {
        Task<Exercise> ReadOneAsync(string id);
        Task<IEnumerable<Exercise>> ReadAllAsync();
        Task<Exercise> CreateOneAsync(Exercise exercise);
        Task<Exercise> DeleteOneAsync(string id);
    }
}
