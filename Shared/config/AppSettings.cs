using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.config
{
    public class AppSettings
    {
        public LoggerSettings LoggerSettings { get; set; }
    }

    public class LoggerSettings
    {
        public string LoggerPath { get; set; }

        private string _rollingInterval = "-1";

        public string RollingInterval
        {
            get
            {
                return _rollingInterval;
            }
            set
            {
                if (int.TryParse(value.ToString(), out int _rollingIntervalResult) && _rollingIntervalResult >= 0 && _rollingIntervalResult <= 5)
                {
                    _rollingInterval = value;
                }
            }
        }

        private string _logEventLevel = "-1";

        public string LogEventLevel
        {
            get
            {
                return _logEventLevel;
            }
            set
            {
                if (int.TryParse(value.ToString(), out int _logEventLevelResult) && _logEventLevelResult >= 0 && _logEventLevelResult <= 5)
                {
                    _logEventLevel = value;
                }
            }
        }
    }
}
