using Bingo.Api.Models;
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

        [HttpGet(Name = "Get All Exercises")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Exercise>))]
        public async Task<IActionResult> GetManyAsync()
        {
            var allExercises = await _exercisesService.ReadAllAsync();

            return Ok(allExercises);
        }
        
        [HttpGet("{exerciseId}", Name = "Get Exercise by Id")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Exercise))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string exerciseId)
        {
            var exercise = await _exercisesService.ReadOneAsync(exerciseId);
            
            if (exercise == null)
                return NotFound();
            
            return Ok(exercise);
        }
        
        [HttpPost(Name = "Post Exercise")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Exercise))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostOneAsync([FromBody] PostExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedExercise = await _exercisesService.CreateOneAsync(exerciseDto.ToExercise());

            if (postedExercise == null)
                return BadRequest();

            return StatusCode(201, postedExercise);
        }

        [HttpDelete("{exerciseId}", Name = "Delete Exercise")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOneByIdAsync(string exerciseId)
        {
            await _exercisesService.DeleteOneAsync(exerciseId);
            
            return NoContent();
        }
    }
}
