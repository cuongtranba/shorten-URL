using Autofac;
using Domain;
using DomainRepository;
using ServiceInterface;

namespace ServiceImplementation
{
    public class ServiceIOCModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register all services
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterType<ReportByLast7Days>().Named<IReport>(ReportType.ReportByLast7Days.ToString());
            builder.RegisterType<ReportByBrowser>().Named<IReport>(ReportType.ReportByBrowser.ToString());
            builder.RegisterType<ReportByCountry>().Named<IReport>(ReportType.ReportByCountry.ToString());
            builder.RegisterType<ReportByPlatforms>().Named<IReport>(ReportType.ReportByPlatforms.ToString());

            builder.RegisterModule<DbContextIocModule>();
            base.Load(builder);
        }
    }
}
