using HTMLParser.Core;
using System;
using System.Windows.Forms;
using HTMLParser.GabeStore;


namespace HTMLParser
{
    public partial class Form1 : Form
    {

        ParserWorker<string[]> parser;



        public Form1()
        {
            InitializeComponent();
            parser = new ParserWorker<string[]>(
                     new GabeStoreParser()
                );
            parser.OnCompleted += Parser_OnComleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnComleted(object obj)
        {
            MessageBox.Show("Complete");
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            listBox1.Items.AddRange(arg2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = "https://gabestore.ru/games/action";
            parser.Settings = new GabeStoreParserSettings(1, 1, url);
            parser.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }




    }
}
