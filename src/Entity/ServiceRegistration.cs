using Entity.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // SQL Server Database Connection
            services.AddDbContext<InvoiceAppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));

            // PostgreSQL Server Database Connection
            //services.AddDbContext<InvoiceExecuterDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Default")));
        }
    }
}
