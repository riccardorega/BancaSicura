using Banca.Conti.Repository.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banca.Conti.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContiRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContiDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Server=localhost,1433;Database=ContiDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;")));

            services.AddScoped<IContiRepository, ContiRepository>();

            return services;
        }
    }
}
