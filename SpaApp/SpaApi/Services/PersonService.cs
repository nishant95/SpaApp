using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaData;
using SpaData.Models;

namespace SpaApi.Services
{
    public class PersonService : BaseService, IPersonService
    {
        #region Constructors

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a person and return the status(IsAdded?).
        /// </summary>
        /// <param name="person">Person</param>
        /// <returns></returns>
        public bool AddPerson(Person person)
        {
            _unitOfWork.Persons.Add(person);
            _unitOfWork.Complete();
            return true;
        }

        public IQueryable<Person> GetAllPersons()
        {
            return _unitOfWork.Persons.GetAll();
        }

        public Person GetPerson(long id)
        {
            return _unitOfWork.Persons.Get(id);
        }

        #endregion
    }
}
