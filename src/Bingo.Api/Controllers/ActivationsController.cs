using Bingo.Api.Models;
using Bingo.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bingo.Api.Controllers
{
    [Route("api/[controller]")]
    public class ActivationsController : Controller
    {
        private readonly IActivationsService _activationsService;

        public ActivationsController(IActivationsService activationsService)
        {
            _activationsService = activationsService ?? throw new ArgumentNullException(nameof(activationsService));
        }

        [HttpGet(Name = "Get All Activations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetManyAsync()
        {
            var allActivations = await _activationsService.ReadAllAsync();

            return Ok(allActivations);
        }
        
        [HttpGet("{id}", Name = "Get Activation by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOneByIdAsync(string id)
        {
            var activation = await _activationsService.ReadOneAsync(id);
            
            if (activation == null)
                return NotFound();
            
            return Ok(activation);
        }

        [HttpPost(Name = "Post Activation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostOneAsync([FromBody] PostActivationDto activationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            var postedActivation = await _activationsService.CreateOneAsync(activationDto.ToActivation());

            if (postedActivation == null)
                return BadRequest();

            return StatusCode(201, postedActivation);
        }

        [HttpDelete("{id}", Name = "Delete Activation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOneByIdAsync(string id)
        {
            await _activationsService.DeleteOneAsync(id);
            
            return NoContent();
        }
    }
}
