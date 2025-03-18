using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Eventplanner.ServiceProvider
{
    public class ServiceLocator : IServiceLocator
    {
        public IContainer Container { get; private set; }

        public ServiceLocator()
        {
            BuildContainer();
        }

        public ILifetimeScope BeginLifetimeScope()
        {
            return Container.BeginLifetimeScope();
        }

        public ILifetimeScope BeginLifetimeScope(Action<ContainerBuilder> configurationAction)
        {
            return Container.BeginLifetimeScope(configurationAction);
        }

        private void BuildContainer()
        {
            var builder = new ContainerBuilder();

            try
            {
                var assemblies = new List<Assembly>();

                var dlls = GetDLLs();
                foreach (var dll in dlls)
                {
                    try
                    {
                        assemblies.Add(Assembly.LoadFile(dll));
                    }
                    catch (Exception ex)
                    {
                      
                    }
                }

                builder.RegisterAssemblyTypes(assemblies.ToArray())
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .AsSelf();

                builder.RegisterAssemblyTypes(assemblies.ToArray())
                    .Where(t => IsContext(t))
                    .AsSelf()
                    .InstancePerLifetimeScope();

                builder.RegisterAssemblyTypes(assemblies.ToArray())
                    .Where(t => IsRepository(t) || IsModalDialog(t))
                    .InstancePerLifetimeScope()
                    .AsImplementedInterfaces();
            }
            catch (Exception ex)
            {
                
            }

            Container = builder.Build();
        }

        private bool IsRepository(System.Reflection.MemberInfo mi)
        {
            return mi.Name.Contains("Repository");
        }

        private bool IsContext(System.Reflection.MemberInfo mi)
        {
            return mi.Name.EndsWith("Context");
        }

        private bool IsModalDialog(System.Reflection.MemberInfo mi)
        {
            return mi.Name.Contains("ModalDialog");
        }

        private static IEnumerable<string> GetDLLs()
        {
            var location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            return Directory.GetFiles(location, "Eventplanner.*.dll", SearchOption.AllDirectories);
        }
    }
}
