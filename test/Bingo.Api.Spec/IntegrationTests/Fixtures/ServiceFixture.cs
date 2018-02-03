using Bingo.Repository.Entities;
using Bingo.Specification.IntegrationTests.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Bingo.Specification.IntegrationTests.Fixtures
{
    public class ServiceFixture : IDisposable
    {
        public ServiceFixture()
        {
            InitializeMongo();
            LoadTestData();

            InitializeWebHost();

            InitializeApiInterface();
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("BingoTestDatabase");
            Runner.Dispose();
        }

        public void InitializeMongo()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);

            MongoClient.DropDatabase("BingoTestDatabase");

            Database = MongoClient.GetDatabase("BingoTestDatabase");
            ExercisesCollection = Database.GetCollection<Exercise>("exercises");
        }

        public void InitializeWebHost()
        {
            HttpServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               .ConfigureServices(ConfigureServices));

            HttpClient = HttpServer.CreateClient();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ExercisesCollection);
        }

        public void InitializeApiInterface()
        {
            Api = RestClient.For<IExercisesApi>(HttpClient);
        }

        public void LoadTestData()
        {
            IEnumerable<Exercise> exercises = TestData.Exercises.GetAllExercisesForCollection();

            ExercisesCollection.InsertMany(exercises);

        }

        public IExercisesApi Api { get; set; }
        public IMongoCollection<Exercise> ExercisesCollection { get; set; }

        private MongoDbRunner Runner { get; set; }
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase Database { get; set; }

        private TestServer HttpServer { get; set; }
        private HttpClient HttpClient { get; set; }

    }
}
