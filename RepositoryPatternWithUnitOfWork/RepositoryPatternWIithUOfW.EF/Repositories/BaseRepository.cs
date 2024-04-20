using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOfW.Core.Consts;
using RepositoryPatternWithUOfW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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


        public async Task<T> GetByIdAsync(int id) 
        {            
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();  
        }
        public T Find(Expression<Func<T,bool>> match ,string[] includes = null)
        {

            IQueryable<T> query = _context.Set<T>();    

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);

                }
            }
            return query.SingleOrDefault(match);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match, string[] includes )// the Include Part is not working here 
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query.Include(include);
                }
            }
            return query.Where(match).ToList();
        }


        public IEnumerable<T> FindAll(Expression<Func<T,bool>> match , int take, int skip)
        {
            return _context.Set<T>().Where(match).Skip(skip).Take(take).ToList();
        }


      public  IEnumerable<T> FindAll(Expression<Func<T, bool>> match, int? take, int? skip,
    Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(match);
            if (take.HasValue)
                query = query.Take((int)take);
            if (skip.HasValue)
                query = query.Skip((int)skip);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                {
                    query = query.OrderBy(orderBy);
                }
                else
                {
                    query = query.OrderByDescending(orderBy);
                }
            }
            return query.ToList();

        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity; 
        }

        public IEnumerable<T> AddRange(IEnumerable<T> enttities)
        {

            _context.Set<T>().AddRange(enttities);
            _context.SaveChanges();
            /*  foreach(T entity in enttities)
              {
                  _context.Set<T>().Add(entity);
                  _context.SaveChanges();
              }*/

            return enttities;
        }
    }
}
