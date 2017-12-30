using System;
using System.Net.Http;
using System.Reflection;
using Bingo.Api;
using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Repository.Repositories;
using Bingo.Services.Contracts;
using Bingo.Services.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using Xunit;
using Bingo.Specification.IntegrationTests.Interfaces;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestEase;

namespace Bingo.Specification.IntegrationTests
{
    public class BaseHttpTest
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; set; }
        protected MongoClient MongoClient { get; set; }

        protected virtual string Environment => "Development";
        
        internal static IMongoCollection<Exercise> ExercisesCollection;

        public BaseHttpTest()
        {
            Server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .ConfigureServices(ConfigureServices));

            Client = Server.CreateClient(); 
        }

        /*[Fact]
        public async void Test1()
        {
            var stuff = await Client.GetAsync("/api/exercises");

            var body = await stuff.Content.ReadAsStringAsync();
            
        }*/

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            var runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(runner.ConnectionString);
            
            IMongoDatabase database = MongoClient.GetDatabase("BingoTest");
            ExercisesCollection = database.GetCollection<Exercise>("exercises");
            
            services.AddSingleton<IMongoCollection<Exercise>>(ExercisesCollection);
        }

        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
        
        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                    Server.Dispose();
                    MongoClient.DropDatabase("BingoTest");
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
