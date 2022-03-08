using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCrm
{
    public class ConfigurationGreeter : IGreeter
    {
        public ConfigurationGreeter(IConfiguration configuration)
        {

        }
        public string GetGreeting()
        {
            return "new greeting from GetGreeting";
        }
    }
}
