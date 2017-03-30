using System.Web;
using System.Web.SessionState;

namespace GameStore.WebUI.Tests.Fakes
{
    public class FakeHttpSessionState : HttpSessionStateBase
    {
        private readonly SessionStateItemCollection _sessionItems;

        public FakeHttpSessionState(SessionStateItemCollection sessionItems)
        {
            _sessionItems = sessionItems;
        }

        public override object this[string name]
        {
            get { return _sessionItems[name]; }
            set { _sessionItems[name] = value; }
        }
    }
}
