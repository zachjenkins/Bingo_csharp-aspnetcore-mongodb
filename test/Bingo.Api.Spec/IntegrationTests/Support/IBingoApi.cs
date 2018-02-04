using Bingo.Api.Models;
using Bingo.Repository.Entities;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Specification.IntegrationTests.Support
{
    [AllowAnyStatusCode]
    public interface IBingoApi
    {
        #region Activations

        [Get("api/activations")]
        Task<Response<List<Activation>>> GetActivations();

        [Get("api/activations/{id}")]
        Task<Response<Activation>> GetActivationById([Path] string id);

        [Post("api/activations")]
        Task<Response<Activation>> PostActivation([Body] PostActivationDto postDto);

        [Delete("api/activations/{id}")]
        Task<Response<string>> DeleteActivationById([Path] string id);

        #endregion

        #region Exercises

        [Get("api/exercises")]
        Task<Response<List<Exercise>>> GetExercises();

        [Get("api/exercises/{id}")]
        Task<Response<Exercise>> GetExerciseById([Path] string id);

        [Post("api/exercises")]
        Task<Response<Exercise>> PostExercise([Body] PostExerciseDto postDto);

        [Delete("api/exercises/{id}")]
        Task<Response<string>> DeleteExerciseById([Path] string id);

        #endregion

        #region Muscles Controller

        [Get("api/muscles")]
        Task<Response<List<Muscle>>> GetMuscles();

        [Get("api/muscles/{id}")]
        Task<Response<Muscle>> GetMuscleById([Path] string id);

        [Post("api/muscles")]
        Task<Response<Muscle>> PostMuscle([Body] PostMuscleDto postDto);

        [Delete("api/muscles/{id}")]
        Task<Response<string>> DeleteMuscleById([Path] string id);

        #endregion
    }
}
