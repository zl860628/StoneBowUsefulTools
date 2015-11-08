using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using MySpace.Utils;
using MyUsefulTools.Utility;
using MyUsefulTools.Utility.FileOperator;
using MyUsefulTools.Utility.HtmlParse;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Nodes;
using Winista.Text.HtmlParser.Util;
using System.Diagnostics;
using System.Reflection;
using DatabaseAccess;
using MyUsefulTools.DAO;
using System.Drawing;
using gma.System.Windows;
using StoneBow.DataStructure.Tree;
using System.Collections;
using System.Globalization;
using System.Threading;
using SevenZip;
using System.Collections.ObjectModel;

namespace MyUsefulTools.Test
{
    public partial class TestForm : Form
    {
        private string oldmessage = "";
        private int warncount = 0;

        public TestForm()
        {
            InitializeComponent();
        }


        //测试的内容
        /// <summary>
        /// 测试从京东网页上获取数据
        /// </summary>
        private void test001()
        {
            //准备数据存储表
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("ImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("PriceImageBytes", typeof(byte[])));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("InsertDate", typeof(DateTime)));
            //获取网页页面HTML代码
            string _url = "http://www.360buy.com/special.aspx?id=2&page=1";
            string html = "";//JingDongNewGoodsParser.GetPageHTML(_url);
            //下面解析HTML文档，并定位有用信息位置
            Lexer lexer = new Lexer(html);
            Parser parser = new Parser(lexer);
            //新建分析文档需要的属性筛选器
            HasAttributeFilter attrFilter = new HasAttributeFilter();
            attrFilter.AttributeName = "id";
            attrFilter.AttributeValue = "plist";
            HasAttributeFilter attrFilter_img = new HasAttributeFilter();
            attrFilter_img.AttributeName = "class";
            attrFilter_img.AttributeValue = "p-img";
            HasAttributeFilter attrFilter_name = new HasAttributeFilter();
            attrFilter_name.AttributeName = "class";
            attrFilter_name.AttributeValue = "p-name";
            HasAttributeFilter attrFilter_price = new HasAttributeFilter();
            attrFilter_price.AttributeName = "class";
            attrFilter_price.AttributeValue = "p-price";
            //应用第一步筛选器，得到需要的局部HTML
            NodeList htmlNodes = parser.Parse(attrFilter);
            INode listnode = htmlNodes[0];
            //将局部HTML作为文档对象进一步分析每个元素
            html = listnode.ToHtml();
            lexer = new Lexer(html);
            parser = new Parser(lexer);
            TagNameFilter tagnameFilter = new TagNameFilter("li");
            htmlNodes = parser.Parse(tagnameFilter);
            //分析第一个元素
            for (int i = 0; i < 1; i++)
            {
                string name, description, imageurl, priceimgurl;
                byte[] imageBytes = new byte[0];
                byte[] priceImgBytes = new byte[0];//用来存储图片的字节形式

                INode node = htmlNodes[i];
                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode testNode = (TagNode)parser.Parse(attrFilter_img)[0].Children[0];
                richTextBox1.Text = testNode.Attributes["HREF"].ToString();

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode imgNode = (TagNode)parser.Parse(attrFilter_img)[0].Children[0].Children[0];
                name = imgNode.Attributes["ALT"].ToString();//获得商品名称
                imageurl = imgNode.Attributes["SRC"].ToString();//获得商品图片URL

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                description = parser.Parse(attrFilter_name)[0].ToPlainTextString();//获得商品描述

                lexer = new Lexer(node.ToHtml());
                parser = new Parser(lexer);
                TagNode priceNode = (TagNode)parser.Parse(attrFilter_price)[0].Children[1];
                priceimgurl = priceNode.Attributes["SRC"].ToString();//获得商品价格图片URL
                //根据图片URL，获取对应的图片字节数组
                try
                {
                    imageBytes = internetTransport.GetImageByteArrayFromUrl(imageurl);
                    priceImgBytes = internetTransport.GetImageByteArrayFromUrl(priceimgurl);
                }
                catch (WebException webex)
                {
                    Console.WriteLine(webex.Message);
                }
                //将对应信息加入到表的一行中
                DataRow dr = dt.NewRow();
                dr["Name"] = name;
                dr["ImageBytes"] = imageBytes;
                dr["PriceImageBytes"] = priceImgBytes;
                dr["Description"] = description;
                dr["InsertDate"] = DateTime.Now;
                dt.Rows.Add(dr);
            }
        }

        /// <summary>
        /// 将down回来的拼音文本文件转换成自己的格式，最初使用自定义xml格式存储
        /// </summary>
        private void test002()
        {
            /*将如下格式转换为
             a => '啊阿呵吖嗄腌锕錒',
             ai => '爱矮挨哎碍癌艾唉哀蔼隘埃皑呆嗌嫒瑷暧捱砹',
             * 自定义的xml格式
             <char>
                <zi>张</zi>
                <py>zhang</py>
             </char>
             */
            //首先准备XML文档
            XmlDocument xmldoc = new XmlDocument();
            XmlNode rootnode = xmldoc.CreateNode(XmlNodeType.Element, "root", "");
            xmldoc.AppendChild(rootnode);
            StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "files/pinyinorgin.txt", Encoding.Default);
            string sline = "";
            while ((sline = sr.ReadLine()) != null)
            {
                string[] parts = sline.Split(new string[] { "=>", " ", "'", "," }, StringSplitOptions.RemoveEmptyEntries);
                string py = parts[0];
                string zis = parts.Length > 1 ? parts[1] : "";//右侧的所有字
                Console.WriteLine(py);
                for (int i = 0; i < zis.Length; i++)
                {
                    //多音字的时候，合并字的读音，先寻找当前的字以前是否出现过
                    XmlNode duoyin_charnode = rootnode.SelectSingleNode("./char[zi='" + zis[i] + "']");
                    if (duoyin_charnode == null)
                    {
                        XmlNode charnode = xmldoc.CreateNode(XmlNodeType.Element, "char", "");
                        XmlNode zinode = xmldoc.CreateNode(XmlNodeType.Element, "zi", "");
                        zinode.InnerText = zis[i].ToString();
                        charnode.AppendChild(zinode);
                        XmlNode pynode = xmldoc.CreateNode(XmlNodeType.Element, "py", "");
                        pynode.InnerText = py;
                        charnode.AppendChild(pynode);
                        rootnode.AppendChild(charnode);
                    }
                    else
                    {//为多音字的时候
                        XmlNode pynode = xmldoc.CreateNode(XmlNodeType.Element, "py", "");
                        pynode.InnerText = py;
                        duoyin_charnode.AppendChild(pynode);
                    }
                }
            }
            sr.Close();
            xmldoc.Save(System.AppDomain.CurrentDomain.BaseDirectory + "files/pinyin.xml");
        }

