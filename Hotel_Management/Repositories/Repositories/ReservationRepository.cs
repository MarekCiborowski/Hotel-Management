using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ReservationRepository
    {
        private DatabaseContext db;

        public ReservationRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public AddReservation (int roomId, )


    }
}
