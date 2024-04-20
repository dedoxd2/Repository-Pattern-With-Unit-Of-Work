using RepositoryPatternWithUOfW.Core.Interfaces;
using RepositoryPatternWithUOfW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Author> Authors { get; }
        IBaseRepository<Book> Books { get; }


        int Complete();


    }
}
