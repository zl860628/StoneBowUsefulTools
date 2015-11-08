using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    /// <summary>
    /// 此类负责将SQL数据类型转变为对应的C#数据类型
    /// </summary>
    public class TranslateSqlTypeToCSharpDataType
    {
        /// <summary>
        /// 将表示数据库字段的TableField对象转换为对应的C#数据类型
        /// </summary>
        /// <param name="_tableField"></param>
        /// <returns></returns>
        public static CSharpDataType TranslateType(TableField _tableField)
        {
            CSharpDataType codeDataType = null;
            switch (_tableField.TheDatabaseType)
            {//根据不同的数据库类型，进行不同的转换方法
                case DatabaseType.SQLServer2008:
                    {
                        SQLServer2008DataType datatype = (SQLServer2008DataType)_tableField.FieldType;
                        bool canNull = _tableField.CanNull;
                        switch (datatype)
                        {
                            case SQLServer2008DataType.Char:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.@string);
                                break;
                            case SQLServer2008DataType.NChar:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.@string);
                                break;
                            case SQLServer2008DataType.NVarChar:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.@string);
                                break;
                            case SQLServer2008DataType.VarChar:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.@string);
                                break;
                            case SQLServer2008DataType.Int:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.int_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.int_value);
                                break;
                            case SQLServer2008DataType.SmallInt:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.short_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.short_value);
                                break;
                            case SQLServer2008DataType.BigInt:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.long_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.long_value);
                                break;
                            case SQLServer2008DataType.DateTime:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.DateTime_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.DateTime_value);
                                break;
                            case SQLServer2008DataType.Real:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.float_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.float_value);
                                break;
                            case SQLServer2008DataType.Float:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.double_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.double_value);
                                break;
                            case SQLServer2008DataType.Image:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.byte_array);
                                break;
                            case SQLServer2008DataType.VarBinary:
                                codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.byte_array);
                                break;
                            case SQLServer2008DataType.Bit:
                                if (canNull) codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.bool_nullable);
                                else codeDataType = CSharpDataType.GetType(CSharpDataTypeEnum.bool_value);
                                break;
                        }
                        break;
                    }
            }
            return codeDataType;
        }
    }
}
