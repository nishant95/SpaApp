#region Namespaces

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SpaApi.Services;
using SpaData.Models;
using SpaApi.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
#endregion

namespace SpaApi
{
    /// <summary>
    /// Manage Persons
    /// </summary>
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

        // GET: api/person
        /// <summary>
        /// Returns all persons.
        /// </summary>
        /// <returns></returns>
        [Authorize("spaUser")]
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
        /// <summary>
        /// Get a person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public PersonViewModel Get(long id)
        {
            return _mapper.Map<PersonViewModel>(_personService.GetPerson(id));
        }

        // POST api/person
        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="personViewModel"></param>
        /// <returns></returns>
        [Authorize("spaAdmin")]
        [HttpPost]
        public ActionResult Post([FromBody]PersonViewModel personViewModel)
        {
            var claims = from c in User.Claims select new { c.Type, c.Value };

            if (!ModelState.IsValid)
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
        /// <summary>
        /// Update a person using the id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="personViewModel"></param>
        [HttpPut("{id}")]
        public void Put(long id, [FromBody]PersonViewModel personViewModel)
        {
        }

        // DELETE api/person/5
        /// <summary>
        /// Delete a person using id.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(long id)
        {
        }

        #endregion
    }
}
