using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyUsefulTools.DAO;

namespace MyUsefulTools.Forms.UnderWater
{
    public partial class FishKindManager : Form
    {
        public FishKindManager()
        {
            InitializeComponent();
        }

        private void FishKindManager_Load(object sender, EventArgs e)
        {
            dataGridView_fishKind.DataSource = UnderWaterFishInfo.GetAllRecordDataTable();
        }
        /// <summary>
        /// 保存更新，已存在的记录进行更新，未存在的记录进行保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_saveUpdate_Click(object sender, EventArgs e)
        {
            DataTable datatable = (DataTable)dataGridView_fishKind.DataSource;
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                DataRow dr = datatable.Rows[i];
                string KindName = "";
                int? BuyNeedLevel = null;
                float LifeLength, MakeCoinInterval, Level;
                try
                {
                    KindName = dr["KindName"].ToString().Trim();
                    Level = (float)Convert.ToDouble(dr["Level"]);
                    if (dr["BuyNeedLevel"] != DBNull.Value) BuyNeedLevel = Convert.ToInt32(dr["BuyNeedLevel"]);
                    LifeLength = (float)Convert.ToDouble(dr["LifeLength"]);
                    MakeCoinInterval = (float)Convert.ToDouble(dr["MakeCoinInterval"]);

                    if (dr["ID"] == DBNull.Value)
                    { //表名记录不存在，为新加的
                        try
                        {
                            UnderWaterFishInfo dao = new UnderWaterFishInfo();
                            dao.SetProperties(KindName, Level, BuyNeedLevel, LifeLength, MakeCoinInterval);
                            dao.InsertNewRecord();
                            //为了不重复添加，需要给新的行赋ID值
                            dr["ID"] = dao.ID;
                            MessageBox.Show("已插入：" + dao.KindName);
                        }
                        catch (Exception ex)
                        { //有任何问题都不插入此条记录，并写入日志文件中
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    { //表名是需要更新的记录

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }
        }
    }
}
