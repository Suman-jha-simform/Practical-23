using Business_Access_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Department
{
    public class OnSite : IDepartment
    {
        public decimal CalculateOvertime(int hours)
        {
            return (hours * 400);
        }
    }
}
