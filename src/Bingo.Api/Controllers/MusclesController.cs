using Bingo.Api.Models;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bingo.Api.Controllers
{
    [Route("api/[controller]")]
    public class MusclesController : Controller
    {
        private readonly IMusclesService _musclesService;

        public MusclesController(IMusclesService musclesService)
        {
            _musclesService = musclesService ?? throw new ArgumentNullException(nameof(musclesService));
        }

        [HttpGet(Name = "Get All Muscles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManyAsync()
        {
            var allMuscles = await _musclesService.ReadAllAsync();

            return Ok(allMuscles);
        }
        
        [HttpGet("{id}", Name = "Get Muscle by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string id)
        {
            var muscle = await _musclesService.ReadOneAsync(id);
            
            if (muscle == null)
                return NotFound();
            
            return Ok(muscle);
        }

        [HttpPost(Name = "Post Muscle")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostOneAsync([FromBody] PostMuscleDto muscleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedMuscle = await _musclesService.CreateOneAsync(muscleDto.ToMuscle());

            if (postedMuscle == null)
                return BadRequest();

            return StatusCode(201, postedMuscle);
        }

        [HttpDelete("{id}", Name = "Delete Muscle")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOneByIdAsync(string id)
        {
            await _musclesService.DeleteOneAsync(id);
            
            return NoContent();
        }
    }
}
