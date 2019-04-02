using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using AngleSharp.Html.Parser;

namespace HTMLParser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        HTMLoader HTMLoader;
        static IWebDriver driver = new ChromeDriver();

        bool isActive;

        public event Action<object, T > OnNewData;
        public event Action<object> OnCompleted;


        public bool isActiveQ { get { return isActive; } }

        public IParser<T> Parser
        {
            get
            {
                return parser;
            }
            set
            {
                parser = value;
            }
        }

        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                HTMLoader = new HTMLoader(value);
            }
        }

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }


        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await HTMLoader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(ExpandFullPage());



                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;

        }


        public string ExpandFullPage()
        {
            driver.Navigate().GoToUrl(HTMLoader.url);
            IWebElement element = driver.FindElement(By.CssSelector("#w0>div>div>section.catalog-list>div.wrapper.catalog-list-wrapper>div.catalog-main.is-active>div>div>button"));

            while (element != null)
            {
                element = driver.FindElement(By.CssSelector("#w0>div>div>section.catalog-list>div.wrapper.catalog-list-wrapper>div.catalog-main.is-active>div>div>button"));
                if (element.Displayed == false)
                {
                    break;
                }
                Actions action = new Actions(driver);
                action.MoveToElement(element).Perform();
                element.Click();

            }

            return driver.PageSource;

        }
    }
}
