using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Data;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    public enum CSharpDataTypeEnum
    {
        bool_value, bool_nullable,
        char_value, char_nullable,
        int_value, int_nullable,
        short_value, short_nullable,
        long_value, long_nullable,
        DateTime_value, DateTime_nullable,
        float_value, float_nullable,
        double_value, double_nullable,
        byte_array,
        @string, @null
    }
    /// <summary>
    /// 表示C#数据类型的一些事
    /// </summary>
    public class CSharpDataType
    {
        //私有属性
        //类型名称
        private string typeName = "";
        //类型默认初始化值
        private string defaultValue = "";

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="_typeName"></param>
        /// <param name="_defaultValue"></param>
        public CSharpDataType(string _typeName, string _defaultValue)
        {
            typeName = _typeName;
            defaultValue = _defaultValue;
        }

        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        public string DefaultValue
        {
            get { return this.defaultValue; }
            set { this.defaultValue = value; }
        }

        public static CSharpDataType GetType(CSharpDataTypeEnum _typeEnum)
        {
            string dataTypeStr = _typeEnum.ToString();

            CSharpDataType dataType = null;
            string typeName = dataTypeStr.Replace("@", "");//类型名称
            string typeDefaultValue = "null";//类型默认初始化值
            bool isNullableValue = true;//类型是否为值类型
            bool isArray = false;//类型是否为数组类型

            //可以使用代码简化操作的类型时
            if (dataTypeStr.Contains("_"))
            {
                //先提取出来类型名
                typeName = dataTypeStr.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[0];
                switch (typeName)
                {//直接类型获取默认初始化值
                    case "bool": typeDefaultValue = "false"; break;
                    case "char": typeDefaultValue = "''"; break;
                    case "DateTime": typeDefaultValue = "Constant.DateTime_MinValue"; break;
                    case "int": typeDefaultValue = "-1"; break;
                    case "short": typeDefaultValue = "-1"; break;
                    case "long": typeDefaultValue = "-1"; break;
                    case "float": typeDefaultValue = "0.0F"; break;
                    case "double": typeDefaultValue = "0.0"; break;
                    case "byte": typeDefaultValue = "0"; break;
                }
                //根据相关命名规则得到类型特性
                if (dataTypeStr.EndsWith("value", StringComparison.CurrentCultureIgnoreCase))
                {
                    isNullableValue = false;
                }
                else if (dataTypeStr.EndsWith("nullable", StringComparison.CurrentCultureIgnoreCase))
                {
                    isNullableValue = true;
                    //nullable的值类型中名称后需要加“?”
                    typeName += "?";
                    typeDefaultValue = "null";
                }
                else if (dataTypeStr.EndsWith("array", StringComparison.CurrentCultureIgnoreCase))
                {
                    isArray = true;
                    typeName += "[]";
                    typeDefaultValue = "null";
                }
            }
            else if (dataTypeStr.Equals("string"))
            {
                typeDefaultValue = "\"\"";
            }
            return new CSharpDataType(typeName, typeDefaultValue);
        }
    }
}