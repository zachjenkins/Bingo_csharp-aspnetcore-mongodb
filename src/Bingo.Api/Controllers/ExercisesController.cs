using Bingo.Api.Models;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManyAsync()
        {
            var allExercises = await _exercisesService.ReadAllAsync();

            return Ok(allExercises);
        }
        
        [HttpGet("{id}", Name = "Get Exercise by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string id)
        {
            var exercise = await _exercisesService.ReadOneAsync(id);
            
            if (exercise == null)
                return NotFound();
            
            return Ok(exercise);
        }

        [HttpPost(Name = "Post Exercise")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostOneAsync([FromBody] PostExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedExercise = await _exercisesService.CreateOneAsync(exerciseDto.ToExercise());

            if (postedExercise == null)
                return BadRequest();

            return StatusCode(201, postedExercise);
        }

        [HttpDelete("{id}", Name = "Delete Exercise")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOneByIdAsync(string id)
        {
            await _exercisesService.DeleteOneAsync(id);
            
            return NoContent();
        }
    }
}
