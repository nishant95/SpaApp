using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaData;

namespace SpaApi.Services
{
    public class PersonService : BaseService
    {
        #region Constructors

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion
    }
}
