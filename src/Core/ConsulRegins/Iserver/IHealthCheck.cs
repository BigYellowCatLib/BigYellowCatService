using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ConsulRegins.Iserver
{
    /// <summary>
    /// 健康监测接口
    /// </summary>
    public interface IHealthCheck
    {
        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <param name="startSeconds">服务启动多久后进行注册</param>
        /// <param name="intervalSeconds">检测的间隔时间</param>
        /// <param name="checkUri">检测地址</param>
        /// <param name="timeoutSeconds">超时的时间</param>
        /// <returns></returns>
        Task<string> AgentHeakthCheck(int startSeconds, int intervalSeconds, Uri checkUri, string timeoutSeconds);
        /// <summary>
        /// 注销实例
        /// </summary>
        /// <param name="checkId">实例的标识</param>
        /// <returns></returns>
        Task<bool> deregisterHealthCheckAsync(string checkId);
    }
}