        private void test003()
        {
            List<string> strlist = new List<string>();
            strlist.Add("aaa");
            richTextBox1.Text = strlist.GetType().ToString();
            string[] strarray = new string[1];
            strarray[0] = "aaa";
            richTextBox1.Text += strarray.GetType().ToString();
            //MessageBox.Show(strlist.GetType().Equals(typeof(string[])).ToString());
        }
        /// <summary>
        /// 测试PinyinTools的相关功能
        /// </summary>
        private void test004()
        {
            DateTime time1 = DateTime.Now;
            PinyinTools pinyintools = new PinyinTools();
            DateTime time2 = DateTime.Now;
            richTextBox1.Text += "初始化时间：" + (time2 - time1).ToString() + "\n";
            string[] pinyinstrs = pinyintools.GetPinyin("陶");
            if (pinyinstrs != null) richTextBox1.Text += string.Join(";", pinyinstrs);
            else richTextBox1.Text += "null";
        }
        /// <summary>
        /// 测试int?能不能和null比较
        /// </summary>
        private void test005()
        {
            int? a = null;
            int? b = null;
            a = 12;
            a = b;
            if (a == null) MessageBox.Show("null");
            else MessageBox.Show("not null");
            if (a.HasValue) MessageBox.Show("hasvalue");
        }
        private void test006()
        {
            DateTime? datetime = null;
            if (datetime == null)
            {
                MessageBox.Show("DateTime也可以使用Nullable了啊！");
            }
        }
        private void test007()
        {
            PinyinDeal.TransFormat();
            MessageBox.Show("完成");
        }
        /// <summary>
        /// 从配置文件中获取需要的网址
        /// </summary>
        /// <param name="_filepath"></param>
        /// <returns></returns>
        private List<string> GetUrlsFromFils(string _filepath)
        {
            List<string> urlStrs = new List<string>();
            StreamReader sr = new StreamReader(_filepath, Encoding.Default);
            string linestr;
            while ((linestr = sr.ReadLine()) != null)
            {
                //对配置字符串进行解释，只支持一个{}标记
                //http://www.360buy.com/special.aspx?id=5&page={1-20}表示加入page=1到page=20共20个网址
                Regex r = new Regex(@"{\d+-\d+}");
                if (r.IsMatch(linestr))
                {
                    MatchCollection mc = r.Matches(linestr);
                    string mcstr = mc[0].Value.Replace("{", "").Replace("}", "");
                    string[] mcstrs = mcstr.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    int fromnum = Convert.ToInt32(mcstrs[0]);
                    int tonum = Convert.ToInt32(mcstrs[1]);
                    string linestr2 = r.Replace(linestr, "stonebow1");
                    for (int i = fromnum; i <= tonum; i++)
                    {
                        urlStrs.Add(linestr2.Replace("stonebow1", i.ToString()));
                    }
                }
                else
                {
                    urlStrs.Add(linestr);
                }
            }
            return urlStrs;
        }
        private void test008()
        {
            List<string> urlStrs = GetUrlsFromFils(System.AppDomain.CurrentDomain.BaseDirectory + @"\files\JingDongNewGoodsUrls.txt");
            foreach (string str in urlStrs)
            {
                richTextBox1.AppendText(str + "\n");
            }
        }
        /// <summary>
        /// 将resltpinyin.txt文件和table_ISCCD.txt文件合并生成自己需要的拼音文件格式
        /// table_ISCCD文件每行取第一个字，然后拼上resltpinyin对应行的拼音
        /// </summary>
        private void test009()
        {
            StreamReader srzi = new StreamReader("./files/table_ISCCD.txt", Encoding.Default);
            StreamReader srpy = new StreamReader("./files/resltpinyin.txt", Encoding.Default);
            string ziline = "", pyline = "";
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            while (true)
            {
                ziline = srzi.ReadLine();
                if (ziline == null) break;
                pyline = srpy.ReadLine();
                //string newline = ziline[0] + pyline;
                string[] pinyinstrs = pyline.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                List<string> pinyin = new List<string>(pinyinstrs);
                dictionary.Add(ziline[0].ToString(), pinyin);
            }
            srpy.Close();
            srzi.Close();
            PinyinTools.CreateZiToPinyinXmlFile(Constant.ZiToPinyinFilePath, dictionary);
        }
        /// <summary>
        /// 获取中国天气网的天气数据
        /// </summary>
        private string getWeatherContent()
        {
            string url = "http://flash.weather.com.cn/sk2/101010300.xml";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";    //post
            request.ContentType = "application/xml";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string htmlstr = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            return htmlstr;
        }
        private void test010()
        {
            richTextBox1.Text = getWeatherContent();
        }
        /// <summary>
        /// 测试CSharpUtility.GetContent方法
        /// </summary>
        private void test011()
        {
            string[] items = null;
            string returnstr = CSharpUtility.GetContent(getWeatherContent(), "<qw", "/>", 1, out items);
            for (int i = 0; i < items.Length; i++)
            {
                richTextBox1.AppendText(i.ToString() + items[i] + "\n");
            }
            richTextBox1.AppendText(returnstr);
        }
        /// <summary>
        /// 测试获取函数调用堆栈的信息的方法
        /// </summary>
        private void test012()
        {
            StackTrace st = new StackTrace(true);
            for (int i = 0; i < st.FrameCount; i++)
            {
                richTextBox1.AppendText(st.GetFrame(i).GetMethod().DeclaringType.ToString());
                richTextBox1.AppendText("\n");
            }
        }
        private void test013()
        {
            //StackTrace st = new StackTrace(true);
            //Type classType = st.GetFrame(1).GetMethod().DeclaringType;
            //object typeObject = classType.Assembly.CreateInstance(classType.FullName);
            //classType.InvokeMember(st.GetFrame(1).GetMethod().Name, BindingFlags.Public | BindingFlags.InvokeMethod, null , typeObject, null);
            string typeName = "MyUsefulTools.Forms.Weather.GetWeatherXml";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type classType = assembly.GetType(typeName);
            MethodInfo method = classType.GetMethod("btn_getFromWeb_Click");
            object typeObject = classType.Assembly.CreateInstance(classType.FullName);
            method.Invoke(typeObject, new object[] { null, null }); //实例方法的调用
        }
        /// <summary>
        /// 测试抓取未读的钱50个网页的上架时间
        /// </summary>
        private void test014()
        {
            string sqlstr = "select top 50 * from JingDongNewGoods where HasRead='否'";
            DataTable dt = DBManager.SelectRecords(sqlstr, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                int id = (int)dr["ID"];
                string url = dr["WebUrl"].ToString();
                string html = Utility.HtmlParse.HTMLCommonParser.GetAndGetHTML(url, null);
                string content = Utility.CSharpUtility.GetContent(html, "<li>上架时间：", "</li>", 1);
                try
                {
                    DateTime onsaleDate = DateTime.Parse(content);
                    JingDongNewGoodsDAO.UpdateBeginSaleDateByID(id, onsaleDate);
                    richTextBox1.AppendText(i.ToString() + "\n");
                }
                catch (Exception ex)
                { }
            }
        }
        /// <summary>
        /// 测试全局按键钩子
        /// 调试的时候会抛出异常，但是直接运行的时候可以成功。问题解决：
        /// The compiled release version works well so that cannot be a .NET 2.0 problem. To workaround, you just need to uncheck the check box in the project properties that says: "Enable Visual Studio hosting process". In the menu: Project -> Project Properties... -> Debug -> Enable the Visual Studio hosting process.
        /// </summary>
        UserActivityHook actHook;

