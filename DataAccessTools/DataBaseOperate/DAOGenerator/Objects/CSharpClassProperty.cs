using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    public enum SqlParameterValueMode
    {
        PropertyName, ParameterName
    }
    /// <summary>
    /// 表示C#语言DAO类中的属性，与数据库表字段相对应
    /// </summary>
    public class CSharpClassProperty
    {
        //私有属性
        //与此字段关联的数据库表字段对象
        private TableField tableField = null;
        //属性名称
        private string propertyName = "";
        //属性作为参数时的名称
        private string paraName = "";
        //属性类型
        private CSharpDataType propertyType = null;
        //强制逻辑属性名称
        private string forcePropertyName = "";
        //强制逻辑属性类型
        private CSharpDataType forcePropertyType = null;

        public string ParaName
        {
            get { return this.paraName; }
            set { this.paraName = value; }
        }

        public ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator.CSharpDataType ForcePropertyType
        {
            get { return this.forcePropertyType; }
            set { this.forcePropertyType = value; }
        }

        public string ForcePropertyName
        {
            get { return this.forcePropertyName; }
            set { this.forcePropertyName = value; }
        }

        public ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator.CSharpDataType PropertyType
        {
            get { return this.propertyType; }
            set { this.propertyType = value; }
        }

        public string PropertyName
        {
            get { return this.propertyName; }
            set { this.propertyName = value; }
        }

        public ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator.TableField TableField
        {
            get { return this.tableField; }
            set { this.tableField = value; }
        }

        //构造方法
        /// <summary>
        /// 将数据库表字段的内容映射到DAO类对应属性上
        /// </summary>
        private void GenerClassProperties(TableField _tableField)
        {
            if (_tableField == null) return;

            //属性名需要首字母小写，其他和字段名相同
            string fieldName = _tableField.FieldName;
            this.propertyName = char.ToLower(fieldName[0]) + fieldName.Substring(1);
            this.paraName = "_" + this.propertyName;
            //基本属性类型
            this.propertyType = TranslateSqlTypeToCSharpDataType.TranslateType(_tableField);
            //当字段为外码时，将外码参照的对象设置为强制逻辑属性
            if (_tableField.ReferFKTable != "")
            {
                this.forcePropertyName = "log_" + this.propertyName;
                this.forcePropertyType = new CSharpDataType(this.tableField.ReferFKTable, "null");
            }
        }

        public CSharpClassProperty(TableField _tableField, CSharpDataType _forcePropertyType)
        {
            this.tableField = _tableField;
            //将_tableField属性映射到ClassProperty属性上
            GenerClassProperties(_tableField);
            //设置强制逻辑属性
            if (_forcePropertyType != null)
            {
                this.forcePropertyName = "log_" + this.propertyName;
                this.forcePropertyType = _forcePropertyType;
            }
        }
        /// <summary>
        /// DAO类的辅助属性，并没有和数据库表字段关联
        /// </summary>
        /// <param name="_propertyName"></param>
        /// <param name="_propertyType"></param>
        public CSharpClassProperty(string _propertyName, CSharpDataType _propertyType)
        {
            this.propertyName = _propertyName;
            this.propertyType = _propertyType;
        }

        #region 生成对象属性相关的DAO代码
        /// <summary>
        /// 生成字段对应的DAO私有属性
        /// 形如：private string mtelephone = "";
        /// *如果有强制逻辑类型，私有属性应该多一句，包括逻辑属性，例如
        /// private bool log_phoneValid = false;
        /// *如果是外码，属性类型应该为外码的表名，强制逻辑类型不受影响
        /// </summary>
        /// <returns></returns>
        public string GenerPropertyDefine()
        {
            StringBuilder sbd = new StringBuilder();
            string str1 = "private {0} {1} = {2};";
            str1 = string.Format(str1, this.propertyType.TypeName, this.propertyName, this.propertyType.DefaultValue);
            sbd.AppendLine(str1);
            //含有强制属性类型，这时包括了含有外码约束的情形
            if (this.forcePropertyType != null)
            {
                string str2 = "private {0} {1} = {2};";
                str2 = string.Format(str2, this.forcePropertyType.TypeName, this.forcePropertyName, this.forcePropertyType.DefaultValue);
                sbd.AppendLine(str2);
            }
            return sbd.ToString();
        }
        /// <summary>
        /// 生成属性访问代码
        /// </summary>
        /// <returns></returns>
        public string GenerPropertyAccess()
        {
            StringBuilder sbd = new StringBuilder();
            //访问属性名：首字母大写
            string AccessPropertyName = char.ToUpper(this.propertyName[0]) + this.propertyName.Substring(1);

            if (this.tableField != null && this.tableField.ReferFKTable != "")
            { //有外码约束的时候，就不考虑其他情况了
                if (this.tableField.CanNull)
                { //字段可以为空时，需要加入判空语句
                    /*
                    public FishKind BabyFishKind
                    {
	                    get { return log_babyFishKind; }
                    }
                    public void SetBabyFishKind(FishKind _value)
                    {
	                    //首先给逻辑属性赋值
	                    log_friendFishKind = _value;
	                    //其次进行转换
	                    if(_value == null)
	                    {
		                    friendFishKind = null;
	                    }
	                    else
	                    {
		                    friendFishKind = _value.ID;
	                    }
                    }
                    private void TransFriendFishKind()
                    {
	                    //执行转换
	                    if(friendFishKind == null) log_friendFishKind = null;
	                    else log_friendFishKind = new FishKind(friendFishKind);
                    }
                    */
                    string str1 = @"public {0} {1}
                                    {{
	                                    get {{ return {3}; }}
                                    }}";
                    str1 = string.Format(str1, this.tableField.ReferFKTable, AccessPropertyName, this.forcePropertyName);
                    string str2 = @"public void Set{0}({1} _value)
                                    {{
	                                    //首先给逻辑属性赋值
	                                    {2} = _value;
	                                    //其次进行转换
	                                    if(_value == null)
	                                    {{
		                                    {3} = null;
	                                    }}
	                                    else
	                                    {{
		                                    {3} = _value.{4};
	                                    }}
                                    }}";
                    str2 = string.Format(str2, AccessPropertyName, this.forcePropertyType.TypeName, this.forcePropertyName,
                        this.propertyName, this.tableField.ReferFKField);
                    string str3 = @"private void Trans{0}()
                                    {{
	                                    //执行转换
	                                    if({3} == null) {1} = null;
	                                    else {1} = new {2}({3});
                                    }}
                                    */";
                    str3 = string.Format(str3, AccessPropertyName, this.forcePropertyName, this.forcePropertyType.TypeName, this.propertyName);
                    sbd.AppendLine(str1).AppendLine(str2).AppendLine(str3);
                }
                else
                {
                    /*
                    public FishKind FriendFishKind
                    {
                        get { return log_friendFishKind; }
                    }
                    public void SetFriendFishKind(FishKind _value)
                    {
                        log_friendFishKind = _value;
                        friendFishKind = _value.ID;
                    }
                    private void TransFriendFishKind()
                    {
                        log_friendFishKind = new FishKind(friendFishKind);
                    }
                    */
                    string str1 = @"public {0} {1}
                                    {{
	                                    get {{ return {2}; }}
                                    }}";
                    str1 = string.Format(str1, this.tableField.ReferFKTable, AccessPropertyName, this.forcePropertyName);
                    string str2 = "public void Set{0}({1} _value) \n {{ \n //首先给逻辑属性赋值\n {2} = _value; \n //其次进行转换\n {3} = _value.{4};\n }} \n";
                    str2 = string.Format(str2, AccessPropertyName, this.forcePropertyType.TypeName, this.forcePropertyName,
                        this.propertyName, this.tableField.ReferFKField);
                    string str3 = "private void Trans{0}()\n {{ \n //执行转换\n {1} = new {2}({3}); \n }} \n ";
                    str3 = string.Format(str3, AccessPropertyName, this.forcePropertyName, this.forcePropertyType.TypeName, this.propertyName);
                    sbd.Append(str1).Append(str2).Append(str3);
                }
            }
            else if (this.forcePropertyType == null)
            { //当不含有强制逻辑属性的时候，直接生成
                /*形如
                public String UserName
                {
                    get { return userName; }
                    set { userName = value; }
                }
                */
                string str1 = @"public {0} {1}
                                {{
                                    get {{ return {2}; }}
                                    set {{ {2} = value; }}
                                }}";
                str1 = string.Format(str1, this.propertyType.TypeName, AccessPropertyName, this.propertyName);
                sbd.Append(str1);
            }
            else
            { //含有强制逻辑属性的时候
                /*形如
                public bool IsEmailValid
                {
                    get { return log_isEmailValid; }
                }
                public void SetIsEmailValid(bool _value)
                {
                    //首先给逻辑属性赋值
                    log_isEmailValid = _value;
                    //其次进行转换
                    isEmailValid = _value?"是":"否";
                }
                private void TransIsEmailValid()
                {
                    //执行转换
                    log_isEmailValid = isEmailValid.Equals("是")?true:false;
                }
                */
                string str1 = @"public {0} {1}
                                {{
                                    get {{ return {2}; }}
                                }}";
                str1 = string.Format(str1, this.forcePropertyType.TypeName, AccessPropertyName, this.forcePropertyName);
                string str2 = "public void Set{0}({1} _value) \n {{ \n //首先给逻辑属性赋值\n {2} = _value; \n //其次进行转换\n {3} = 需用户填写;\n }} \n";
                str2 = string.Format(str2, AccessPropertyName, this.forcePropertyType.TypeName, this.forcePropertyName, this.propertyName);
                string str3 = "private void Trans{0}()\n {{ \n //执行转换\n {1} = 需用户填写; \n }} \n ";
                str3 = string.Format(str3, AccessPropertyName, this.forcePropertyName);
                sbd.Append(str1).Append(str2).Append(str3);
            }

            sbd.AppendLine("");
            return sbd.ToString();
        }
        /// <summary>
        /// 生成GetPropertiesBy方法中从数据库记录赋值的语句代码
        /// </summary>
        /// <returns></returns>
        public string GenerGetValueFromDatabase()
        {
            if (this.tableField == null) return "";

            StringBuilder sbd = new StringBuilder();
            string getValue = "";
            string fieldName = this.tableField.FieldName;
            //根据不同的属性类型，使用不同的取值语句
            if (this.propertyType.TypeName == "bool")
            {
                string str = "{0} = Convert.ToBoolean(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "bool?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = Convert.ToBoolean(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "int")
            {
                string str = "{0} = Convert.ToInt32(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "int?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = Convert.ToInt32(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "short")
            {
                string str = "{0} = Convert.ToInt16(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "short?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = Convert.ToInt16(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "long")
            {
                string str = "{0} = Convert.ToInt64(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "long?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = Convert.ToInt64(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "DateTime")
            {
                string str = "{0} = DateTime.Parse(dr[\"{1}\"].ToString().Trim());";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "DateTime?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = DateTime.Parse(dr[""{1}""].ToString().Trim());";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "float")
            {
                string str = "{0} = (float)Convert.ToDouble(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "float?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = (float)Convert.ToDouble(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "double")
            {
                string str = "{0} = (float)Convert.ToDouble(dr[\"{1}\"]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "double?")
            {
                string str = @" if (dr[""{1}""] == DBNull.Value) {0} = null;
                                else {0} = Convert.ToDouble(dr[""{1}""]);";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "byte[]")
            {
                string str = "{0} = (byte[])dr[\"{1}\"];";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            else if (this.propertyType.TypeName == "string")
            {
                string str = "{0} = dr[\"{1}\"].ToString().Trim();";
                getValue = string.Format(str, this.propertyName, fieldName);
            }
            sbd.AppendLine(getValue);
            if (this.forcePropertyType != null)
            {//有强制逻辑属性的时候，使用Trans方法将字段值转换为逻辑属性值
                sbd.AppendLine(string.Format("Trans{0}();", fieldName));
            }
            return sbd.ToString();
        }
        /// <summary>
        /// 生成字段对应的数据库语句参数，形如
        /// paras[0] = new SqlParameter("@userid", SqlDbType.VarChar, 20);
        /// paras[0].Value = userID;
        /// </summary>
        /// <param name="_index"></param>
        /// <returns></returns>
        public string GenerSQLParameter(int _index, SqlParameterValueMode _mode)
        {
            if (this.tableField == null) return "";
            
            StringBuilder sbd = new StringBuilder();
            //str1表示参数项初始化
            string str1 = "paras[{0}] = new SqlParameter(\"@{1}\", SqlDbType.{2}, {3});";
            str1 = string.Format(str1, _index, this.tableField.FieldName.ToLower(), this.tableField.GetFieldTypeStr(), 
                this.tableField.FieldSize);
            sbd.AppendLine(str1);
            //表示给参数赋值语句
            string str2 = "";
            string valueString = "";
            //对应两种模式，分别用在不同的场景
            if(_mode == SqlParameterValueMode.PropertyName)
                valueString = string.Format("{0}", this.propertyName);
            else if(_mode == SqlParameterValueMode.ParameterName)
                valueString = string.Format("{0}", this.paraName);
            if (this.tableField.CanNull)
            {
                /*
                paras[2] = new SqlParameter("@buyneedlevel", SqlDbType.Int, 4);
                if (buyNeedLevel == null) paras[2].Value = DBNull.Value;
                else paras[2].Value = buyNeedLevel;
                */
                str2 = @"
                    if ({0} == null) paras[{1}].Value = DBNull.Value;
                    else paras[{1}].Value = {0};";
                str2 = string.Format(str2, valueString, _index);
            }
            else
            {
                str2 = "paras[{0}].Value = {1};";
                str2 = string.Format(str2, _index, valueString);
            }
            sbd.AppendLine(str2);
            
            return sbd.ToString();
        }
        #endregion
    }
}
