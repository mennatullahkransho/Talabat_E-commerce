using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfile;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AssemblyReferenceService).Assembly));
            services.AddTransient<PictureURLResolver>();
            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
