using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using ProgLessons.IoC_example.App.Services.Auth;

namespace ProgLessons.IoC_example.App.Services.IoC
{
    public class ContainerConfigurator
    {
        public Dictionary<Type, Type> DynamicServices { get; private set; } = new Dictionary<Type, Type>();

        public IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<UserAccountsRegister>();
            serviceCollection.AddSingleton<UserAccountsManager>();
            serviceCollection.AddSingleton<IAuthenticationService, AuthenticationService>();

            //Registra os servicos/classes GUI
            foreach (KeyValuePair<Type, Type> dynSvc in DynamicServices)
                serviceCollection.AddSingleton(dynSvc.Key, dynSvc.Value);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
