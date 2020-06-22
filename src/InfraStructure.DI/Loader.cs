using Application.Transfer.Commands;
using CQRSHelper._Common;
using Domain.Repositories;
using InfraStructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace InfraStructure.DI
{
    public class Loader
    {

        public static void Register(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration cfg, Action<Type> setMediator)
        {
            var conn = cfg.GetConnectionString("AccountDB");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TrasnferRepository>();

            setMediator(typeof(TrasnferCommandHandler));

            //foreach (var item in CQRSHelper.Loader.GetAll())
            //{
            //    setMediator(item);
            //}
            //foreach (var item in GetAll())
            //{

            //    setMediator(item);
            //}


        }

        public static Assembly[] GetAll()
        {
            return new[] {
                typeof(ICommand).GetTypeInfo().Assembly,
                typeof(IQuery<Response>).GetTypeInfo().Assembly
            };
        }

    }
}
