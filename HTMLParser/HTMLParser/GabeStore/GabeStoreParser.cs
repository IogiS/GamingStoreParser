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
            long flag = 0;
            string[] itemsArray = new string[items.Count()];
            string[] itemsERArray = new string[items.Count()];

            foreach (var item in itemsER)
            {
                itemsArray[flag] = item.TextContent;
                flag++;

            }
            flag = 0;

            foreach (var item in items)
            {

                itemsERArray[flag] = item.TextContent;
                flag++;
            }
            itemsERArray = itemsERArray.Distinct().ToArray();

            for (int i = 0; i < itemsERArray.Count(); i++)
            {
                list.Add(itemsERArray[i] + ' ' + itemsArray[i]);
            }


            return list.ToArray();


            


        }
    }
}
