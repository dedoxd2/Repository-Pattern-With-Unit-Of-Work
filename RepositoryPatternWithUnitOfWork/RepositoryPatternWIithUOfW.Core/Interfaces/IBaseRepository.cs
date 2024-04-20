using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOfW.Core.Interfaces
{
    public interface IBaseRepository <T> where T : class
    {

        T GetById(int id);


    }
}
