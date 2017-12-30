using Bingo.Repository.Contracts;
using Bingo.Repository.Entities;
using Bingo.Repository.Repositories;
using Bingo.Services.Contracts;
using Bingo.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mongo2Go;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bingo.Specification.IntegrationTests
{
    public class TestStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Services
            services.AddTransient<IExercisesService, ExercisesService>();

            // Register Repositories
            services.AddTransient<IExercisesRepository, ExercisesRepository>();

            // Settings
            services.AddMvc();
            services.AddMvc().AddJsonOptions(opt => opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
            JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }

    }
}
