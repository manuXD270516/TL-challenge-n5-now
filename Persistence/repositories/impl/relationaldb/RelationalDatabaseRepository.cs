using Domain.commons;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Shared.helpers.HelpersEntities;
using Application.interfaces;
using Application.parameters;
using Application.helpers;
using System.Net;

namespace Persistence.repositories.impl.relationaldb
{
    public class RelationalDatabaseRepository<T> : IRelationalDatabaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext context;
        internal DbSet<T> dbSet; // optional

        public RelationalDatabaseRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void add(T entity)
        {

            dbSet.Add(entity);
            //dbSet.FindAsync()
        }

        public async Task<T> addAsync(T entity)
        {
            EntityEntry<T> entityCreated = await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entityCreated.Entity;
        }

        public async Task<List<T>> addMassiveAsync(List<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities;
        }



        public async Task<int> countAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (!isNull(filter))
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

        public T get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> getAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));

            // aplicate filter
            if (!isNull(filter))
            {
                query = query.Where(filter);
            }


            // aplicate order by
            if (!isNull(orderBy))
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public async Task<List<T>> getAllAsync(Dictionary<string, int> additionalProps, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, RequestParameter pagination = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            includes.ToList()
                .ForEach(prop => query = query.Include(prop));

            if (pagination != null)
            {
                int pageNumber = pagination.pageNumber, pageSize = pagination.pageSize;
                query = query.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }
            // aplicate filter
            if (!isNull(filter))
            {
                query = query.Where(filter);
            }

            // aplicate order by
            if (!isNull(orderBy))
            {
                query = orderBy(query);
            }

            List<T> resultsItems = await query.ToListAsync();
            additionalProps[HelpersConstApplication.KEY_TOTAL_COUNT] = resultsItems.Count();

            return resultsItems;
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public T getFirstOrDefault(Expression<Func<T, bool>> filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            // observation: include properties : ToLower()
            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));


            // aplicate filter
            if (!isNull(filter))
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task<T> getFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            // observation: include properties : ToLower()
            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));

            // aplicate filter
            if (!isNull(filter))
            {
                query = query.Where(filter);
            }


            return await query.FirstOrDefaultAsync();
        }

        public void remove(int id)
        {
            T entityRemove = dbSet.Find(id);

            remove(entityRemove);
        }

        public void remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> removeAsync(int id)
        {
            T entityRemove = dbSet.Find(id);
            entityRemove.isDeleted = 1;
            context.Entry(entityRemove).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityRemove;
            //dbSet.
        }

        public async Task<T> updateAsync(int id, T entity)
        {

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
