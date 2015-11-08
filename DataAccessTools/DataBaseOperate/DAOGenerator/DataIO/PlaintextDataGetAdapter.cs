using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    /// <summary>
    /// 从直接的规定的列表文本流中获得需要的内容
    /// 数据库表设计字段格式：
    /// 字段名	字段类型	是否主码	唯一组名称  是否可空	是否标识ID	外码参照表	外码参照字段	强制逻辑类型
    /// 强制定义的属性类型用于将数据库字段类型与DAO类逻辑类型相匹配，数据库设计时为了方便等原因会将某些逻辑字段用其他方式记录，此时在程序中需要进行两种类型的转换
    /// DAO属性的最终类型受到数据库类型、强制定义属性类型、外码表名三方面的影响
    /// </summary>
    public class PlaintextDataGetAdapter : IDataGetAdapter
    {
        private StreamReader textStreamReader = null;
        private DatabaseType databaseType;
        /// <summary>
        /// 使用StreamReader构造对象
        /// </summary>
        /// <param name="_textStreamReader"></param>
        public PlaintextDataGetAdapter(StreamReader _textStreamReader, DatabaseType _databaseType)
        {
            this.textStreamReader = _textStreamReader;
            this.databaseType = _databaseType;
        }

        #region IDataGetAdapter 成员

        public List<CSharpClassProperty> GetCSharpClassPropertyList()
        {
            if (textStreamReader == null) return null;

            List<CSharpClassProperty> classProperties = new List<CSharpClassProperty>();
            while (true)
            {
                string strline = textStreamReader.ReadLine();
                if (strline == null) break;
                classProperties.Add(GetCSharpClassProperty(strline));
            }
            return classProperties;
        }

        #endregion

        /// <summary>
        /// 从定义好的表字段格式中获取字段对应的对象属性
        /// 分隔符为制表符\t
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private CSharpClassProperty GetCSharpClassProperty(string _str)
        {
            string[] items = _str.Split(new string[] { "\t", " " }, StringSplitOptions.RemoveEmptyEntries);
            string fieldName = items[0].Trim();
            //对表示类型的字符串进行处理
            string fieldTypeTemp = items[1].Trim().ToLower();
            string fieldTypeStr = "";
            SQLServer2008DataType fieldType = SQLServer2008DataType.Char;
            int fieldSize = 0;
            //分析获取字段类型和大小
            //注意这里假定数据库类型为MSSQL2008，如果需要扩展的时候，可以进行数据库类型的扩展
            ParseTypeSizeFromMSSql2008TypeStr(fieldTypeTemp, out fieldType, out fieldSize);
            //判断主码
            bool isPK = false;
            if (items[2].Trim().Equals("y")) isPK = true;
            else isPK = false;
            //获取唯一组名称信息
            string uniqueGroupName = items[3].Trim();
            if (uniqueGroupName.Equals("n")) uniqueGroupName = "";
            //是否可空
            bool canNull = true;
            if (items[4].Trim().Equals("y")) canNull = true;
            else canNull = false;
            //是否是标识ID
            bool isID = false;
            if (items[5].Trim().Equals("y")) isID = true;
            else isID = false;
            //获得外码性质
            string FKTable = "";
            string FKField = "";
            if (!items[6].Trim().Equals("n")) FKTable = items[6].Trim();
            if (!items[7].Trim().Equals("n")) FKField = items[7].Trim();
            //强制逻辑类型
            CSharpDataType forcePropertyType = null;
            if (!items[8].Trim().Equals("n"))
            {
                forcePropertyType = CSharpDataType.GetType((CSharpDataTypeEnum)Enum.Parse(typeof(CSharpDataTypeEnum), items[8].Trim()));
            }

            TableField tf = new TableField(databaseType, fieldName, (int)fieldType, fieldSize, isPK, uniqueGroupName, isID, canNull, FKTable, FKField);
            CSharpClassProperty classProperty = new CSharpClassProperty(tf, forcePropertyType);
            return classProperty;
        }
        /// <summary>
        /// 对形如varchar(max)的数据库字段类型表示方法进行解析，得到具体的类型和字段大小等信息
        /// </summary>
        /// <param name="_str"></param>
        /// <param name="_fieldType"></param>
        /// <param name="_fieldSize"></param>
        private void ParseTypeSizeFromMSSql2008TypeStr(string _str, out SQLServer2008DataType _fieldType, out int _fieldSize)
        {
            SQLServer2008DataType fieldType = SQLServer2008DataType.Char;
            int fieldSize = 0;
            bool needParse = false;//表示是否需要特殊方法获取类型内部的信息
            string fieldTypeStr = "";
            switch (_str)
            {//对于不需要根据字段内容获取字段大小的数据库类型
                case "int":
                    fieldType = SQLServer2008DataType.Int;
                    fieldSize = 4;
                    break;
                case "smallint":
                    fieldType = SQLServer2008DataType.SmallInt;
                    fieldSize = 2;
                    break;
                case "bigint":
                    fieldType = SQLServer2008DataType.BigInt;
                    fieldSize = 8;
                    break;
                case "datetime":
                    fieldType = SQLServer2008DataType.DateTime;
                    fieldSize = 8;
                    break;
                case "real":
                    fieldType = SQLServer2008DataType.Real;
                    fieldSize = 4;
                    break;
                case "float":
                    fieldType = SQLServer2008DataType.Float;
                    fieldSize = 8;
                    break;
                case "image":
                    fieldType = SQLServer2008DataType.Image;
                    break;
                case "bit":
                    fieldType = SQLServer2008DataType.Bit;
                    fieldSize = 1;
                    break;
                default:
                    needParse = true;
                    break;
            }
            if (needParse)
            {
                int leftkuo = _str.IndexOf('(');
                int rightkuo = _str.IndexOf(')');
                fieldTypeStr = _str.Substring(0, leftkuo);
                string fieldSizeStr = _str.Substring(leftkuo + 1, rightkuo - leftkuo - 1);
                //需要处理fieldSizeStr为max的情况
                if (fieldSizeStr.Equals("max")) fieldSize = -1;
                else fieldSize = Int32.Parse(fieldSizeStr);
            }
            switch (fieldTypeStr)
            {
                case "char":
                    fieldType = SQLServer2008DataType.Char;
                    break;
                case "nchar":
                    fieldType = SQLServer2008DataType.NChar;
                    break;
                case "varchar":
                    fieldType = SQLServer2008DataType.VarChar;
                    break;
                case "nvarchar":
                    fieldType = SQLServer2008DataType.NVarChar;
                    break;
                case "varbinary":
                    fieldType = SQLServer2008DataType.VarBinary;
                    break;
            }
            _fieldType = fieldType;
            _fieldSize = fieldSize;
        }
    }
}
