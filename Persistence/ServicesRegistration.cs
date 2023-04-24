using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence.impl;
using Persistence.options;
using Persistence.repositories.impl;
using Persistence.repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.interfaces;
using Application.interfaces.permission;
using Persistence.repositories.impl.relationaldb;
using Persistence.extensions;
using Persistence.repositories.impl.search;
using Microsoft.Extensions.Hosting;

namespace Persistence
{
    public static class ServicesRegistration
    {

        public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = "DatabaseSettings";
            var xx = configuration.GetSection("DatabaseSettings");
            services.Configure<ConnectionOptions>(configuration.GetSection("DatabaseSettings"));

            //services.AddTransient<DatabaseContextFactory>();
            //var x = configuration.GetSection("ConnectionStrings")["SqlServer"];

            IServiceProvider sp = services.BuildServiceProvider();
            var env = sp.GetService<IHostEnvironment>();
            if (env.IsEnvironment("IntegrationTests"))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddScoped<IDatabaseStrategy, InMemoryStrategy>(sp => new InMemoryStrategy(""));
            }
            else
            {
                var sqlServerConnectionString = configuration.GetConnectionString("SqlServer");
                //var mySqlConnectionString = configuration.GetConnectionString("MySql");

                services.AddScoped<IDatabaseStrategy, SqlServerDatabaseStrategy>(provider => new SqlServerDatabaseStrategy(sqlServerConnectionString));
                //services.AddScoped<IDatabaseStrategy, MySqlDatabaseStrategy>(provider => new MySqlDatabaseStrategy(mySqlConnectionString));

            }


            services.AddScoped<DatabaseContextFactory>();


            services.AddDbContextFactory<ApplicationDbContext>((provider, options) =>
            {
                //var connectionOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionOptions>>().Value;


                //var databaseStrategy = provider.GetService<IDatabaseStrategy>();
                var databaseStrategy = services.BuildServiceProvider().GetRequiredService<IDatabaseStrategy>();
                Console.Write($"DI =>  {databaseStrategy.GetConnectionString()}");

                databaseStrategy.ConfigureOptions(options);

                //return new DatabaseContextFactory(databaseStrategy);
                // opciones para usar MySQL en lugar de SQL Server
                // options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            /*services.AddSingleton<IDesignTimeDbContextFactory<ApplicationDbContext>>(provider =>
            {
                var strategy = provider.GetServices<IDatabaseStrategy>().FirstOrDefault();
                return new DatabaseContextFactory(strategy);
            });*/

            /*services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var databaseStrategy = provider.GetServices<IDatabaseStrategy>().FirstOrDefault();
                databaseStrategy?.ConfigureOptions(options);
            });*/
            services.AddElasticsearch(configuration);

            services.AddScoped(typeof(IRelationalDatabaseRepository<>), typeof(RelationalDatabaseRepository<>));
            services.AddScoped(typeof(ISearchRepository<>), typeof(ElasticSearchRepository<>));

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ISearchPermissionRepository, PermissionSearchRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //services.AddElasticsearch(configuration.GetSection("Elasticsearch"));


            return services;

        }
    }
}