        private void test015()
        {
            try
            {
                actHook = new UserActivityHook(); // crate an instance with global hooks
                // hang on events
                actHook.KeyDown += new KeyEventHandler(MyKeyDown);
                actHook.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            richTextBox1.Text = e.KeyValue.ToString() + "   " + e.KeyData.ToString() + "   " + e.KeyCode.ToString();
        }
        /// <summary>
        /// 测试GetQueryStringFromUrl方法
        /// </summary>
        private void test016()
        {
            string url = "http://match.sports.sina.com.cn/livecast/basketball/live.php?id=11734";
            MessageBox.Show(GetQueryStringFromUrl(url, "ID"));
        }
        private string GetQueryStringFromUrl(string _url, string _queryKey)
        {
            int wenhao_index = _url.IndexOf('?');
            string temp = _url.Substring(wenhao_index + 1);
            string[] pairs = temp.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pairs.Length; i++)
            {
                string[] values = pairs[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                //values[0]为key,values[1]为value
                values[0] = values[0].Trim().ToLower();
                values[1] = values[1].Trim();
                if (values[0].Equals(_queryKey.ToLower()))
                {
                    return values[1];
                }
            }
            return null;
        }

        public static InternetTransport internetTransport;
        /// <summary>
        /// 测试静态变量的生命周期，就是静态变量何时执行析构方法
        /// </summary>
        private void test017()
        {
            internetTransport = new InternetTransport("test1");
            internetTransport = new InternetTransport("test2");
            internetTransport = new InternetTransport("test3");
            internetTransport = new InternetTransport("test4");
        }
        /// <summary>
        /// 测试传参表名创建数据库表
        /// </summary>
        private void test018()
        {
            DataBaseOperate.DataStructure.Tree.Mssql2008Operator treeDataHandler =
                new DataBaseOperate.DataStructure.Tree.Mssql2008Operator(Constant.DefaultDatabaseConnectionString, "tree_school");
            treeDataHandler.CreateDefaultTable();
        }
        /// <summary>
        /// 测试建立树形分类的方法
        /// </summary>
        public void test019()
        {
            TreeHandler treeHandler = new TreeHandler(Constant.DefaultDatabaseConnectionString, "JingDongGoodsKind");
            //准备新的节点
            string[] kindNames = { "北京化工大学", "北京工商大学", "北京大学" };
            for (int i = 0; i < kindNames.Length; i++)
            {
                JingDongGoodsKind newkind = new JingDongGoodsKind(kindNames[i]);
                newkind.InsertNewRecord();
                treeHandler.AddNewNode(newkind);
            }
        }
        /// <summary>
        /// 显示TreeHandler建立的树
        /// </summary>
        public void test020()
        {
            TreeHandler treeHandler = new TreeHandler(Constant.DefaultDatabaseConnectionString, "JingDongGoodsKind");
            treeHandler.InitTreeView(treeView1);
        }
        /// <summary>
        /// 测试base64编码
        /// </summary>
        public void test021()
        {
            byte[] bytes = Encoding.Default.GetBytes("helloworld");
            string str = Convert.ToBase64String(bytes);
            richTextBox1.Text = str;
        }
        /// <summary>
        /// 测试URL编码
        /// </summary>
        public void test022()
        {
            string str = textBox1.Text.Trim();
            byte[] byt = Encoding.Default.GetBytes(str);
            string desstr = "";
            for (int i = 0; i < byt.Length; i++)
            {
                desstr += "%";
                desstr += byt[i].ToString("x2");
            }
            richTextBox1.Text = desstr;
        }

        //测试seti@home
        public void test023()
        {
            //先登录论坛，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            string urlLogin = "http://www.primegrid.com/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            string html1 = HTMLCommonParser.PostAndGetHTML(urlLogin, cc, param);
            string url = "http://www.primegrid.com/results.php?userid=60141&offset=0&show_names=1&state=3";
            string html2 = HTMLCommonParser.GetAndGetHTML(url, cc);
            html2 = Utility.CSharpUtility.GetContent(html2, "class=bordered", "</table>", 1);
            html2 = Utility.CSharpUtility.GetContent(html2, "<tr", "</tr>", 2);

            richTextBox1.Text = html2;
        }
        //测试milkyway
        public void test024()
        { }
        //测试cas@home
        public void test025()
        {
            //先登录论坛，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            string urlLogin = "http://casathome.ihep.ac.cn/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            HTMLCommonParser.PostAndGetHTML(urlLogin, cc, param);
            string url = "http://casathome.ihep.ac.cn/results.php?userid=139&offset=0&show_names=1&state=3";
            string htmlCode = HTMLCommonParser.GetAndGetHTML(url, cc);
            string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
            string[] itemStrs;
            Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
            DataTable dt = new DataTable();
            dt.Columns.Add("TaskName", typeof(string));
            dt.Columns.Add("WuId", typeof(string));
            dt.Columns.Add("ComputerId", typeof(string));
            dt.Columns.Add("SentDate", typeof(DateTime));
            dt.Columns.Add("ReportedDate", typeof(DateTime));
            dt.Columns.Add("RunTime", typeof(float));
            dt.Columns.Add("CPUTime", typeof(float));
            dt.Columns.Add("Credit", typeof(float));
            dt.Columns.Add("Application", typeof(string));
            for (int i = 1; i < itemStrs.Length; i++)
            {
                DataRow dr = dt.NewRow();
                string[] drStrs;
                Utility.CSharpUtility.GetContent(itemStrs[i], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                dr["SentDate"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                dr["ReportedDate"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                dr["RunTime"] = float.Parse(drStrs[6].Trim());
                dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                dr["Credit"] = float.Parse(drStrs[8].Trim());
                dr["Application"] = drStrs[9].Trim();
                dt.Rows.Add(dr);
            }
            DataRow dr2 = dt.Rows[8];
            for (int i = 0; i < dr2.ItemArray.Length; i++)
            {
                richTextBox1.AppendText(dr2.ItemArray[i].ToString() + "\n");
            }
        }
        
        /// <summary>
        /// 测试string[,]二维数组
        /// </summary>
        public void test034()
        {
            //先对传入的开始、结束代码进行正则转义
            string[,] zhuanyi = new string[,] { 
                { ".", "\\u002E" } ,
                { "$", "\\u0024" } ,
                { "^", "\\u005E" } ,
                { "{", "\\u007B" } ,
                { "[", "\\u005B" } ,
                { "(", "\\u0028" } ,
                { "|", "\\u007C" } ,
                { ")", "\\u0029" } ,
                { "*", "\\u002A" } ,
                { "+", "\\u002B" } ,
                { "?", "\\u003F" } ,
                { "\\", "\\u005C" }
            };
            richTextBox1.Text = zhuanyi.GetLength(2).ToString();
        }
        /// <summary>
        /// 测试日期解析
        /// </summary>
        public void test035()
        {
            string datestr = "19 Dec 2010 16:36:00 +00:00";
            DateTime dateTime = DateTime.ParseExact(datestr, "dd MMM yyyy HH:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
            richTextBox1.Text = dateTime.ToString("yyyy MM dd HH:mm:ss zzz");
        }
        public void test036()
        {
            string temp = "<a href=\"show_host_detail?abc=1\">1213812</a>";
            temp = Utility.CSharpUtility.StripHTML(temp);
            richTextBox1.Text = temp;
        }
        public void test037()
        {
            string url = "http://flash.weather.com.cn/sk2/101030100.xml";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            StringBuilder resultSbd = new StringBuilder();
            string result = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.UserAgent = "Mozilla/5.0";

                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(stream, Encoding.UTF8);
                using (StreamWriter sw = new StreamWriter("I:\\weather.txt"))
                {
                    while (true)
                    {
                        int read = streamreader.Read();
                        if (read == -1) break;
                        sw.Write(read);
                        sw.Write(" ");
                        richTextBox1.AppendText(((char)read).ToString());
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                if (responseStream != null) responseStream.Close();
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
        }
        /// <summary>
        /// 测试SevenZipSharp类的使用方法
        /// </summary>
        public void test038()
        {
            SevenZipExtractor extractor = new SevenZipExtractor(@"C:\QQDownload\cddjr_A2.3_Gallery3D.apk");
            FileStream fileStream = new FileStream("AndroidManifest.xml", FileMode.Create);
            extractor.ExtractFile("AndroidManifest.xml", fileStream);
            fileStream.Close();

            extractor.Dispose();//AndroidManifest.xml
        }
        public void test039()
        {
            Process p = new Process();
            p.StartInfo.FileName = "java";
            p.StartInfo.Arguments = "-jar AXMLPrinter2.jar AndroidManifest.xml";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            //ps.Arguments = "-jar AXMLPrinter2.jar AndroidManifest.xml";
            //ps.CreateNoWindow = true;
            p.Start();
            //p.StandardInput.WriteLine(textBox1.Text);
            //p.StandardInput.WriteLine("exit");
            StreamReader myStreamReader = p.StandardOutput;
            // Read the standard output of the spawned process.
            string myString = myStreamReader.ReadToEnd();
            string errString = p.StandardError.ReadToEnd();
            richTextBox1.Text = myString;
            richTextBox1.AppendText("<h2>" + errString + "</h2>");
            p.Close();
        }
        public void test040()
        {
            long time = long.Parse(textBox1.Text);
            long second = time % 60;
            time /= 60;
            long minute = time % 60;
            time /= 60;
            long hour = time % 24;
            time /= 24;
            long day = time % 365;
            time /= 365;
            long year = time;
            richTextBox1.Text = year + "年" + day + "天" + hour + "小时" + minute + "分钟" + second + "秒";
        }
        public void test041()
        {
            long seconds = long.Parse(textBox1.Text);
            DateTime datetime = new DateTime(1970, 1, 1);
            datetime = datetime.AddSeconds(seconds);
            richTextBox1.Text = datetime.ToString();
        }
        /// <summary>
        /// 测试从world community grid获取积分数据
        /// </summary>
        public void test026()
        {
            InternetTransport transportTool = new InternetTransport("Test", 100);
            string url = "https://secure.worldcommunitygrid.org/j_security_check";
            string resulturl = "https://secure.worldcommunitygrid.org/ms/viewBoincResults.do?filterDevice=0&filterStatus=4&projectId=-1&sortBy=returnedTime&pageNum=1";
            //string resulturl = "https://secure.worldcommunitygrid.org/ms/viewMyMemberPage.do";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream requestStream = null;
            CookieContainer cc = new CookieContainer();
            try
            {
                string formData = "j_username=zl860628&j_password=" + Constant.PassWord1;
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(formData);

                cc.Add(new Cookie("__utma", "2464182.1748491084.1301203514.1301203514.1301234717.2", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmz", "2464182.1301203514.1.1.utmccn=(direct)|utmcsr=(direct)|utmcmd=(none)", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmc", "2464182", "/", "secure.worldcommunitygrid.org"));
                cc.Add(new Cookie("__utmb", "2464182", "/", "secure.worldcommunitygrid.org"));

                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
                request.Accept = "text/html,application/xhtml+xml,*/*";
                request.ContentLength = data.Length;
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 100 * 1000;
                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "en";
                request.Headers["Accept-Charset"] = "GB2312,utf-8";
                request.Headers["Cookie"] = "__utma=2464182.1748491084.1301203514.1301203514.1301234717.2; __utmz=2464182.1301203514.1.1.utmccn=(direct)|utmcsr=(direct)|utmcmd=(none); __utmc=2464182; __utmb=2464182";
                //request.Headers["Cookie"] = "__utmb=2464182";
                requestStream = request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);

                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = cc.GetCookies(request.RequestUri);
                //将response.Headers["set-cookie"]中的值放到Cookie中
                //JSESSIONID=0000XThLYMFwIA-TMCeFSVeT5PY:13p0en2u3; Expires=Sun, 27 Mar 2011 17:10:50 GMT; Path=/; Domain=worldcommunitygrid.org
                //string[] cookiestrs = response.Headers["set-cookie"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                //string[] cookieNameValue = cookiestrs[0].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                //string[] cookiePath = cookiestrs[2].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                //string[] cookieDomain = cookiestrs[3].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                //Cookie cookie = new Cookie(cookieNameValue[0], cookieNameValue[1], cookiePath[1], cookieDomain[1]);
                //cookie.Expires = DateTime.Now.AddHours(3);
                //cc.Add(cookie);
                //获取登陆后的Cookie
                cc.Add(response.Cookies);

                //request = (HttpWebRequest)WebRequest.Create(url);
                //request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
                //request.Accept = "text/html,application/xhtml+xml,*/*";
                //request.ContentLength = data.Length;
                //request.CookieContainer = cc;
                //request.KeepAlive = true;
                //request.Timeout = 100 * 1000;
                //request.Headers["Accept-Encoding"] = "gzip, deflate";
                //request.Headers["Accept-Language"] = "zh-cn";
                //requestStream = request.GetRequestStream();
                //requestStream.Write(data, 0, data.Length);

                //response = (HttpWebResponse)request.GetResponse();
                ////获取登陆后的Cookie
                //cc.Add(response.Cookies);
                //CookieCollection collection = cc.GetCookies(new Uri("worldcommunitygrid.org"));
                //foreach (Cookie acookie in collection)
                { 
                    
                }
                //使用得到的Cookie，获取任务结果页面
                string html = transportTool.GetAndGetHTML(resulturl, cc, Encoding.Default);
                richTextBox1.Text = html;
            }
            catch (WebException ex)
            {
            }
            finally
            {
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
        }
        public void test027()
        {
            string url = "";
            WebClient webClient = new WebClient();
            webClient.Headers["User-Agent"] = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; Trident/4.0; Maxthon; GTB6.4; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1) ; SLCC1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.5.21022; .NET CLR 3.5.30729; InfoPath.2; .NET CLR 3.0.30729)";
            webClient.Headers["Accept-Language"] = "zh-CN";
            webClient.Headers["Accept-Encoding"] = "gzip, deflate";
            //webClient.Headers.Add(“Accept”, “image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, */*”);
            webClient.Credentials = CredentialCache.DefaultCredentials;
            byte[] data = webClient.DownloadData(url);
            string html = Encoding.UTF8.GetString(data);
            richTextBox1.Text = html;
        }
        public void test042()
        {
            richTextBox1.Text = "";
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                System.Diagnostics.PerformanceCounter CpuWatch = new System.Diagnostics.PerformanceCounter();//新建一个性能计数器
                //设置属性
                CpuWatch.CategoryName = "Process";
                CpuWatch.CounterName = "% Processor Time";
                CpuWatch.InstanceName = process.ProcessName;
                //显示cpu使用率,第二个参数就是取小数点几位，这里取2位
                richTextBox1.AppendText(process.ProcessName + " : " + CpuWatch.RawValue.ToString() + "\n");
            }
            //MessageBox.Show(Math.Round(CpuWatch.NextValue(), 2).ToString());
        }
        private void comparelottery(int[] a, int[] b, int aindex, int bindex, List<int> sameNumbers)
        {
            if (aindex >= a.Length || bindex >= b.Length)
            {
                return;
            }
            if (a[aindex] > b[bindex])
            {
                comparelottery(a, b, aindex, bindex + 1, sameNumbers);
            }
            else if (a[aindex] < b[bindex])
            {
                comparelottery(a, b, aindex + 1, bindex, sameNumbers);
            }
            else
            {
                sameNumbers.Add(a[aindex]);
                comparelottery(a, b, aindex + 1, bindex + 1, sameNumbers);
            }
        }
        public void test043()
        {
            int[] a1 = new int[] { 5, 6, 8, 18, 26 };
            int[] b1 = new int[] { 5, 7, 8, 18, 26};
            List<int> sameNumbers = new List<int>();
            comparelottery(a1, b1, 0, 0, sameNumbers);
            for (int i = 0; i < sameNumbers.Count; i++)
            {
                richTextBox1.AppendText(sameNumbers[i].ToString() + "\t");
            }
        }
        public void test044()
        {
            //先登录论坛，获取登陆后的Cookie信息
            CookieContainer cc = new CookieContainer();//this is for keep the Session and Cookie
            Hashtable param = new Hashtable();//this is for keep post data.
            string urlLogin = "http://einstein.phys.uwm.edu/login_action.php";
            //do find the elementId that needed. check the source of login page can get this information
            param.Add("next_url", "");
            param.Add("email_addr", "zl860628@gmail.com");
            param.Add("passwd", Constant.PassWord1);
            param.Add("stay_logged_in", "on");
            param.Add("mode", "%E7%99%BB%E5%BD%95");
            param.Add("cookietime", "2592000");
            HTMLCommonParser.PostAndGetHTML(urlLogin, cc, param);
            string url = "http://einstein.phys.uwm.edu/results.php?userid=500614&offset=0&show_names=1&state=3";
            string htmlCode = HTMLCommonParser.GetAndGetHTML(url, cc);
            string usedHtml = Utility.CSharpUtility.GetContent(htmlCode, "class=bordered", "</table>", 1);
            string[] itemStrs;
            Utility.CSharpUtility.GetContent(usedHtml, "<tr", "</tr>", 1, out itemStrs);
            DataTable dt = new DataTable();
            dt.Columns.Add("TaskName", typeof(string));
            dt.Columns.Add("WuId", typeof(string));
            dt.Columns.Add("ComputerId", typeof(string));
            dt.Columns.Add("SentDate", typeof(DateTime));
            dt.Columns.Add("ReportedDate", typeof(DateTime));
            dt.Columns.Add("RunTime", typeof(float));
            dt.Columns.Add("CPUTime", typeof(float));
            dt.Columns.Add("Credit", typeof(float));
            dt.Columns.Add("Application", typeof(string));
            for (int i = 1; i < itemStrs.Length; i++)
            {
                DataRow dr = dt.NewRow();
                string[] drStrs;
                Utility.CSharpUtility.GetContent(itemStrs[i], @"<td[\S\s]*?>", "</td>", 1, out drStrs);
                dr["TaskName"] = Utility.CSharpUtility.StripHTML(drStrs[0]).Trim();
                dr["WuId"] = Utility.CSharpUtility.StripHTML(drStrs[1]).Trim();
                dr["ComputerId"] = Utility.CSharpUtility.StripHTML(drStrs[2]).Trim();
                drStrs[3] = drStrs[3].Replace("UTC", "+00:00");
                drStrs[4] = drStrs[4].Replace("UTC", "+00:00");
                dr["SentDate"] = DateTime.ParseExact(drStrs[3].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                dr["ReportedDate"] = DateTime.ParseExact(drStrs[4].Trim(), "d MMM yyyy H:mm:ss zzz", CultureInfo.GetCultureInfo("en-US"));
                dr["RunTime"] = float.Parse(drStrs[6].Trim());
                dr["CPUTime"] = float.Parse(drStrs[7].Trim());
                dr["Credit"] = float.Parse(drStrs[9].Trim());
                dr["Application"] = drStrs[10].Trim();
                dt.Rows.Add(dr);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr2 = dt.Rows[i];
                for (int j = 0; j < dr2.ItemArray.Length; j++)
                {
                    richTextBox1.AppendText(dr2.ItemArray[j].ToString() + "\t");
                }
                richTextBox1.AppendText("\n");
            }
        }
        public string test045()
        {
            string url = "http://qiang.360buy.com/LimitBuy.ashx?callback=jsonp1308203175652&_=1308203175700&cid=3501";
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            CookieContainer cc = new CookieContainer();
            string result = "";

            try
            {
                cc.Add(new Cookie("uuid", "2d73774c-ca05-4a2d-a470-afa5049f0088", "/", "360buy.com"));
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Referer = "http://qiang.360buy.com/LimitBuy.htm";
                request.ContentType = "application/json";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
                request.Accept = "text/javascript, application/javascript, */*";
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 100 * 1000;
                request.Headers["Accept-Encoding"] = "gzip, deflate";
                request.Headers["Accept-Language"] = "zh-cn";
                request.Headers["x-requested-with"] = "XMLHttpRequest";
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader streamreader = new StreamReader(stream, Encoding.Default);
                result = streamreader.ReadToEnd();
            }
            catch (WebException ex)
            {
            }
            finally
            {
                if (response != null) response.Close();
                if (request != null) request.Abort();
            }
            return result;
        }
        /// <summary>
        /// 对js代码反混编
        /// </summary>
        public void test046()
        {
            string words_code = "|||||div|class|return|li|if|||||||var|img||function|data|http|com||||this||fore|360buy|price|MoreSpecial|limitBuy|null|html|serviceUrl|replace|strong||name|href|src|for|getRandomImgUrl||qiang|getItem|else|target|_blank|contenthtml||ul|extendClass||Math||360buyimg||ashx|callback|wait||height|width|www|product|100|ajax|url|cid|dataType|json|success|in|indexOf|floor|random|LimitBuy|forea|foreb|rob|topimg|misc|img03|jpg|jd2008|purchase|InitCart|aspx|pid|pcount|ptype|182|193|tom|title|content|LimitBuyNormal|gp|png|contenta|3501|3502";
            
            string code = @"
9($){$.R=j(a){g r={};G(g o 1c a){r[o]=a[o]}7 r};$.H=j(a,b){9(a){9(a.1d(""l://h"")==-1){7""l://h""+T.1e(T.1f()*4+10)+"".V.m/n""+b+""/""+a}7 a}7""""};$.w={z:""l://J.t.m/1g.X?Y=?"",k:x,K:j(i){9(!q.k){7 x}9(q.k[i]){7 q.k[i]}7 x},A:j(){g c=j(a,i){g b=[""s"",""1h"",""1i""];9(a){7""<8 6=\'""+b[i]+""\'><B>""+a.C+""</B><5 6=\'p-D\'>""+a.W+""</5></8>""}L{7""<8 6=\'""+b[i]+""\'><B></B><5 6=\'p-D\'></5></8>""}};g d=j(a){9(!a){7""<8 6=\'s Z\'><5 6=\'p-h\'></5><5 6=\'p-u\'></5></8>""}9(a.I&&(a.S==1||a.S==2)){7""<8 6=\'s 1j\'><5 6=\'p-h\'>""+(a.S==2?""<a 6=\'1k\' E=\'#\'><h F=\'l://J.t.m/1l/h/p-1m.1n\'/></a>"":"""")+""<a E=""+(a.S==2?""\'#\'"":(""\'l://1o.t.m/1p/1q.1r?1s=""+a.I+""&1t=1&1u=1\' M=\'N\'""))+""><h F=\'""+$.H(a.U,2)+""\' 11=\'1v\' 12=\'1w\' /></a></5><5 6=\'p-u\'>￥""+a.P+""</5></8>""}L{7""<8 6=\'""+(a.S==0?""s Z"":""s 1x"")+""\'><5 6=\'p-h\'></5><5 6=\'p-u\'></5></8>""}};9(q.k){g e="""",O="""";g f=x;G(g i=0;i<3;i++){f=q.K(i);e+=c(f,0);O+=d(f)}$("".1y Q"").y(e);$("".1z Q"").y(O)}}};$.v=$.R($.w);$.v.z=""l://J.t.m/1A.X?Y=?"";$.v.A=j(){g b=j(a){9(a&&a.I){7""<8 6=\'s\'><5 6=\'p-h\'><a E=\'l://13.t.m/14/""+a.I+"".y\' M=\'N\'><h F=\'""+$.H(a.U,4)+""\' 11=\'15\' 12=\'15\' /></a></5><5 6=\'p-D\'><a E=\'l://13.t.m/14/""+a.I+"".y\' M=\'N\'>""+a.W+""</a></5><5 6=\'p-u\'><h F=\'l://u.V.m/1B""+a.I+"",2.1C\'/></5></8>""}L{7""<8 6=\'s\'><5 6=\'p-h\'></5><5 6=\'p-D\'></5><5 6=\'p-u\'></5></8>""}};9(q.k){g c="""";g d=x;G(g i=0;i<4;i++){d=q.K(i);c+=b(d)}$("".1D Q"").y(c)}};$.16({17:$.w.z,k:{18:1E},19:""1a"",1b:j(r){9(r){$.w.k=r;$.w.A()}}});$.16({17:$.v.z,k:{18:1F},19:""1a"",1b:j(r){9(r){$.v.k=r;$.v.A()}}})}
";
            string newcode = "";
            Hashtable dictionary = new Hashtable();
            string[] words = words_code.Split(new char[] { '|' });
            char index1 = '0';
            char index2 = '0';

            for (int i = 0; i < words.Length; i++)
            {
                string word_index = "";
                if (index1 == '0') 
                    word_index = index2.ToString();
                else 
                    word_index = index1.ToString() + index2.ToString();
                if (index2 == '9') index2 = 'a';
                else if (index2 == 'z') index2 = 'A';
                else if (index2 == 'Z')
                {
                    index1++;
                    index2 = '0';
                }
                else index2++;

                dictionary[word_index] = words[i];
            }
            string nowkey = "";
            code += "     ";//避免边界出现问题
            for (int i = 0; i < code.Length; i++)
            {
                nowkey += code[i];
                if (dictionary.Contains(nowkey) && dictionary[nowkey].ToString() != "")
                {
                    newcode += dictionary[nowkey].ToString();
                    nowkey = "";
                }
                else
                {
                    if (nowkey.Length >= 2)
                    {
                        newcode += nowkey[0];
                        i -= 2 - 1;
                        nowkey = "";
                    }
                }
            }
            richTextBox1.Text = newcode;
        }
        public void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 15000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string now = test045();
            if (!oldmessage.Equals(now))
            {
                oldmessage = now;
                richTextBox1.AppendText(DateTime.Now.ToString() + "：" + now + "\n");
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;
                this.TopMost = true;
                MessageBox.Show("有新商品啦！！！！！！");
                this.TopMost = false;
                //timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            warncount++;
            if (warncount < 2)
            {
                MessageBox.Show("有新商品啦！！！！！！");
            }
            else
            {
                timer2.Stop();
                warncount = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Forms.JingDong.ActivityForm form = new MyUsefulTools.Forms.JingDong.ActivityForm();
            form.Show();
        }
    }
}
