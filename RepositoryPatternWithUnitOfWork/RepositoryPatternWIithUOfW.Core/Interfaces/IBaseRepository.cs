using RepositoryPatternWithUOfW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.Core.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {

        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> criteria , string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool> > criteria, int take, int skip);
    
        IEnumerable<T> FindAll(Expression<Func<T,bool>> criteria , int? take, int? skip , 
            Expression<Func<T, object>> orderBy =null , string orderByDirection = OrderBy.Ascending);

        T Add(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> enttities);

        T Update (T entity);    
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> enttities);
        void Attach(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);  

    }
}
