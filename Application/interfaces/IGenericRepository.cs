using Application.parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region sync methods

        IEnumerable<T> getAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties
        ) => null;

        T get(int id) => default(T);

        void add(T entity) { }

        void remove(int id) { }

        void remove(T entity) { }

        #endregion


        #region async methods
        Task<List<T>> getAllAsync(
            Dictionary<string, int> additionalProps = null,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            RequestParameter pagination = null,
            params Expression<Func<T, object>>[] includes
            );

        Task<T> getByIdAsync(int id);

        Task<T> addAsync(T entity);

        Task<List<T>> addMassiveAsync(List<T> entities) => default;

        Task<T> updateAsync(int id, T entity);

        Task<T> removeAsync(int id);


        #endregion


    }
}
