using DataAccessTools.Entities.GoogleRSS;
using log4net;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using TryAndTestConsole.LinqToSQL;
using Utility;
using System.IO;
using StoneUtils.Internet;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Filters;
using StoneBowReader.InfoParser;
using EFDataAccess.Entities;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TryAndTestConsole
{
    class Program
    {
        public static void test001_FundInfoInitTest()
        {
            FundInfoInitTest test = new FundInfoInitTest();
            test.GetAllFundInfoFromInternet();
        }
        public static void test002_CurrencyFundUpdate()
        {
            FundInfoInitTest test = new FundInfoInitTest();
            test.GetCurrencyFundEverydayValue(DateTime.Parse("2010-1-1"), DateTime.Now);
        }
        public static void test003_GoogleReaderAPI()
        {
            GoogleReader gr = new GoogleReader("zl860628@gmail.com", "");
            ILog log = log4net.LogManager.GetLogger("LogFileAppender");
            //List<GoogleFeed> feeds = gr.GetUnreadFeedList();
            string itemstring = gr.GetUnreadItemsByFeed(null);
            log.Debug(itemstring);
        }
        public static void test004()
        {
            //http://item.jd.com/1076109.html
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(@"c:\leozhang\temp\test.sqlite", SqliteDialect.Provider);
            IDbConnection db = dbFactory.OpenDbConnection();
            db.CreateTable<GoogleReaderSubscription>();
            GoogleReaderSubscription test1 = new GoogleReaderSubscription { Title = "CnBeta", FeedUrl = "www.cnbeta.com" };
            db.Insert<GoogleReaderSubscription>(test1);
            List<GoogleReaderSubscription> readtest1 = db.Select<GoogleReaderSubscription>(a => a.Title == "CnBeta");
            int resultCount = readtest1.Count;
        }
        public static void test005_GetHostName()
        {
            string hostname = Dns.GetHostName();
            Console.WriteLine(hostname);
        }
        private static void test006()
        {
            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem()
            {
                URL = "http://www.cnbeta.com/articles/436765.htm",
            };
            //httpItem.Header.Add("Accept-Encoding", "gzip,deflate");
            HttpResult httpResult = httpHelper.GetHtml(httpItem);
            Console.WriteLine("Finished : Get website html.");
            //string info = regex.Match(httpResult.Html).Value;
            StreamWriter sw = new StreamWriter(@"temphttp.xml");
            //sw.WriteLine(CSharpUtility.GetContent(httpResult.Html, "<h2 id=\"news_title\">", "</h2>", 1));
            //sw.WriteLine(CSharpUtility.GetContent(httpResult.Html, "<span class=\"date\">", "</span>", 1));
            //sw.WriteLine(CSharpUtility.GetContent(httpResult.Html, "<span class=\"where\">稿源：", "</span>", 1));
            //sw.WriteLine(CSharpUtility.GetContent(httpResult.Html, "<div class=\"introduction\">", "</div>", 1));
            //sw.WriteLine(CSharpUtility.GetContent(httpResult.Html, "<div class=\"content\">", "</div>", 1));
            Parser parser = new Winista.Text.HtmlParser.Parser(new Lexer(httpResult.Html));
            Winista.Text.HtmlParser.Util.NodeList nl_all = parser.Parse(null);

            Winista.Text.HtmlParser.Util.NodeList nl_title = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "news_title"), true);
            if(nl_title.Count == 0)
            {
                sw.Close();
                return;
            }
            sw.WriteLine(nl_title[0].ToPlainTextString());

            Winista.Text.HtmlParser.Util.NodeList nl_date = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "date"), true);
            sw.WriteLine(nl_date[0].ToPlainTextString());

            Winista.Text.HtmlParser.Util.NodeList nl_source = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "where"), true);
            sw.WriteLine(nl_source[0].ToPlainTextString());

            Winista.Text.HtmlParser.Util.NodeList nl_introduction = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "introduction"), true)
                                                                          .ExtractAllNodesThatMatch(new NodeClassFilter(typeof(Winista.Text.HtmlParser.Tags.ParagraphTag)), true);
            for (int i = 0; i < nl_introduction.Count; i++)
            {
                Console.WriteLine(nl_introduction[i].GetType());
                sw.WriteLine(nl_introduction[i].ToPlainTextString().Trim().Replace("\r\n", ""));
            }

            Winista.Text.HtmlParser.Util.NodeList nl_content = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "content"), true)[0].Children;
            for (int i = 0; i < nl_content.Count; i++)
            {
                sw.WriteLine(nl_content[i].ToHtml().Trim());
            }

            sw.Close();
        }

        public static void test007()
        {
            CnBetaNewsParser parser = new CnBetaNewsParser();
            using (CnBetaInfoContext context = new CnBetaInfoContext())
            {
                context.CnBetaInfos.Add(parser.ParseCnBetaInfo("http://www.cnbeta.com/articles/436765.htm"));
                context.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            //test001_FundInfoInitTest();
            //test002_CurrencyFundUpdate();
            //test003_GoogleReaderAPI();
            //test004();
            //test005_GetHostName();
            test007();
            Console.Write("finished...");
            Console.Read();
        }
    }
}
