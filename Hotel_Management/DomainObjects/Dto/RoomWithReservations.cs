using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class RoomWithReservations
    {
        public Room Room { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
