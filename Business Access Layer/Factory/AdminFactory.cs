using Business_Access_Layer.Interface;
using Business_Access_Layer.Department;

namespace Business_Access_Layer.Factory
{
    public class AdminFactory : IDepartmentFactory
    {
        public IDepartment CreateDepartment()
        {
            return new Admin();
        }
    }
}
