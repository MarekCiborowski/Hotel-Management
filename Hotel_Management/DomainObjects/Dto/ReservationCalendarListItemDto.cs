using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class ReservationCalendarListItemDto
    {
        public int Sr { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
