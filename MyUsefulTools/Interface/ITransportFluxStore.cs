using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyUsefulTools.Interface
{
    /// <summary>
    /// 传输流量存储规范的接口
    /// </summary>
    public interface ITransportFluxStore
    {
        /// <summary>
        /// 获取上传流量
        /// </summary>
        long UploadFlux { get; }
        /// <summary>
        /// 获取下载流量
        /// </summary>
        long DownloadFlux { get; }
        /// <summary>
        /// 流量统计的项目名称
        /// </summary>
        string ItemName { get; }
        /// <summary>
        /// 开始传输时间
        /// </summary>
        DateTime BeginTransportTime { get; }
    }
}
