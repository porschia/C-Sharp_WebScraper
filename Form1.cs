using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace WebScraper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Create a list of urls to use 
            List<string> urlList = new List<string>();
            string[] syms = new string[] { tickerBox1.Text, tickerBox2.Text, tickerBox3.Text, tickerBox4.Text };
            foreach(string sym in syms)
            {
                //Verify if a symbol is present
                if (sym != "")
                {
                    string url = "http://www.seekingalpha.com/symbol/" + sym + "/transcripts";
                    urlList.Add(url);
                }
            }

            //Scrape for each symbol requested
            foreach (string url in urlList)
            {
                string symbol = url.Replace("http://www.seekingalpha.com/symbol/", "");
                symbol = symbol.Replace("/", "");
                string sourceCode = ScraperClass.getSourceCode(url);

                //Create a list for all links present in source code
                List<string> linkList = new List<string>();
                int startIndex = sourceCode.IndexOf("<div class=\"symbol_article\">");
                int endIndex = sourceCode.IndexOf("<div class=\"loader\">", startIndex);
                sourceCode = sourceCode.Substring(startIndex, endIndex - startIndex);

                linkList = ScraperClass.getLinks(sourceCode, url, linkList);

                foreach(string link in linkList)
                {
                    if (link.Contains(yearBox.Text))
                    {
                        if (Q1Box.Checked)
                        {
                            if (link.Contains("q1") | link.Contains("1q"))
                            {
                                if (!link.Contains("comments_header"))
                                {
                                    string tsourceCode = ScraperClass.getSourceCode(link);
                                    string transcript = ScraperClass.parseSourceCode(tsourceCode);
                                    string filename = fileBox.Text + "Q1" + yearBox.Text + symbol + ".txt";
                                    StreamWriter sw = new StreamWriter(filename, false);
                                    sw.Write(transcript);
                                    sw.Close();
                                }
                            }
                        }
                        if (Q2Box.Checked)
                        {
                            if (link.Contains("q2") | link.Contains("2q"))
                            {
                                if (!link.Contains("comments_header"))
                                {
                                    string tsourceCode = ScraperClass.getSourceCode(link);
                                    string transcript = ScraperClass.parseSourceCode(tsourceCode);
                                    string filename = fileBox.Text + "Q2" + yearBox.Text + symbol + ".txt";
                                    StreamWriter sw = new StreamWriter(filename, false);
                                    sw.Write(transcript);
                                    sw.Close();
                                }
                            }
                        }
                        if (Q3Box.Checked)
                        {
                            if (link.Contains("q3") | link.Contains("3q"))
                            {
                                if (!link.Contains("comments_header"))
                                {
                                    string tsourceCode = ScraperClass.getSourceCode(link);
                                    string transcript = ScraperClass.parseSourceCode(tsourceCode);
                                    string filename = fileBox.Text + "Q3" + yearBox.Text + symbol + ".txt";
                                    StreamWriter sw = new StreamWriter(filename, false);
                                    sw.Write(transcript);
                                    sw.Close();
                                }
                            }
                        }
                        if (Q4Box.Checked)
                        {
                            if (link.Contains("q4") | link.Contains("4q"))
                            {
                                if (!link.Contains("comments_header"))
                                {
                                    string tsourceCode = ScraperClass.getSourceCode(link);
                                    string transcript = ScraperClass.parseSourceCode(tsourceCode);
                                    string filename = fileBox.Text + "Q4" + yearBox.Text + symbol + ".txt";
                                    StreamWriter sw = new StreamWriter(filename, false);
                                    sw.Write(transcript);
                                    sw.Close();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.MyDocuments;
            fb.Description = "***Select Folder***";

            if (fb.ShowDialog() == DialogResult.OK)
            {
                fileBox.Text = fb.SelectedPath + "\\";
            }
        }

    }
}
