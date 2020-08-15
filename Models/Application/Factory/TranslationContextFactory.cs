using FictionalLanguageTranslator.Models.Application.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FictionalLanguageTranslator.Models.Application.Factory
{
    public static class TranslationContextFactory
    {
        public static IHost CreateDbIfNotExists(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<TranslationContext>();
                context.Database.EnsureCreated();
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<TranslationContext>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }

            return host;
        }
    }
}
