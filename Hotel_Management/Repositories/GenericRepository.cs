using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DatabaseContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DatabaseContext _context)
        {
            this.context = _context;
            this.dbSet = context.Set<TEntity>();
        }

    }
}
