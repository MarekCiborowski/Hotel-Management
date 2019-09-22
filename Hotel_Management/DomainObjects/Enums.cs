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
        }

        public enum ReservationStatusEnum
        {
            Canceled = 0,
            Confirmed = 1,
            Closed = 2,
            AwaitingConfirmation = 3
        }
    }
}
