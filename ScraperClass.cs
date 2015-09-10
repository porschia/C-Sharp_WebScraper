using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebScraper
{
    class ScraperClass
    {
        public static string getSourceCode(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string sourceCode = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return sourceCode;

        }

        public static List<string> getLinks(string sourceCode, string url, List<string> linkList)
        {
            while(sourceCode.IndexOf("<a href=") != -1)
            {
                int startIndex = sourceCode.IndexOf("a href=") + 8;
                int endIndex = sourceCode.IndexOf("sasource");
                string link = sourceCode.Substring(startIndex, (endIndex - startIndex) - 2);
                string fullLink = "http://seekingalpha.com" + link;
                linkList.Add(fullLink);
                sourceCode = sourceCode.Substring(endIndex + 7);
            }
            return linkList;
        }

        public static string parseSourceCode(string sourceCode)
        {
            int startIndex = sourceCode.IndexOf("<div id=\"article_content\"") + 55;
            int endIndex = sourceCode.IndexOf("<div id=\"article_disclaimer\"");
            string transcript = sourceCode.Substring(startIndex, endIndex - startIndex);
            return transcript;
        }
    }
}
