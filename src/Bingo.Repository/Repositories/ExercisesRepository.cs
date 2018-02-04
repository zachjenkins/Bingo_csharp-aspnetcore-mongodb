using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        private readonly IMongoCollection<Exercise> _collection;

        public ExercisesRepository(IMongoCollection<Exercise> collection)
        {
            _collection = collection;
        }

        public async Task<Exercise> ReadOneAsync(string id)
        {
            if (id.IsNot24BitHex())
                return null;

            var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
            var result = await _collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Exercise>> ReadAllAsync()
        {
            var filter = Builders<Exercise>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return await results.ToListAsync();
        }
        
        public async Task<Exercise> CreateOneAsync(Exercise exercise)
        {
            await _collection.InsertOneAsync(exercise);

            return (exercise.Id == null) ? null : exercise;
        }

        public async Task<Exercise> DeleteOneAsync(string id)
        {
            var exerciseToDelete = await ReadOneAsync(id);

            if (exerciseToDelete == null)
                return null;
                
            var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
            await _collection.DeleteOneAsync(filter);

            return exerciseToDelete;
        }
    }
}
