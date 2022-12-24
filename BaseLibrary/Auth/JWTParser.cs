using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace ContractsApi.BaseLibrary.Auth
{
    public static class JWTParser
    {
        static string GetJWTBearerFromHeaders(IHeaderDictionary headers)
        {
            StringValues strings = new StringValues();
            headers.TryGetValue("Authorization", out strings);
            var stringJWT = strings.ToString().Remove(0, strings.ToString().IndexOf(" ", 0)+1);
            return stringJWT;
        }

        static PayloadDataModel ParseJWTToModel(string stringJWT)
        {
            PayloadDataModel model = new PayloadDataModel();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(stringJWT) as JwtSecurityToken;
            List<string> roles = new List<string>();
            foreach (var item in token.Claims.Where(claim => claim.Type == "role"))
            {
                roles.Add(item.Value);
            }
            model.role = roles;
            List<string> permissions = new List<string>();
            foreach (var item in token.Claims.Where(claim => claim.Type == "permission"))
            {
                permissions.Add(item.Value);
            }
            model.permission = permissions;
            model.iat = Convert.ToUInt64(token.Claims.First(claim => claim.Type == "iat").Value);
            model.exp = Convert.ToUInt64(token.Claims.First(claim => claim.Type == "exp").Value);
            return model;
        }


        public static PayloadDataModel ParseToModel(IHeaderDictionary headers)
        {
            string bearer = GetJWTBearerFromHeaders(headers);
            PayloadDataModel model = ParseJWTToModel(bearer);
            return model;
        }
    }
}
