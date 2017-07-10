using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;

namespace aspnet_Core_test
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use((context, next) =>
            {
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {

                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                // Call the next delegate/middleware in the pipeline
                return next();
            });

            app.Run(async (context) =>
            {
                var msg = "Hello";
                switch (CultureInfo.CurrentCulture.ToString()
               ) {
                    case "it-IT":
                        msg = "Ciao";
                        break;
                    case "fr-FR":
                        msg = "Bonjour";
                        break;
                }
                await context.Response.WriteAsync( $"{msg} {CultureInfo.CurrentCulture.DisplayName} {DateTime.Now} ");
            });

        }
    }
}
