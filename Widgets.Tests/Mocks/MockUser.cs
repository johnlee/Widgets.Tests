using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace Widgets.Tests.Mocks
{
    public static class MockUser
    {
        private static string _userName = "mockUser";
        private static string _userId = "1000";

        public static void CurrentUser(this ApiController controller)
        {
            var identity = new GenericIdentity(_userName);
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", _userName));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", _userId));

            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }

        public static string GetUserName()
        {
            return _userName;
        }

        public static string GetUserId()
        {
            return _userId;
        }
    }
}
