using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.config
{
    public class ProfileLogger
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
        }

        static ProfileLogger()
        {
            MasterLogger = new ProfileLogger();
        }

        private static ProfileLogger MasterLogger;
        private static Profiler Profiler;

        public static Profiler Profile(string text, string categoryName)
        {
            Profiler = new Profiler(categoryName, text);
            return Profiler;
        }
    }

    public class Profiler : IDisposable
    {
        public Stopwatch Watch { get; set; }
        private string Text;
        private string CategoryName;
        private readonly ILogger _logger = Log.ForContext(typeof(Profiler));

        public Profiler(string categoryName, string text)
        {
            try
            {
                CategoryName = categoryName;
                Text = text;
                Watch = Stopwatch.StartNew();
            }
            catch (Exception)
            {
            }
        }

        public void Dispose()
        {
            try
            {
                Watch.Stop();
                LogProfiler(Text, Watch.ElapsedMilliseconds.ToString());
                Watch = null;
            }
            catch (Exception)
            {
            }
        }

        public void LogProfiler(string text, string time)
        {
            //Log.Logger.Information(CategoryName, string.Format("{0}: {1}ms", text, time));
            _logger.Information("{text}: {time}ms", text, time);
        }
    }
}
