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

        public ExercisesRepository()//IMongoCollection<Exercise> collection)
        {
            //_collection = collection;

            IMongoClient client = new MongoClient(@"mongodb://localhost:27017?connectionTimeout=30000");
            var database = client.GetDatabase("bingo");
            _collection = database.GetCollection<Exercise>("exercises");
        }

        public async Task<Exercise> ReadOneAsync(string id)
        {
            try
            {
                var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
                var result = await _collection.FindAsync(filter);
                return result.FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Exercise>> ReadAllAsync()
        {
            var filter = Builders<Exercise>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return results.ToEnumerable();
        }
        
        public async Task<Exercise> CreateOneAsync(Exercise exercise)
        {
            try
            {
                await _collection.InsertOneAsync(exercise);

                return (exercise.Id == null) ? null : exercise;
            }
            catch
            {
                return null;
            }
            
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
