using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Repository.Repositories
{
    public class ActivationsRepository : IActivationsRepository
    {
        private readonly IMongoCollection<Activation> _collection;

        public ActivationsRepository(IMongoCollection<Activation> collection)
        {
            _collection = collection;
        }

        public async Task<Activation> ReadOneAsync(string id)
        {
            if (id.IsNot24BitHex())
                return null;

            var filter = Builders<Activation>.Filter.Eq(ex => ex.Id, id);
            var result = await _collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Activation>> ReadAllAsync()
        {
            var filter = Builders<Activation>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return await results.ToListAsync();
        }
        
        public async Task<Activation> CreateOneAsync(Activation activation)
        {
            await _collection.InsertOneAsync(activation);

            return (activation.Id == null) ? null : activation;
        }

        public async Task<Activation> DeleteOneAsync(string id)
        {
            var activationToDelete = await ReadOneAsync(id);

            if (activationToDelete == null)
                return null;
                
            var filter = Builders<Activation>.Filter.Eq(ex => ex.Id, id);
            await _collection.DeleteOneAsync(filter);

            return activationToDelete;
        }
    }
}
