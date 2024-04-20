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
        public T Find(Expression<Func<T,bool>> criteria ,string[] includes = null)
        {

            IQueryable<T> query = _context.Set<T>();    

            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query.Include(include);

                }
            }
            return query.SingleOrDefault(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes )// the Include Part is not working here 
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach(var include in includes)
                {
                    query.Include(include);
                }
            }
            return query.Where(criteria).ToList();
        }


        public IEnumerable<T> FindAll(Expression<Func<T,bool>> criteria , int take, int skip)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }


      public  IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
    Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);

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
            return entity; 
        }

        public IEnumerable<T> AddRange(IEnumerable<T> enttities)
        {

            _context.Set<T>().AddRange(enttities);
            /*  foreach(T entity in enttities)
              {
                  _context.Set<T>().Add(entity);
                  _context.SaveChanges();
              }*/

            return enttities;
        }



        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }


        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
         //   _context.SaveChanges();
        }

        public void DeleteRange(IEnumerable<T> enttities)
        {
            _context.Set<T>().RemoveRange(enttities);
        }


        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
        {

            return _context.Set<T>().Count();
        }
        
        
        public int Count(Expression<Func<T, bool>> criteria)
        {

            return _context.Set<T>().Count(criteria);
        }
     


    }
}
