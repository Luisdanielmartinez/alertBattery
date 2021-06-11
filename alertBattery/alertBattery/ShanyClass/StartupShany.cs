using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Shiny;

namespace alertBattery.ShanyClass
{
    public class StartupShany : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            // register your shiny services here
            services.UseNotifications(); // set true
        }
    }
}
