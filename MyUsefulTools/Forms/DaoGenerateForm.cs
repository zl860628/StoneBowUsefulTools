using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZLSpace.DataAccessTools;
using ZLSpace.DataAccessTools.DataBaseOperate.DAOGenerator;

namespace MyUsefulTools.Forms
{
    public partial class DaoGenerateForm : Form
    {
        public DaoGenerateForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream stream = new MemoryStream(Encoding.Default.GetBytes(richTextBox1.Text));
            StreamReader textStreamReader = new StreamReader(stream);
            DatabaseType databaseType = DatabaseType.SQLServer2008;
            PlaintextDataGetAdapter dataGetAdapter = new PlaintextDataGetAdapter(textStreamReader, databaseType);
            //获得数据库字段对应的DAO类属性
            List<CSharpClassProperty> classProperties = dataGetAdapter.GetCSharpClassPropertyList();
            //添加DAO类辅助属性
            CSharpClassProperty property_isRecord = new CSharpClassProperty("isRecord", CSharpDataType.GetType(CSharpDataTypeEnum.bool_value));
            classProperties.Add(property_isRecord);
            CSharpDAOGenerator daoGenerator = new CSharpDAOGenerator(tb_tableName.Text.Trim(), classProperties);
            richTextBox2.Text = daoGenerator.GenerDAOCode();
        }
    }
}
