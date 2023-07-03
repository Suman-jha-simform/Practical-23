using Business_Access_Layer.Department;
using Business_Access_Layer.Interface;


namespace Business_Access_Layer.Factory
{
    public class SalesFactory :IDepartmentFactory
    {
        public IDepartment CreateDepartment()
        {
            return new Sales();
        }
    }
}
