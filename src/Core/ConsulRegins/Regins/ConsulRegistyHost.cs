using Consul;
using Core.ConsulRegins.Iserver;
using Core.ConsulRegins.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Core.ConsulRegins.Regins
{
    public class ConsulRegistyHost : Iregistry
    {
        private const string VERSION_PREFIX = "version-";
        private readonly ConsulRegistryConfig _configuration;
        private readonly ConsulClient _consul;
        public ConsulRegistyHost(ConsulRegistryConfig configuration = null)
        {
            _configuration = configuration;
            _consul = new ConsulClient(config =>
            {
                config.Address = new Uri(_configuration.Address);
                if (!string.IsNullOrWhiteSpace(_configuration.Datacenter))
                {
                    config.Datacenter = _configuration.Datacenter;
                }
            });

        }

        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <param name="startSeconds">服务启动多久后进行注册</param>
        /// <param name="intervalSeconds">检测的间隔时间</param>
        /// <param name="checkUri">检测地址</param>
        /// <param name="timeoutSeconds">超时的时间</param>
        /// <returns></returns>
        public Task<string> AgentHeakthCheck(int startSeconds, int intervalSeconds, Uri checkUri, string timeoutSeconds)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 注销实例
        /// </summary>
        /// <param name="checkId">实例的标识</param>
        /// <returns></returns>
        public async Task<bool> deregisterHealthCheckAsync(string checkId)
        {
            var writeResult = await _consul.Agent.CheckDeregister(checkId);
            bool isSuccess = writeResult.StatusCode == HttpStatusCode.OK;
            string success = isSuccess ? "succeeded" : "failed";

            return isSuccess;
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="version">版本号</param>
        /// <param name="healthCheckUri">健康检查地址</param>
        /// <param name="tags">标签</param>
        /// <returns></returns>
        public async Task<ReginModel> serviceRegister(Uri address, string serviceName, string version, Uri healthCheckUri = null, IEnumerable<string> tags = null)
        {
            ///服务id
            string serverID = GetServiceId(serviceName, address);

            string healCheck = healthCheckUri?.ToString() ?? $"{address}".TrimEnd('/') + "/status";//心跳检测的路径
            var tagList = (tags ?? Enumerable.Empty<string>()).ToList();//版本号信息
            string versionLabel = $"{VERSION_PREFIX}{version}";//版本号信息
            tagList.Add(versionLabel);//添加新的版本号
            //心跳检测
            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),//服务启动多久后注册
                Interval = TimeSpan.FromSeconds(10),//健康监测，多久进行检查一次
                HTTP = healCheck,//心跳检测地址
                Timeout = TimeSpan.FromSeconds(5)//超时
            };
            //服务注册
            var registrtion = new AgentServiceRegistration()
            {
                Checks = new[] { httpCheck },
                ID = serverID,//服务编号
                Name = serviceName,//服务名称
                Address = address.Host,//服务地址
                Tags = tagList.ToArray(),//服务标签
                Port = address.Port//服务端口号
            };
            await _consul.Agent.ServiceRegister(registrtion);

            return new ReginModel
            {
                Address = registrtion.Address,
                ID = registrtion.ID,
                Port = registrtion.Port,
                ServiceName = registrtion.Name
            };
        }

        private string GetServiceId(string serviceName, Uri uri)
        {
            return $"{serviceName}_{uri.Host.Replace(".", "_")}_{uri.Port}";
        }


    }
}
