using Practical_23.Model;

namespace Practical_23.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        public Task CreateEmployeeAsync(Employee employee);
        public Task DeleteEmployeeAsync(Employee employee);
        public Task<bool> SaveChangesAsync();
    }
}
