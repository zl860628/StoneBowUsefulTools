using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoneUtils.Interface
{
    /// <summary>
    /// 树节点中的数据需要实现此接口的方法，以便树表能正常工作
    /// </summary>
    public interface ITreeNodeData
    {
        /// <summary>
        /// 获取记录ID
        /// </summary>
        int ID { get; }
        /// <summary>
        /// 是否为数据库中存储的记录
        /// </summary>
        bool IsRecord { get; }
    }
}
