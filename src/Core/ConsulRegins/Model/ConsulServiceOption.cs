using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ConsulRegins.Model
{
    public class ConsulServiceOption
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 服务
        /// </summary>

        public ConsulRegistryConfig Consul { get; set; }
        /// <summary>
        /// 心跳检测地址
        /// </summary>

        public string HealthCheckTemplate { get; set; }

        public string[] Endpoints { get; set; }
    }
}
