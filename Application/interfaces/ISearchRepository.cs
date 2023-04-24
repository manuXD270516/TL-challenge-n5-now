using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface ISearchRepository<T>: IGenericRepository<T> where T : class
    {
        Task<List<T>> searchAsync(string term);
    }
}
