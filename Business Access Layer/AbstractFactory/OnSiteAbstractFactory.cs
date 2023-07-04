using Business_Access_Layer.Department;
using Business_Access_Layer.Interface;


namespace Business_Access_Layer.AbstractFactory
{
    public class OnSiteAbstractFactory : OutDoorDepartmentFactory
    {
        public override IDepartment CreateDepartment()
        {
            return new OnSite();
        }
    }
}
