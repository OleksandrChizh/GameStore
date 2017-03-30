using System.Web.Mvc;

namespace GameStore.WebUI.Utils
{
    public static class TermDataTag
    {
        public static string GetPair(string key, string value)
        {
            var result = string.Empty;
            var dt = new TagBuilder("dt");
            var dd = new TagBuilder("dd");
            dt.SetInnerText(key);
            dd.SetInnerText(value);
            result += dt.ToString();
            result += dd.ToString();
            return result;
        }
    }
}