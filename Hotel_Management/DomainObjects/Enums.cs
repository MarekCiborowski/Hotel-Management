using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class Enums
    {
        public enum RolesEnum
        {
            Admin = 0,
            RegularUser = 1,
            CompanyUser = 2
        }

        public enum StatusEnum
        {
            Canceled = 0,
            Confirmed = 1,
            Closed = 2,
            AwaitingPayment = 3
        }
    }
}
