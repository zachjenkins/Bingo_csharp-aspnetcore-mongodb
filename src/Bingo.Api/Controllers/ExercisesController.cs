using Bingo.Api.Models;
using Bingo.Api.Models.Activations;
using Bingo.Services.Contracts;
using Bingo.Repository.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bingo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExercisesController : Controller
    {
        private readonly IExercisesService _exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            _exercisesService = exercisesService ?? throw new ArgumentNullException(nameof(exercisesService));
        }

        #region Get
        
        [HttpGet(Name = "Get All Exercises")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Exercise>))]
        public async Task<IActionResult> GetExercises()
        {
            var allExercises = await _exercisesService.FindExercises();

            return Ok(allExercises);
        }
        
        [HttpGet("{exerciseId}", Name = "Get Exercise by Id")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Exercise))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExercise(string exerciseId)
        {
            var exercise = await _exercisesService.FindExercise(exerciseId);
            
            if (exercise == null)
                return NotFound();
            
            return Ok(exercise);
        }
        
        [HttpGet("{exerciseId}/activations", Name = "Get Activations by Exercise Id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Activation>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivationsForExercise(string exerciseId)
        {
            var response = await _exercisesService.FindActivations(exerciseId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        
        [HttpGet("{exerciseId}/activations/{activationId}", Name = "Get Activation by Id for Exercise")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Activation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivationForExercise(string exerciseId, string activationId)
        {
            var response = await _exercisesService.FindActivation(exerciseId, activationId);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
        
        #endregion
        
        #region Post
        
        [HttpPost(Name = "Post Exercise")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Exercise))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostExercise([FromBody] PostExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedExercise = await _exercisesService.CreateExercise(exerciseDto.ToExercise());

            if (postedExercise == null)
                return BadRequest();

            return StatusCode(201, postedExercise);
        }

        [HttpPost("{exerciseId}/activations", Name = "Post Activation to Exercise")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Activation))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostActivationToExercise(string exerciseId, [FromBody] PostActivationDto activationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedActivation = await _exercisesService.CreateActivation(exerciseId, activationDto.ToActivation());

            if (postedActivation == null)
                return NotFound();

            return StatusCode(201, postedActivation);
        }
        
        #endregion

        #region Delete
        
        [HttpDelete("{exerciseId}", Name = "Delete Exercise")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteExercise(string exerciseId)
        {
            await _exercisesService.DeleteExercise(exerciseId);
            
            return NoContent();
        }
        
        #endregion
    }
}
