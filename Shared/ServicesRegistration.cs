using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Shared.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ServicesRegistration
    {


        public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<IDateTimeService, DateTimeService>();
            /*string d = Directory.GetCurrentDirectory();
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(d)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);*/

            //IConfiguration configuration = configBuilder.Build();


            //var serviceProvider = services.BuildServiceProvider();
            services.AddLogging(loggingBuilder =>
            {

                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            var loggerSettings = configuration.GetSection("LoggerSettings").Get<LoggerSettings>();
            var _rollingInterval = Convert.ToInt32(loggerSettings?.RollingInterval);
            bool _rollingIntervalError = false;

            if (_rollingInterval < 0)
            {
                _rollingIntervalError = true;
                _rollingInterval = 0;
            }

            var _logEventLevel = Convert.ToInt32(loggerSettings?.LogEventLevel);
            bool _logEventLevelError = false;

            if (_logEventLevel < 0)
            {
                _logEventLevelError = true;
                _logEventLevel = 0;
            }

            var _path = Environment.CurrentDirectory + loggerSettings?.LoggerPath;

            var _levelSwitch = new LoggingLevelSwitch();
            _levelSwitch.MinimumLevel = (LogEventLevel)_logEventLevel;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(_levelSwitch)
                .WriteTo.File(_path, rollingInterval: (RollingInterval)_rollingInterval, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss zzz}][{Level:u3}] {Message:lj}{NewLine}{Exception}")
                //.WriteTo.Console(LogEventLevel.Debug)
                .CreateLogger();

            if (_rollingIntervalError)
            {
                Log.Logger.Error("Error en el parámetro: {Parametro} del archivo: {Archivo}. Se creará un único archivo de log.", nameof(RollingInterval), "appsettings.json");
            }

            if (_logEventLevelError)
            {
                Log.Logger.Error("Error en el parámetro: {Parametro} del archivo: {Archivo}. El mínimo evento por default será Verbose.", nameof(LogEventLevel), "appsettings.json");
            }


            services.AddMemoryCache();

            return services;


        }

    }
}
