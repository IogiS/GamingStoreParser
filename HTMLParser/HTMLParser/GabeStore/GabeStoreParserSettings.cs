using HTMLParser.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using HTMLParser.GabeStore;

namespace HTMLParser.GabeStore
{
    public class GabeStoreParserSettings : IParserSettings
    {


        public GabeStoreParserSettings(int start, int abort, string baseUrl)
        {
            StartPoint = start;
            EndPoint = abort;
            BaseURL = baseUrl;
        }

        public GabeStoreParserSettings() { }

        public string BaseURL { get; set; } 

        public string Prefix { get; set; } = "";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

    }
}
