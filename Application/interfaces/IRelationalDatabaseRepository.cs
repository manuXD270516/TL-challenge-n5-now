using Application.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IRelationalDatabaseRepository<T>: IGenericRepository<T> where T : class
    {
        
        T getFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            params string[] includeProperties
            );

        Task<T> getFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            params string[] includeProperties
            );

        Task<int> countAsync(Expression<Func<T, bool>> filter = null);

        
    }
}
