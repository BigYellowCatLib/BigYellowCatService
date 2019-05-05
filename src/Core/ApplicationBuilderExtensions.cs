
using Core.ConsulRegins.Model;
using Core.ConsulRegins.Regins;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Core
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 注册consul
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lifetime"></param>
        /// <param name="serviceEntity"></param>
        /// <returns></returns>
        public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            ConsulServiceOption consulServiceOption = new ConsulServiceOption();
            configuration.GetSection("ServiceDiscovery").Bind(consulServiceOption);
            ConsulRegistryConfig consulRegistryConfig = new ConsulRegistryConfig();
            configuration.GetSection("ConsulRegistryConfig").Bind(consulRegistryConfig);


            consulServiceOption.Consul = new ConsulRegistryConfig();
            if (consulRegistryConfig == null)
            {
                consulRegistryConfig.Address = string.Format($"http://localhost:8500");
                consulRegistryConfig.Datacenter = string.Format($"dc1");

            }
            consulServiceOption.Consul.Address = consulRegistryConfig.Address;
            consulServiceOption.Consul.Datacenter = consulRegistryConfig.Datacenter;


            ConsulRegistyHost consulRegistyHost = new ConsulRegistyHost(consulServiceOption.Consul);

            IEnumerable<Uri> address = consulServiceOption.Endpoints.Select(p => new Uri(p));
            foreach (var item in address)
            {
                Uri healthCheck = new Uri(item, consulServiceOption.HealthCheckTemplate);
                var test = consulRegistyHost.serviceRegister(item, consulServiceOption.ServiceName, consulServiceOption.Version, healthCheck, tags: new[] { $"test-/{consulServiceOption.ServiceName}" }).Result;


            }
            return app;
        }

    }
}
