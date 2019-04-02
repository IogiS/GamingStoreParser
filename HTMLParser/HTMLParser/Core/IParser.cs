using AngleSharp;
using AngleSharp.Html.Dom;

namespace HTMLParser.Core
{
    interface IParser<T> where T: class
    {

        T Parse (IHtmlDocument document );

    }
}
