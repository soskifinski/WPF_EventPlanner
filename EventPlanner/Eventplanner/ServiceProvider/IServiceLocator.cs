using System;
using Autofac;

namespace Eventplanner.ServiceProvider
{
    public interface IServiceLocator
    {
        ILifetimeScope BeginLifetimeScope();
        ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction);
    }
}
