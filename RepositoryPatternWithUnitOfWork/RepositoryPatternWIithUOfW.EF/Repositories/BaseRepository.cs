using RepositoryPatternWithUOfW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext  context)
        {
            _context = context;
        }
       
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
