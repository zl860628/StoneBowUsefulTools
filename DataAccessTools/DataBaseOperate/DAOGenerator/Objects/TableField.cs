using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator
{
    /// <summary>
    /// 表示数据库中的表字段，包含对应DAO类属性
    /// </summary>
    public class TableField
    {
        #region 私有属性
        //所属数据库的类型
        private DatabaseType theDatabaseType = DatabaseType.SQLServer2008;
        //字段名
        private string fieldName = "";
        //字段类型，实际上为枚举类型，为了多种枚举类型可以通用，故使用int类型
        private int fieldType = -1;
        //字段数据长度
        private int fieldSize = 0;
        //是否为主码
        private bool isPrimeKey = false;
        //唯一组名称
        private string uniqueGroupName = "";
        //是否为标识ID
        private bool isIdentifier = false;
        //是否允许空
        private bool canNull = false;
        //外码参照的表名
        private string referFKTable = "";
        //外码参照的字段名
        private string referFKField = "";

        public string UniqueGroupName
        {
            get { return this.uniqueGroupName; }
            set { this.uniqueGroupName = value; }
        }

        public DatabaseType TheDatabaseType
        {
            get { return this.theDatabaseType; }
            set { this.theDatabaseType = value; }
        }

        public bool IsIdentifier
        {
            get { return this.isIdentifier; }
            set { this.isIdentifier = value; }
        }

        public bool CanNull
        {
            get { return this.canNull; }
            set { this.canNull = value; }
        }

        public string ReferFKField
        {
            get { return this.referFKField; }
            set { this.referFKField = value; }
        }

        public string ReferFKTable
        {
            get { return this.referFKTable; }
            set { this.referFKTable = value; }
        }

        public bool IsPrimeKey
        {
            get { return this.isPrimeKey; }
            set { this.isPrimeKey = value; }
        }

        public int FieldSize
        {
            get { return this.fieldSize; }
            set { this.fieldSize = value; }
        }

        public int FieldType
        {
            get { return this.fieldType; }
            set { this.fieldType = value; }
        }

        public string FieldName
        {
            get { return this.fieldName; }
            set { this.fieldName = value; }
        }

        #endregion
        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public TableField(DatabaseType _databaseType, string _fieldName, int _fieldType, int _fieldSize, bool _isPrimeKey, 
            string _uniqueGroupName, bool _isIdentifier, bool _canNull, string _referFKTable, string _referFKField)
        {
            this.theDatabaseType = _databaseType;
            this.fieldName = _fieldName;
            this.fieldType = _fieldType;
            this.fieldSize = _fieldSize;
            this.isPrimeKey = _isPrimeKey;
            this.uniqueGroupName = _uniqueGroupName;
            this.isIdentifier = _isIdentifier;
            this.canNull = _canNull;
            this.referFKTable = _referFKTable;
            this.referFKField = _referFKField;
        }

        #endregion
        public string GetFieldTypeStr()
        {
            string typestr = "";
            switch (this.theDatabaseType)
            {
                case DatabaseType.SQLServer2008:
                    {
                        SQLServer2008DataType datatype = (SQLServer2008DataType)this.fieldType;
                        typestr = datatype.ToString();
                        break;
                    }
            }
            return typestr;
        }
    }
}
