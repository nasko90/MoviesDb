using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesDatabase.Context
{
    public class MovieDataSession : IDataSession
    {
        private DbContext movieDbContext;

        public MovieDataSession(DbContext dbContext)
        {
            movieDbContext = dbContext;
        }

        public void Dispose()
        {
            movieDbContext.Dispose();
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return movieDbContext.Set<T>();
        }

        public void SaveChanges()
        {
            movieDbContext.SaveChanges();
        }

        public int SqlCommand(string sql, params object[] parameters)
        {
            return movieDbContext.Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}
