using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Interface
{
    public abstract class IAbstractFactory : IDepartmentFactory
    {
        public abstract IDepartment CreateDepartment();
    }
}
