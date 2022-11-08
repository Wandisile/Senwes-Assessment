using SenwesAssignment_API.Models.Employees;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenwesAssignment_API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAsync();
        Task<Employee> GetAsyncById(int id);
        Task<IEnumerable<Employee>> GetAsyncByJoiningDate();
        Task<IEnumerable<Employee>> GetAsyncByAge();
        Task<IEnumerable<Employee>> GetAsyncHighestPaid();
        Task<Employee> SearchEmployee(string name, string surname, string city);
        Task<IEnumerable<Salaries>> GetAsyncByFirstName(); //treasure
        Task<IEnumerable<Cities>> GetAsyncCities();
    }
}
