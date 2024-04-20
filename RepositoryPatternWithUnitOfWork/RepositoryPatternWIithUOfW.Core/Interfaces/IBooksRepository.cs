using RepositoryPatternWithUOfW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.Core.Interfaces
{
    public interface IBooksRepository : IBaseRepository<Book> 
    {


        IEnumerable<Book> CustomMethodForBooks();
        
    }
}
