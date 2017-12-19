using Bingo.Domain.Entities;
using Bingo.Repository.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bingo.Repository.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Exercise> _collection;

        public ExercisesRepository()
        {
            _client = new MongoClient(@"mongodb://localhost:27017?connectionTimeout=30000");
            _database = _client.GetDatabase("bingo");
            _collection = _database.GetCollection<Exercise>("exercises");
        }

        public async Task<Exercise> SelectExerciseById(string id)
        {
            try
            {
                var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
                var result = await _collection.FindAsync(filter);
                return result.FirstOrDefault();
            }
            catch (MongoWriteException e)
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
            await _collection.InsertOneAsync(exercise);
            return exercise;
        }

        public async Task<bool> DeleteExercise(string id)
        {
            try
            {
                var filter = Builders<Exercise>.Filter.Eq(ex => ex.Id, id);
                var result = await _collection.DeleteOneAsync(filter);
                return (result.DeletedCount > 0) ? true : false;
            }
            catch (FormatException e)
            {
                return true;
            }
        }
    }
}
