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
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
