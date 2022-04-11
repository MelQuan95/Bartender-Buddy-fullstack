using System;
using Microsoft.AspNetCore.Mvc;
using BartenderBuddy.Repositories;
using BartenderBuddy.Models;

namespace BartenderBuddy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpiritController : ControllerBase
    {
        private readonly ISpiritRepository _spiritRepository;
        public SpiritController(ISpiritRepository spiritRepository)
        {
            _spiritRepository = spiritRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_spiritRepository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var spirits = _spiritRepository.GetSpiritsById(id);
            if (spirits == null)
            {
                return NotFound();
            }
            return Ok(spirits);
        }

        [HttpPost]
        public IActionResult Post(Spirits spirits)
        {
            _spiritRepository.Add(spirits);
            return CreatedAtAction("Get", new { id = spirits.Id }, spirits);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Spirits spirits)
        {
            if (id != spirits.Id)
            {
                return BadRequest();
            }

            _spiritRepository.Update(spirits);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _spiritRepository.Delete(id);
            return NoContent();
        }
    }
}