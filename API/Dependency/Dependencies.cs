using Autofac;
using Domain.Interfaces;
using Infrastructure;

namespace API.Dependency
{
    public class Dependencies : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
