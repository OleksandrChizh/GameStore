using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Attributes
{
    public class MvcAuthoriseAttribute : AuthorizeAttribute
    {
        public string ForbiddenRoles { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsAuthenticated &&
                   !IsUserHaveForbiddenRoles(httpContext, GetRolesArray(ForbiddenRoles)) &&
                   IsUserHaveRoles(httpContext, GetRolesArray(Roles));
        }

        private bool IsUserHaveRoles(HttpContextBase httpContext, string[] rolesArray)
        {
            return rolesArray.Length == 0 || rolesArray.Any(r => httpContext.User.IsInRole(r));
        }

        private bool IsUserHaveForbiddenRoles(HttpContextBase httpContext, string[] rolesArray)
        {
            return rolesArray.Length != 0 && rolesArray.Any(r => httpContext.User.IsInRole(r));
        }

        private string[] GetRolesArray(string roles)
        {
            string[] rolesArray = { };

            if (!string.IsNullOrEmpty(roles))
            {
                rolesArray = roles.Split(',');

                for (int i = 0; i < rolesArray.Length; i++)
                {
                    rolesArray[i] = rolesArray[i].Trim();
                }
            }

            return rolesArray;
        }
    }
}