using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Core.ConsulRegins.Model
{
    /// <summary>
    /// consul注册信息实体类
    /// </summary>
    public class ConsulRegistryConfig
    {
        /// <summary>
        /// consul的url地址
        /// </summary>
        public DnsEndpoint DnsEndPoint { get; set; }
        /// <summary>
        /// Consul的url地址
        /// </summary>

        public string Address { get; set; }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string Datacenter { get; set; }
        public string AclToken { get; set; }
    }

    public class DnsEndpoint
    {
        public string Address { get; set; }

        public int Port { get; set; }

        public IPEndPoint ToIPEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Address), Port);
        }
    }
}
