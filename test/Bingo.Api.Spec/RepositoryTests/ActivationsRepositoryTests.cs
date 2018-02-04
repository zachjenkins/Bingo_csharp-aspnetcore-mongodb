using Bingo.Repository.Entities;
using Bingo.Repository.Repositories;
using Bingo.Specification.Helpers;
using Mongo2Go;
using MongoDB.Driver;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bingo.Specification.RepositoryTests
{
    [Trait("Repository", nameof(ActivationsRepositoryTests))]
    public class ActivationsRepositoryTests : IDisposable
    {
        private IMongoDatabase Database { get; }
        private IMongoCollection<Activation> Collection { get; }
        private MongoDbRunner Runner { get; }
        private MongoClient MongoClient { get; }
        private ActivationsRepository ActivationsRepository { get; }

        public ActivationsRepositoryTests()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);
            MongoClient.DropDatabase("ActivationsTestDatabase");
            Database = MongoClient.GetDatabase("ActivationsTestDatabase");
            Collection = Database.GetCollection<Activation>("ActivationsTestCollection");

            ActivationsRepository = new ActivationsRepository(Collection);
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("ActivationsTestDatabase");
            Runner.Dispose();
        }

        #region Task<Activation> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ByValidActivationId_ReturnsExpectedActivation200()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            Collection.InsertOne(expectedActivation);

            // Act
            var result = await ActivationsRepository.ReadOneAsync(expectedActivation.Id);

            // Assert
            result.ShouldBe(expectedActivation);
        }

        [Fact]
        public async void ReadOneAsync_ByNonExistentActivationId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.ReadOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void ReadOneAsync_ByNon24BitHexActivationId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.ReadOneAsync("ThisIsNotA24BitHexStringValue");

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region Task<IEnumerable<Activation>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_WhenDataExists_ReturnsListOfExpectedActivations()
        {
            // Arrange
            var expectedActivations = TestData.Activations.ContractActivations;
            Collection.InsertMany(expectedActivations);

            // Act
            var result = await ActivationsRepository.ReadAllAsync();

            // Assert
            result.ShouldBe(expectedActivations);
        }

        [Fact]
        public async void ReadAllAsync_WhenCollectionIsEmpty_ReturnsListOfExpectedActivations()
        {
            // Act
            var result = await ActivationsRepository.ReadAllAsync();

            // Assert
            result.ShouldBeOfType(typeof(List<Activation>));
            result.ShouldBeEmpty();
        }

        #endregion

        #region Task<Activation> CreateOneAsync(Activation Activation)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedActivationWithId_WhenObjectIsInserted()
        {
            // Arrange
            var ActivationToCreate = TestData.Activations.ActivationWithoutId;

            // Act
            var result = await ActivationsRepository.CreateOneAsync(ActivationToCreate);

            // Assert
            result.Id.ShouldNotBeNull();
            result.ShouldBe(ActivationToCreate);
        }

        #endregion

        #region Task<Activation> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ByValidActivationId_ReturnsDeletedActivation()
        {
            // Arrange
            var Activations = TestData.Activations.ContractActivations;
            var ActivationToDelete = Activations.First();
            Collection.InsertMany(Activations);

            // Act
            var result = await ActivationsRepository.DeleteOneAsync(ActivationToDelete.Id);

            // Assert
            result.ShouldBe(ActivationToDelete);
            Collection.ShouldNotContain(ActivationToDelete);
            Collection.ShouldNotBeEmpty();
        }

        [Fact]
        public async void DeleteOneAsync_ByNonExistentId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.DeleteOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void DeleteOneAsync_ByNon24BitHexId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.DeleteOneAsync("ThisIsNonHex");

            // Asssert
            result.ShouldBeNull();
        }

        #endregion
    }
}