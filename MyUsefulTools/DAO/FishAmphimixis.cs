using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DatabaseAccess;
using MySpace.Utils;

namespace MyUsefulTools.DAO
{
    public class FishAmphimixis
    {
        private int iD = -1;

        private int selfFishKind = -1;
        private UnderWaterFishInfo log_selfFishKind = null;

        private bool selfIsZhen = false;

        private int friendFishKind = -1;
        private UnderWaterFishInfo log_friendFishKind = null;

        private bool friendIsZhen = false;

        private int babyFishKind = -1;
        private UnderWaterFishInfo log_babyFishKind = null;

        private bool babyIsZhen = false;

        private DateTime insertDate = Constant.DateTime_MinValue;

        private bool isRecord = false;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public UnderWaterFishInfo SelfFishKind
        {
            get { return log_selfFishKind; }
        }
        public void SetSelfFishKind(UnderWaterFishInfo _value)
        {
            //首先给逻辑属性赋值
            log_selfFishKind = _value;
            //其次进行转换
            selfFishKind = _value.ID;
        }
        private void TransSelfFishKind()
        {
            //执行转换
            log_selfFishKind = new UnderWaterFishInfo(selfFishKind);
        }


        public bool SelfIsZhen
        {
            get { return selfIsZhen; }
            set { selfIsZhen = value; }
        }

        public UnderWaterFishInfo FriendFishKind
        {
            get { return log_friendFishKind; }
        }
        public void SetFriendFishKind(UnderWaterFishInfo _value)
        {
            //首先给逻辑属性赋值
            log_friendFishKind = _value;
            //其次进行转换
            friendFishKind = _value.ID;
        }
        private void TransFriendFishKind()
        {
            //执行转换
            log_friendFishKind = new UnderWaterFishInfo(friendFishKind);
        }


        public bool FriendIsZhen
        {
            get { return friendIsZhen; }
            set { friendIsZhen = value; }
        }

        public UnderWaterFishInfo BabyFishKind
        {
            get { return log_babyFishKind; }
        }
        public void SetBabyFishKind(UnderWaterFishInfo _value)
        {
            //首先给逻辑属性赋值
            log_babyFishKind = _value;
            //其次进行转换
            babyFishKind = _value.ID;
        }
        private void TransBabyFishKind()
        {
            //执行转换
            log_babyFishKind = new UnderWaterFishInfo(babyFishKind);
        }


        public bool BabyIsZhen
        {
            get { return babyIsZhen; }
            set { babyIsZhen = value; }
        }

        public DateTime InsertDate
        {
            get { return insertDate; }
            set { insertDate = value; }
        }

        public bool IsRecord
        {
            get { return isRecord; }
            set { isRecord = value; }
        }

        /// <summary>
        /// 根据ID获取对象属性
        /// </summary>
        private void GetPropertiesByID(int _id)
        {
            string sqlstr = "select * from [FishAmphimixis] where ID=@id;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", SqlDbType.Int);
            paras[0].Value = _id;
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

                selfFishKind = Convert.ToInt32(dr["SelfFishKind"]);
                TransSelfFishKind();

                selfIsZhen = Convert.ToBoolean(dr["SelfIsZhen"]);

                friendFishKind = Convert.ToInt32(dr["FriendFishKind"]);
                TransFriendFishKind();

                friendIsZhen = Convert.ToBoolean(dr["FriendIsZhen"]);

                babyFishKind = Convert.ToInt32(dr["BabyFishKind"]);
                TransBabyFishKind();

                babyIsZhen = Convert.ToBoolean(dr["BabyIsZhen"]);

                insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

