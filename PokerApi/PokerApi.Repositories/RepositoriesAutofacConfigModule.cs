using Autofac;
using Microsoft.EntityFrameworkCore;
using PokerApi.Repositories.Concretes;
using PokerApi.Repositories.Interfaces;
using Serilog;

namespace PokerApi.Repositories
{
    public class RepositoriesAutofacConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(Log.Logger)
                .As<ILogger>()
                .SingleInstance();

            builder.RegisterType<PokerContext>()
                .As<DbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
