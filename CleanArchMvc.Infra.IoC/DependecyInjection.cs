using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.IoC
{
    public static class DependecyInjection
    {
        /*
         * Denição de metodo de extensão para utilização do conceito DependecyInjection
         * em todo o projeto sem ferir o padrão
         */
        public static IServiceCollection addInfrastructure(this IServiceCollection services, 
            IConfiguration configuration)
        {
            /* Regista o contexto ApplicationDbContext pelo metodo AddDbContext
             * Definição do provedor do banco UseSqlServer
             * defini a string de conexão DefaultConnection 
             * E estou definindo que as migrações vão ficar na pasta onde está definido o arquivo de contexto
             * MigrationsAssembly - Defini o assembley a onde as migrações serão mantidas para o contexto
             */
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //Registrando os repositorios
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //Registrando os serviços
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            //Registrando o automapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}