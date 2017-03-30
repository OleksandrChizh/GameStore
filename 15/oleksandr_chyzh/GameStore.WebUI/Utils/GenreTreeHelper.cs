using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.WebUI.ViewModels.Genre;

namespace GameStore.WebUI.Utils
{
    public static class GenreTreeHelper
    {
        public static MvcHtmlString CreateGenreTree(this HtmlHelper html, ICollection<GenreViewModel> genres)
        {
            var ol = new TagBuilder("ol");
            List<GenreViewModel> parentGenres = genres.Where(g => g.ParentGenreId == null).ToList();
            foreach (var parentGenre in parentGenres)
            {
                var parentLi = new TagBuilder("li");
                var span = new TagBuilder("span");
                span.SetInnerText(parentGenre.Name);
                parentLi.InnerHtml += span.ToString();
            
                List<GenreViewModel> childGenres = genres.Where(g => g.ParentGenreId != null && g.ParentGenreId == parentGenre.Id)
                                        .ToList();
                if (childGenres.Count == 0)
                {
                    ol.InnerHtml += parentLi.ToString();
                    continue;
                }

                var ul = new TagBuilder("ul");
                foreach (GenreViewModel childGenre in childGenres)
                {
                    var childLi = new TagBuilder("li");
                    childLi.SetInnerText(childGenre.Name);
                    ul.InnerHtml += childLi.ToString();
                }

                parentLi.InnerHtml += ul.ToString();
                ol.InnerHtml += parentLi.ToString();
            }

            return new MvcHtmlString(ol.ToString());
        }
    }
}