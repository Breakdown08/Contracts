using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace ContractsApi.BaseLibrary.Auth
{
    public static class Authorizer
    {
        public static bool Authorize(ref AuthorizationFilterContext context, string _scope)
        {
            var headers = context.HttpContext.Request.Headers;
            PayloadDataModel model = JWTParser.ParseToModel(headers);
            foreach (var role in model.role)
            {
                if (role == _scope) return true;
            }
            foreach (var permission in model.permission)
            {
                if (permission == _scope) return true;
            }
            return false;
        }
    }
}
