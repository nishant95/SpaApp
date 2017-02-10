using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaApi.Services;
using SpaData.Models;
using SpaApi.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpaApi
{
    [Authorize]
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

        [Authorize("spaUser")]
        // GET: api/person
        [HttpGet]
        public IEnumerable<PersonViewModel> Get()
        {
            List<PersonViewModel> personViewModels = new List<PersonViewModel>();
            List<Person> persons = _personService.GetAllPersons().ToList();
            foreach(var person in persons)
            {
                personViewModels.Add(_mapper.Map<PersonViewModel>(person));
            }
            return personViewModels;
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public PersonViewModel Get(long id)
        {
            return _mapper.Map<PersonViewModel>(_personService.GetPerson(id));
        }

        [Authorize("spaAdmin")]
        // POST api/person
        [HttpPost]
        public ActionResult Post([FromBody]PersonViewModel personViewModel)
        {
            if(!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values
                    .SelectMany(v => v.Errors);
                return new JsonResult(new
                {
                    Status = false,
                    Message = "Unsuccessful",
                    Errors = allErrors
                });
            }

            Person person = _mapper.Map<PersonViewModel, Person>(personViewModel);
            _personService.AddPerson(person);
            return new JsonResult(new { Status = true, Message = "Successful" });
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]PersonViewModel personViewModel)
        {
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }

        #endregion
    }
}
