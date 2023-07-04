using Business_Access_Layer.Department;
using Business_Access_Layer.Interface;


namespace Business_Access_Layer.AbstractFactory
{
    public class SalesAbstractFactory :OutDoorDepartmentFactory
    {
        public override IDepartment CreateDepartment()
        {
            return new Sales();
        }
    }
}
