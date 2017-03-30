using System.Web.Mvc;

namespace GameStore.WebUI.ActionResults
{
    public class CustomHtmlResult : ActionResult
    {
        private readonly string _html;
        private readonly string _title;

        public CustomHtmlResult(string html, string title)
        {
            _html = html;
            _title = title;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var html = new TagBuilder("html");
            var head = new TagBuilder("head");
            var body = new TagBuilder("body");
            var title = new TagBuilder("title");
            var meta = new TagBuilder("meta");

            title.SetInnerText(_title);
            meta.Attributes["charset"] = "utf-8";
      
            head.InnerHtml += title.ToString();
            head.InnerHtml += meta.ToString();
            body.InnerHtml += _html;
            html.InnerHtml += head.ToString();
            html.InnerHtml += body.ToString();

            context.HttpContext.Response.Write(html.ToString());
        }
    }
}