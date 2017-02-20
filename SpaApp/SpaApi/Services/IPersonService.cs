using SpaData.Models;
using System.Linq;

namespace SpaApi.Services
{
    public interface IPersonService
    {
        bool AddPerson(Person person);
        IQueryable<Person> GetAllPersons();
        Person GetPerson(long id);
    }
}