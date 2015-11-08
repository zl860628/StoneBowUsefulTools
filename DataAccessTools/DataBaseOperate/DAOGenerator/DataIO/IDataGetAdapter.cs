using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    /// <summary>
    /// 数据访问类自动生成功能中定义数据字段的数据获取适配器接口，通过此接口可以从数据中获取到需要的TableField对象列表
    /// </summary>
    public interface IDataGetAdapter
    {
        List<CSharpClassProperty> GetCSharpClassPropertyList();
    }
}
