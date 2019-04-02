using AngleSharp.Html.Dom;
using HTMLParser.Core;

using System.Collections.Generic;
using System.Linq;



namespace HTMLParser.GabeStore
{
    class GabeStoreParser : IParser<string[]>
    {

        
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();

            var itemsER = document.QuerySelectorAll("span").Where(item => item.ClassName != null && item.ClassName.Contains("catalog-card__title-text"));
            var items = document.QuerySelectorAll("font").Where(item => item.ClassName != null && item.ClassName.Contains("currencyPrice"));
            foreach (var item in items)
            {
                
                list.Add(item.TextContent + ' ');


            }
            
            foreach (var item in itemsER)
            {

                list.Add(item.TextContent);
            }
            return list.Distinct().ToArray();


            


        }
    }
}
