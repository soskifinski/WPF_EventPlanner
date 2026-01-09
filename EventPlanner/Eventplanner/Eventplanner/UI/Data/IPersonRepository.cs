using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Person FindById(int id);
        Task<List<Person>> GetAllPersonsAsync();
        Task<List<Person>> GetPersonsLookupAsync();
        Task<List<Person>> GetAllEmpoyeesAsync();
        Task SavePersonAndAddress(Person person, Address address);
    }
}