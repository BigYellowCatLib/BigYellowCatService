using Core.ConsulRegins.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ConsulRegins.Iserver
{
   public interface IReginConsul
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="address">服务地址</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="version">版本号</param>
        /// <param name="healthCheckUri">健康检查地址</param>
        /// <param name="tags">标签</param>
        /// <returns></returns>
        Task<ReginModel> serviceRegister(Uri address, string serviceName, string version, Uri healthCheckUri = null, IEnumerable<string> tags = null);


    }
}
