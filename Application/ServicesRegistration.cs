using Application.behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            /*services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies(), configure =>
            {
                configure.AsScoped();
                configure.Using<ServiceFactory>(with =>
                {
                    with.Configure<MediatR.DependencyInjection.ServiceFactoryOptions>(options =>
                    {
                        options.WithDelegateFactory();
                    });
                });
            });*/

            services.AddMediatR(Assembly.GetExecutingAssembly());


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
