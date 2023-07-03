﻿using Business_Access_Layer.Department;
using Business_Access_Layer.Interface;

namespace Business_Access_Layer.Factory
{
    public class ITFactory : IDepartmentFactory
    {
        public IDepartment CreateDepartment()
        {
            return new IT();
        }
    }
}
