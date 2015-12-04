using StoneUtils.Internet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Filters;
using EFDataAccess.Entities;

namespace StoneBowReader.InfoParser
{
    public class CnBetaNewsParser
    {
        public CnBetaInfo ParseCnBetaInfo(string url)
        {
            CnBetaInfo cnBetaInfo = new CnBetaInfo();

            cnBetaInfo.Id = cnBetaInfo.URL = url;

            HttpHelper httpHelper = new HttpHelper();
            HttpItem httpItem = new HttpItem()
            {
                URL = url,
            };
            //httpItem.Header.Add("Accept-Encoding", "gzip,deflate");
            HttpResult httpResult = httpHelper.GetHtml(httpItem);
            Console.WriteLine("Finished : Get website html.");
            Parser parser = new Winista.Text.HtmlParser.Parser(new Lexer(httpResult.Html));
            Winista.Text.HtmlParser.Util.NodeList nl_all = parser.Parse(null);

            Winista.Text.HtmlParser.Util.NodeList nl_title = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("id", "news_title"), true);
            if (nl_title.Count == 0)
            {
                return null;
            }
            cnBetaInfo.Title = nl_title[0].ToPlainTextString();

            Winista.Text.HtmlParser.Util.NodeList nl_date = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "date"), true);
            cnBetaInfo.PubTime = DateTime.Parse(nl_date[0].ToPlainTextString());

            Winista.Text.HtmlParser.Util.NodeList nl_source = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "where"), true);
            cnBetaInfo.Source = nl_source[0].ToPlainTextString();

            Winista.Text.HtmlParser.Util.NodeList nl_introduction = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "introduction"), true)
                                                                          .ExtractAllNodesThatMatch(new NodeClassFilter(typeof(Winista.Text.HtmlParser.Tags.ParagraphTag)), true);
            for (int i = 0; i < nl_introduction.Count; i++)
            {
                cnBetaInfo.Content += nl_introduction[i].ToPlainTextString().Trim().Replace("\r\n", "");
            }

            Winista.Text.HtmlParser.Util.NodeList nl_content = nl_all.ExtractAllNodesThatMatch(new HasAttributeFilter("class", "content"), true)[0].Children;
            for (int i = 0; i < nl_content.Count; i++)
            {
                cnBetaInfo.Content += nl_content[i].ToHtml().Trim();
            }

            return cnBetaInfo;
        }
    }
}
