using HTMLParser.Core;
using System;
using System.Windows.Forms;
using HTMLParser.GabeStore;
using Newtonsoft.Json;
using System.IO;

namespace HTMLParser
{
    class MyGames
    {
        public Game[] Games { get; set; }
    }

    class Game
    {
        public string Price { get; set; }
        public string GameName { get; set; }

    }

    public partial class Form1 : Form
    {
        StreamWriter file = new StreamWriter(@"C:\Users\Logis\Desktop\NodeAPI\public\games.json");
        ParserWorker<string[]> parser;

        struct UserInfo
        {
            [JsonProperty("GameName")]
            public string GameName;
            [JsonProperty("Price")]
            public string Price;
            [JsonProperty("InternetShop")]
            public string InternetShop;
        }


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
            file.Close();
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            var data = new UserInfo();

            listBox1.Items.AddRange(arg2);
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                data = new UserInfo { GameName = listBox1.Items[i].ToString().Substring(listBox1.Items[i].ToString().IndexOf(' ')).Trim(), Price = listBox1.Items[i].ToString().Split(' ')[0], InternetShop = "gabestore.com" };
                string serialized = JsonConvert.SerializeObject(data, Formatting.Indented);
                file.Write(serialized);
            }
            
            
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
