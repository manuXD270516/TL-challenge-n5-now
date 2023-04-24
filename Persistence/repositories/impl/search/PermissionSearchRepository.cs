using Application.interfaces;
using Application.interfaces.permission;
using Domain.entities;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.repositories.impl.search
{
    public class PermissionSearchRepository : ElasticSearchRepository<Permission>, ISearchPermissionRepository
    {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public PermissionSearchRepository(IElasticClient client, IConfiguration configuration) : base(client, configuration)
        {
            _elasticClient = client;
            _indexName = "permissions";
        }
    }
}
