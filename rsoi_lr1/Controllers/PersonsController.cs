using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rsoi_lr1.Database;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http;
using rsoi_lr1.Dto;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace rsoi_lr1.Controllers
{
    [ApiController]
    [Route("api/v1/persons")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonsContext _personsContext;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(PersonsContext personsContext, ILogger<PersonsController> logger)
        {
            _logger = logger;
            _personsContext = personsContext;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PersonDto[]))]
        public async Task<IActionResult> Get()
        {
            var entities = await _personsContext.Persons
                .AsNoTracking()
                .ToListAsync();

            var persons = entities.Select(entity => Convert(entity)).ToArray();
            return Ok(persons);
        }
        
        [HttpGet("{personId:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PersonDto))]
        public async Task<IActionResult> Get(int personId)
        {
            var person = await _personsContext.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personId);
            if (person == null)
                return NotFound();
            return Ok(Convert(person));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDto person)
        {
            var entity = Convert(person);
            await _personsContext.Persons.AddAsync(entity);
            await _personsContext.SaveChangesAsync();

            return Created($"api/v1/persons/{entity.Id}", null);
        }

        [HttpPatch("{personId:int}")]
        public async Task<IActionResult> Patch(int personId, PersonDto patchPersonDto)
        {
            var person = await _personsContext.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (person == null)
                return NotFound();
            person.Name = patchPersonDto.Name!=null ? patchPersonDto.Name : person.Name;
            person.Age = patchPersonDto.Age != null ? patchPersonDto.Age : person.Age;
            person.Address = patchPersonDto.Address != null ? patchPersonDto.Address : person.Address;
            person.Job = patchPersonDto.Job != null ? patchPersonDto.Job : person.Job;
            await _personsContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{personId:int}")]
        public async Task<IActionResult> Delete(int personId)
        {
            var person = await _personsContext.Persons.FirstOrDefaultAsync(x => x.Id == personId);
            if (person == null)
                return NotFound();
            _personsContext.Persons.Remove(person);
            await _personsContext.SaveChangesAsync();
            return NoContent();
        }

        private static PersonDto Convert(PersonEntity entity)
        {
            return new PersonDto()
            {
                Id = entity.Id,
                Address = entity.Address,
                Age = entity.Age,
                Job = entity.Job,
                Name = entity.Name
            };
        }

        private static PersonEntity Convert(PersonDto entity)
        {
            return new PersonEntity()
            {
                Id = entity.Id,
                Address = entity.Address,
                Age = entity.Age,
                Job = entity.Job,
                Name = entity.Name
            };
        }
    }
}
