﻿using Autofac;
using DomainRepository;

namespace ServiceImplementation
{
    public class ServiceIOCModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(c => c.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();
            builder.RegisterModule<DbContextIocModule>();
            base.Load(builder);
        }
    }
}
