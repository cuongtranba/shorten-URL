using Autofac;

namespace DomainRepository
{
    public class DbContextIocModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new ShortenURlDbContext()).AsSelf().InstancePerRequest();
            base.Load(builder);
        }
    }
}