                this.isRecord = true;
            }
        }
        public FishAmphimixis()
        { }

        public FishAmphimixis(int _id)
        {
            GetPropertiesByID(_id);
        }

        public void SetProperties(UnderWaterFishInfo _selfFishKind, bool _selfIsZhen, UnderWaterFishInfo _friendFishKind,
            bool _friendIsZhen, UnderWaterFishInfo _babyFishKind, bool _babyIsZhen, DateTime _insertDate)
        {
            SetSelfFishKind(_selfFishKind);
            SelfIsZhen = _selfIsZhen;
            SetFriendFishKind(_friendFishKind);
            FriendIsZhen = _friendIsZhen;
            SetBabyFishKind(_babyFishKind);
            BabyIsZhen = _babyIsZhen;
            InsertDate = _insertDate;
        }

        public void InsertNewRecord()
        {
            if (isRecord) throw new Exception("记录重复");

            string sqlstr = "insert into [FishAmphimixis] values(@selffishkind,@selfiszhen,@friendfishkind,@friendiszhen,@babyfishkind,@babyiszhen,@insertdate);";

            SqlParameter[] paras = new SqlParameter[7];
            paras[0] = new SqlParameter("@selffishkind", SqlDbType.Int, 4);
            paras[0].Value = selfFishKind;

            paras[1] = new SqlParameter("@selfiszhen", SqlDbType.Bit, 1);
            paras[1].Value = selfIsZhen;

            paras[2] = new SqlParameter("@friendfishkind", SqlDbType.Int, 4);
            paras[2].Value = friendFishKind;

            paras[3] = new SqlParameter("@friendiszhen", SqlDbType.Bit, 1);
            paras[3].Value = friendIsZhen;

            paras[4] = new SqlParameter("@babyfishkind", SqlDbType.Int, 4);
            paras[4].Value = babyFishKind;

            paras[5] = new SqlParameter("@babyiszhen", SqlDbType.Bit, 1);
            paras[5].Value = babyIsZhen;

            paras[6] = new SqlParameter("@insertdate", SqlDbType.DateTime, 8);
            paras[6].Value = insertDate;

            DBManager.InsertRecord(sqlstr, paras);

            sqlstr = "select ID from [FishAmphimixis] where SelfFishKind=@selffishkind and SelfIsZhen=@selfiszhen and FriendFishKind=@friendfishkind and FriendIsZhen=@friendiszhen and BabyFishKind=@babyfishkind and BabyIsZhen=@babyiszhen and InsertDate=@insertdate;";
            DataTable dt = DBManager.SelectRecords(sqlstr, paras);
            iD = (int)dt.Rows[0][0];

            isRecord = true;
        }
        //尾部手工添加代码
        public static List<FishAmphimixis> GetEntitiesFromDataTable(DataTable _dt)
        {
            List<FishAmphimixis> entities = new List<FishAmphimixis>();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {   
                DataRow dr = _dt.Rows[i];
                FishAmphimixis entity = new FishAmphimixis();
                entity.iD = Convert.ToInt32(dr["ID"]);

                entity.selfFishKind = Convert.ToInt32(dr["SelfFishKind"]);
                entity.TransSelfFishKind();

                entity.selfIsZhen = Convert.ToBoolean(dr["SelfIsZhen"]);

                entity.friendFishKind = Convert.ToInt32(dr["FriendFishKind"]);
                entity.TransFriendFishKind();

                entity.friendIsZhen = Convert.ToBoolean(dr["FriendIsZhen"]);

                entity.babyFishKind = Convert.ToInt32(dr["BabyFishKind"]);
                entity.TransBabyFishKind();

                entity.babyIsZhen = Convert.ToBoolean(dr["BabyIsZhen"]);

                entity.insertDate = DateTime.Parse(dr["InsertDate"].ToString().Trim());

                entity.isRecord = true;

                entities.Add(entity);
            }
            return entities;
        }
        public static void DeleteRecordByID(int _id)
        {
            string sqlstr = "delete from [FishAmphimixis] where ID=@id;";
            SqlParameter[] paras = new SqlParameter[1];
            paras[0] = new SqlParameter("@id", SqlDbType.Int, 4);
            paras[0].Value = _id;
            DBManager.DeleteRecords(sqlstr, paras);
        }
    }
}
