using StoneUtils.Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;

namespace StoneUtils.BL.StoneBowReader
{
    public class GrabWebContent
    {
        public static string GetCnBetaContent(string url)
        {
            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem()
            {
                URL = url,
            };
            //httpItem.Header.Add("Accept-Encoding", "gzip,deflate");
            HttpResult httpResult = httpHelper.GetHtml(httpItem);
            Console.WriteLine("Finished : Get website html.");
            //string info = regex.Match(httpResult.Html).Value;
            Parser parser = new Winista.Text.HtmlParser.Parser(new Lexer(httpResult.Html));
            Winista.Text.HtmlParser.Util.NodeList nl_all = parser.Parse(null);

            Winista.Text.HtmlParser.Util.NodeList nl_title = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "news_title"), true);
            if (nl_title.Count == 0)
            {
                return "";
            }
            string cb_title = nl_title[0].ToPlainTextString();

            Winista.Text.HtmlParser.Util.NodeList nl_date = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "date"), true);
            string cb_date = nl_date[0].ToPlainTextString();

            Winista.Text.HtmlParser.Util.NodeList nl_source = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "where"), true);
            string cb_source = nl_source[0].ToPlainTextString();

            Winista.Text.HtmlParser.Util.NodeList nl_introduction = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "introduction"), true)
                                                                          .ExtractAllNodesThatMatch(new NodeClassFilter(typeof(Winista.Text.HtmlParser.Tags.ParagraphTag)), true);

            StringBuilder sb_cb_introduction = new StringBuilder();
            for (int i = 0; i < nl_introduction.Count; i++)
            {
                Console.WriteLine(nl_introduction[i].GetType());
                sb_cb_introduction.AppendLine(nl_introduction[i].ToPlainTextString().Trim().Replace("\r\n", ""));
            }

            StringBuilder sb_cb_content = new StringBuilder();
            Winista.Text.HtmlParser.Util.NodeList nl_content = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "content"), true)[0].Children;
            for (int i = 0; i < nl_content.Count; i++)
            {
                sb_cb_content.AppendLine(nl_content[i].ToHtml().Trim());
            }

            return sb_cb_content.ToString();
        }
    }
}
