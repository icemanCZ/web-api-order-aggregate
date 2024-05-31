using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAggregate.Core.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;
        public SenderServiceConfiguration SenderService{ get; set; }

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            SenderService = new SenderServiceConfiguration(configuration);
        }

        public class SenderServiceConfiguration
        {
            private readonly IConfiguration _configuration;

            public int IntervalSec { get { return _configuration.GetValue<int>("SenderService:IntervalSec"); } }

            public SenderServiceConfiguration(IConfiguration configuration)
            {
                _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            }
        }
    }
}
