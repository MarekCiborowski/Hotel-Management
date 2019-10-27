using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class RoomWithAmenitiesDto
    {
        public Room Room { get; set; }

        public List<Amenity> Amenities { get; set; }
    }
}
