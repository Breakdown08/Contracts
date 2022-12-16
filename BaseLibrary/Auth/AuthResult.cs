using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ContractsApi.BaseLibrary.Auth
{
    public class AuthResult : TypeFilterAttribute
    {
        public AuthResult(string scope)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { scope };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        public readonly string _scope;
        public AuthorizeActionFilter(string scope)
        {
            _scope = scope;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                bool isAuthorized = Authorizer.Authorize(ref context, _scope); // :)
                if (!isAuthorized)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch (Exception)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}