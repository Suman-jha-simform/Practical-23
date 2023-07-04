using Business_Access_Layer.Interface;
using Business_Access_Layer.Department;
using Business_Access_Layer.Factory;

namespace Business_Access_Layer.AbstractFactory
{
    public class AdminAbstractFactory : IndoorDepartmentFactory
    {
        public override IDepartment CreateDepartment()
        {
            return new Admin();
        }
    }
}
