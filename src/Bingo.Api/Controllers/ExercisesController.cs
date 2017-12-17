using Bingo.Domain.Entities;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Bingo.Api.Models;
using Microsoft.AspNetCore.Http;

namespace Bingo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ExercisesController : Controller
    {
        private readonly IExercisesService _exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            _exercisesService = exercisesService;
        }

        [HttpGet(Name = "Get All Exercises")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllExercises()
        {
            var exercises = _exercisesService.FindAllExercises();

            if (exercises == null)
                return Ok(new List<Exercise>());

            return Ok(exercises);
        }
       
        [HttpGet("{id}", Name = "Get Exercise by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetExerciseById(string id)
        {
            var exercise = _exercisesService.FindExerciseById(id);
            
            if (exercise == null)
                return NotFound();

            return Ok(exercise);
        }

        [HttpPost(Name = "Post Exercise")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult PostExercise([FromBody] PostExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedExercise = _exercisesService.CreateExercise(exerciseDto.ToExercise());

            return StatusCode(201, postedExercise);
        }

        [HttpDelete("{id}", Name = "Delete Exercise")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteExercise(string id)
        {
            var result = _exercisesService.RemoveExercise(id);

            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
