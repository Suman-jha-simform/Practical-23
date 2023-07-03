using Business_Access_Layer.Department;
using Business_Access_Layer.Interface;

namespace Business_Access_Layer.AbstractFactory
{
    public class ITAbstractFactory : IAbstractFactory
    {
        public override IDepartment CreateDepartment()
        {
            return new IT();
        }
    }
}
