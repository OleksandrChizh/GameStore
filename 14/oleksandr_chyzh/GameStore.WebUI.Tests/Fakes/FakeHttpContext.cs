using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using GameStore.WebUI.Authorization;

namespace GameStore.WebUI.Tests.Fakes
{
    public class FakeHttpContext : HttpContextBase
    {
        private readonly SessionStateItemCollection _sessionItems;

        public FakeHttpContext(SessionStateItemCollection sessionItems)
        {
            _sessionItems = sessionItems;
        }

        public override IPrincipal User { get; set; } = new UserPrincipal("email", "id", new DateTime(), new List<string>());

        public override HttpSessionStateBase Session => new FakeHttpSessionState(_sessionItems);
    }
}
