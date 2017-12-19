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

        public ExercisesRepository()
        {
            IMongoClient client = new MongoClient(@"mongodb://localhost:27017?connectionTimeout=30000");
            var database = client.GetDatabase("bingo");
            _collection = database.GetCollection<Exercise>("exercises");
        }

        public async Task<Exercise> SelectExerciseById(string id)
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

        public async Task<IEnumerable<Exercise>> SelectAllExercises()
        {
            var filter = Builders<Exercise>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return results.ToEnumerable();
        }

        // Be aware that this is not close the the actual mongo implementation
        public async Task<Exercise> InsertExercise(Exercise exercise)
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

        public async Task<bool> DeleteExercise(string id)
        {
            try
            {
                var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
                var result = await _collection.DeleteOneAsync(filter);
                return (result.DeletedCount > 0);
            }
            catch
            {
                return true;
            }
        }
    }
}
