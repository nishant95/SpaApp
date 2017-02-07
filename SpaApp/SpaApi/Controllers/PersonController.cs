using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaApi.Services;
using SpaData.Models;
using SpaDto.SpaDtos;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaApi
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        #region Private Field and Constants

        IPersonService _personService;
        IMapper _mapper;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for PersonController
        /// </summary>
        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        #endregion

        #region Methods

        // GET: api/persons
        [HttpGet]
        public IEnumerable<PersonDto> Get()
        {
            List<PersonDto> personDtos = new List<PersonDto>();
            List<Person> persons = _personService.GetAllPersons().ToList();
            foreach(var person in persons)
            {
                personDtos.Add(_mapper.Map<PersonDto>(person));
            }
            return personDtos;
        }

        // GET api/persons/5
        [HttpGet("{id}")]
        public PersonDto Get(long id)
        {
            return _mapper.Map<PersonDto>(_personService.GetPerson(id));
        }

        // POST api/persons
        [HttpPost]
        public ActionResult Post([FromBody]PersonDto personDto)
        {
            Person person = _mapper.Map<PersonDto, Person>(personDto);
            _personService.AddPerson(person);
            return new JsonResult(new { Status = true, Message = "Successful" });
        }

        // PUT api/persons/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]PersonDto personDto)
        {
        }

        // DELETE api/persons/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }

        #endregion
    }
}
