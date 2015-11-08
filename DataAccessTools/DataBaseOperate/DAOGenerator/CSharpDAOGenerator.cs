using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    /// <summary>
    /// DAO类生成器代码
    /// 主要根据数据库表定义生成对应的数据访问对象类
    /// 使用定义好的CSharpClassProperty对象来完成主要的生成工作
    /// </summary>
    public class CSharpDAOGenerator
    {
        #region 私有属性

        //数据库表名称
        private string tableName = "";
        //对象属性集
        private List<CSharpClassProperty> classProperties = null;
        //是否包含ID标识字段
        private bool hasIdentifier = false;

        public bool HasIdentifier
        {
            get { return this.hasIdentifier; }
            set { this.hasIdentifier = value; }
        }

        public string TableName
        {
            get { return this.tableName; }
            set { this.tableName = value; }
        }

        public List<CSharpClassProperty> ClassProperties
        {
            get { return this.classProperties; }
            set { this.classProperties = value; }
        }
        #endregion

        #region 构造方法

        public CSharpDAOGenerator(string _tableName, List<CSharpClassProperty> _classProperties)
        {
            this.tableName = _tableName;
            this.classProperties = _classProperties;
            foreach (CSharpClassProperty cp in _classProperties)
            {
                if (cp.TableField != null && cp.TableField.IsIdentifier)
                {
                    hasIdentifier = true;
                    break;
                }
            }
        }

        #endregion

        #region 生成方法
        /// <summary>
        /// 根据所需所有属性，生成完整的DAO类
        /// </summary>
        /// <returns></returns>
        public string GenerDAOCode()
        {
            if (tableName.Equals("") || classProperties == null)
            {
                //参数缺失
                return null;
            }
            StringBuilder codeSbd = new StringBuilder();
            codeSbd.Append(GenerPropertiesDefineCode()).Append(GenerPropertiesAccessCode())
                .Append(GenerAllGetPropertiesByUniqueFromDatabaseCode())
                .Append(GenerInsertRecordCode());
            return codeSbd.ToString();
        }
        /// <summary>
        /// 生成私有属性定义代码
        /// </summary>
        /// <returns></returns>
        public string GenerPropertiesDefineCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                code.AppendLine(cp.GenerPropertyDefine());
            }
            return code.ToString();
        }
        /// <summary>
        /// 生成属性访问代码
        /// </summary>
        /// <returns></returns>
        public string GenerPropertiesAccessCode()
        {
            StringBuilder code = new StringBuilder();
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                code.AppendLine(cp.GenerPropertyAccess());
            }
            return code.ToString();
        }
        /// <summary>
        /// 生成getPropertiesByID方法代码，用来根据表ID字段获取对象属性
        /// </summary>
        /// <returns></returns>
        public string GenerGetPropertiesByIDFromDatabaseCode()
        {
            //没有标识ID的时候，不生成此方法
            if (!hasIdentifier) return "";

            StringBuilder code = new StringBuilder();
            //函数头
            code.Append(@"  /// <summary>
                            /// 根据ID获取对象属性
                            /// </summary>");
            code.Append("\n");
            code.Append("private void GetPropertiesByID(int _id)\n{\n");
            //SQL语句、参数及执行
            code.Append("string sqlstr = \"select * from [" + this.tableName + "] where ID=@id;\";\n");
            code.Append(@"  SqlParameter[] paras = new SqlParameter[1];
                            paras[0] = new SqlParameter(""@id"", SqlDbType.Int);
                            paras[0].Value = _id;
                            DataTable dt = DBManager.SelectRecords(sqlstr, paras);");
            //判断记录
            code.AppendLine("");
            code.Append(@"  if (dt.Rows.Count == 0)
                            {
                                this.isRecord = false;
                                return;
                            }");
            code.AppendLine(@"  else
                                {
                                    DataRow dr = dt.Rows[0];");
            //给其他字段属性赋值
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null)
                    code.AppendLine(cp.GenerGetValueFromDatabase());
            }
            //使对象isRecord属性为true
            code.AppendLine("this.isRecord = true;");
            code.AppendLine("}").AppendLine("}");
            return code.ToString();
        }
        /// <summary>
        /// 生成所有唯一组的GetPropertiesByUnique方法
        /// </summary>
        /// <returns></returns>
        public string GenerAllGetPropertiesByUniqueFromDatabaseCode()
        {
            StringBuilder sbd_code = new StringBuilder();
            //获取所有的唯一组的名称
            Dictionary<string, bool> uniqueGroupNames = new Dictionary<string, bool>();
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null && !cp.TableField.UniqueGroupName.Equals(""))
                {
                    uniqueGroupNames[cp.TableField.UniqueGroupName] = true;
                }
            }
            for (int i = 0; i < uniqueGroupNames.Count; i++)
            {
                string groupName = uniqueGroupNames.Keys.ElementAt(i);
                sbd_code.AppendLine(GenerGetPropertiesByUniqueFromDatabaseCode(groupName));
            }
            return sbd_code.ToString(); ;
        }
        public string GenerGetPropertiesByUniqueFromDatabaseCode(string _groupName)
        {
            StringBuilder code = new StringBuilder();
            //唯一组（一组码）中的所有类属性
            List<CSharpClassProperty> uniqueProperties = new List<CSharpClassProperty>();
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null && cp.TableField.UniqueGroupName.Equals(_groupName))
                {
                    uniqueProperties.Add(cp);
                }
            }
            //函数头
            code.Append(@"  /// <summary>
                            /// 根据唯一属性组获取对象属性
                            /// </summary>");
            code.AppendLine();
            //生成形如“int _id, string _apple”
            StringBuilder sbd_paralist = new StringBuilder();
            foreach (CSharpClassProperty cp in uniqueProperties)
            {
                if (sbd_paralist.Length > 0) sbd_paralist.Append(", ");
                sbd_paralist.AppendFormat("{0} {1}", cp.PropertyType.TypeName, cp.ParaName);
            }
            string methodDef = string.Format("private void GetPropertiesBy{0}({1})\n{{", _groupName, sbd_paralist.ToString());
            code.AppendLine(methodDef);
            //准备SQL语句
            string sqlstr = @"string sqlstr = ""select * from [{0}] where {1};"";";
            string wherePart = "";
            foreach (CSharpClassProperty cp in uniqueProperties)
            {
                if (wherePart.Length > 0) wherePart += " and ";
                wherePart += string.Format("{0}=@{1}", cp.TableField.FieldName, cp.TableField.FieldName.ToLower());
            }
            code.AppendFormat(sqlstr, this.tableName, wherePart);
            code.AppendLine();
            //准备SQL语句参数
            string sqlParaDef = @"SqlParameter[] paras = new SqlParameter[{0}];";
            sqlParaDef = string.Format(sqlParaDef, uniqueProperties.Count);
            StringBuilder sbd_sqlParaAssign = new StringBuilder();
            for (int i = 0; i < uniqueProperties.Count; i++)
            {
                CSharpClassProperty cp = uniqueProperties[i];
                sbd_sqlParaAssign.Append(cp.GenerSQLParameter(i, SqlParameterValueMode.ParameterName));
            }
            string sqlExec = "DataTable dt = DBManager.SelectRecords(sqlstr, paras);";
            code.AppendLine(sqlParaDef).AppendLine(sbd_sqlParaAssign.ToString()).AppendLine(sqlExec);
            //判断记录
            code.AppendLine("");
            code.AppendLine(@"  if (dt.Rows.Count == 0)
                            {
                                this.isRecord = false;
                                return;
                            }
                            else
                            {
                                DataRow dr = dt.Rows[0];");
            //给其他字段属性赋值
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null)
                    code.AppendLine(cp.GenerGetValueFromDatabase());
            }
            //使对象isRecord属性为true
            code.AppendLine("this.isRecord = true;");
            code.AppendLine("}").AppendLine("}");
            return code.ToString();
        }
        /// <summary>
        /// 生成InsertNewRecord方法的全代码
        /// </summary>
        /// <returns></returns>
        public string GenerInsertRecordCode()
        {
            StringBuilder code = new StringBuilder();
            //方法头
            code.Append("public void InsertNewRecord()\n{\n");
            //判断此记录是否已在数据库表中
            code.AppendLine("if (isRecord) throw new Exception(\"记录重复\");");
            //生成SQL插入语句，注意此句中对于起标识作用的ID字段的处理，ID字段不出现在插入语句中
            code.AppendLine("");
            StringBuilder insertsql = new StringBuilder("insert into ");
            ////先写表名
            insertsql.Append("[").Append(this.tableName).Append("] values(");
            ////加入参数变量，除了标识属性
            int i = 0;
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null)
                {
                    if (cp.TableField.IsIdentifier && cp.TableField.IsPrimeKey)
                    {
                        continue;
                    }

                    if (i++ != 0) insertsql.Append(",");
                    insertsql.Append("@").Append(cp.TableField.FieldName.ToLower());
                }
            }
            insertsql.Append(");");
            code.Append("string sqlstr = \"").Append(insertsql.ToString()).Append("\";");

            ////加入参数数组定义，参数个数需要正确（否则会出现错误）
            int paraCount = 0;
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null) paraCount++;
            }
            if (hasIdentifier) paraCount--;//标识ID不算在参数里面
            code.Append("\n\n");
            code.Append("SqlParameter[] paras = new SqlParameter[").Append(paraCount).Append("];");
            ////加入每个字段对应的参数定义
            code.AppendLine("");
            i = 0;
            foreach (CSharpClassProperty cp in this.classProperties)
            {
                if (cp.TableField != null)
                {
                    if (cp.TableField.IsIdentifier && cp.TableField.IsPrimeKey)
                    {//唯一标识符类型的主码，数据库自动生成
                        continue;
                    }

                    code.AppendLine(cp.GenerSQLParameter(i++, SqlParameterValueMode.PropertyName));
                }
            }
            ////插入语句执行
            code.AppendLine("DBManager.InsertRecord(sqlstr, paras);");
            ////如果表包含ID字段，加入获取ID的代码
            if (hasIdentifier)
            {
                code.Append("sqlstr = \"select ID from [").Append(tableName).Append("] where ");
                i = 0;
                foreach (CSharpClassProperty cp in this.classProperties)
                {
                    if (cp.TableField != null)
                    {
                        if (cp.TableField.IsIdentifier && cp.TableField.IsPrimeKey)
                        {//唯一标识符类型的主码，数据库自动生成
                            continue;
                        }

                        if (i++ != 0) code.Append(" and ");
                        code.Append(cp.TableField.FieldName).Append("=@").Append(cp.TableField.FieldName.ToLower());
                    }
                }
                code.AppendLine(";\";");
                code.AppendLine("DataTable dt = DBManager.SelectRecords(sqlstr, paras);");
                code.AppendLine("iD = (int)dt.Rows[0][0];");
            }
            ////对象标为“已存在记录”
            code.AppendLine("");
            code.AppendLine("isRecord = true;");
            code.AppendLine("");
            code.AppendLine("}");
            return code.ToString();
        }
        #endregion
    }
}
