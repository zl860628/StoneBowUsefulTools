using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DatabaseAccess;
using MySpace.Utils;
using DataBaseOperate.DataStructure;
using StoneUtils.Interface;

namespace MyUsefulTools.DAO
{
    public class JingDongGoodsKind : ITreeNodeData
    {
        private int iD = -1;

        private string name = null;

        private int amount = -1;

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        /// <summary>
        /// 根据唯一属性组获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _iD)
        {
            string sqlstr = "select * from [JingDongGoodsKind] where ID=@id;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            paras[0].Value = _iD;

            DataTable dt = DBManager.SelectRecords(sqlstr, paras);

            if (dt.Rows.Count == 0)
            {
                this.isRecord = false;
                return;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                iD = Convert.ToInt32(dr["ID"]);

                name = dr["Name"].ToString().Trim();

                amount = Convert.ToInt32(dr["Amount"]);

                this.isRecord = true;
            }
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [JingDongGoodsKind] values(@name,@amount);";

            SqlParameter[] paras = new SqlParameter[2];
            paras[0] = new SqlParameter("@name", SqlDbType.NVarChar, 100);
            paras[0].Value = name;

            paras[1] = new SqlParameter("@amount", SqlDbType.Int, 4);
            paras[1].Value = amount;

            DBManager.InsertRecord(sqlstr, paras);
            sqlstr = "select ID from [JingDongGoodsKind] where Name=@name and Amount=@amount;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;

        }

        //手动添加方法
        //构造方法
        public JingDongGoodsKind(int _id)
        {
            GetPropertiesByID(_id);
        }
        public JingDongGoodsKind(string _name)
        {
            this.name = _name;
            this.amount = 0;
        }
        public JingDongGoodsKind(string _name, int _amount)
            : this(_name)
        {
            this.amount = _amount;
        }
        //重载方法
        public override string ToString()
        {
            return name;
        }
    }
}
