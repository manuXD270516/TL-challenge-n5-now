using Application.interfaces;
using Application.parameters;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories.impl.search
{
    public class ElasticSearchRepository<T> : ISearchRepository<T> where T : class
    {

        private readonly IElasticClient _client;
        private readonly string _indexName;


        public ElasticSearchRepository(IElasticClient client,IConfiguration configuration)
        {
            _client = client;
            _indexName = configuration["Elasticsearch:DefaultIndex"];
        }

        public async Task<T> addAsync(T entity)
        {
            var response = await _client.IndexAsync(entity, i => i.Index(_indexName));
            return null;
        }

        public async Task<List<T>> getAllAsync(Dictionary<string, int> additionalProps = null, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, RequestParameter pagination = null, params Expression<Func<T, object>>[] includes)
        {
            var response = await _client.SearchAsync<T>(s => s.Index(_indexName));
            return response.Documents
                .ToList();
        }

        public async Task<T> getByIdAsync(int id)
        {
            var response = await _client.GetAsync<T>(id, g => g.Index(_indexName));
            return response.Source;
        }

        public async Task<T> removeAsync(int id)
        {
            var deleteItem = await getByIdAsync(id);
            await _client.DeleteAsync<T>(id, d => d.Index(_indexName));
            return deleteItem;
        }

        public async Task<List<T>> searchAsync(string term)
        {
            var response = await _client.SearchAsync<T>(s => s.Index(_indexName));
            return response.Documents
                .ToList();
        }

        public async Task<T> updateAsync(int id, T entity)
        {
            var response = await _client.UpdateAsync<T>(id, u => u.Index(_indexName).Doc(entity));
            return await getByIdAsync(id);
        }
    }
}
