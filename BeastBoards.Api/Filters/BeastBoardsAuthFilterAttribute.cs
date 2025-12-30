using BeastBoards.Api.Services;
using JWT.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeastBoards.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BeastBoardsAuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwt = context.HttpContext.RequestServices.GetService<BeastBoardsJwtService>();

            var token = context.HttpContext.Request.Headers.Authorization.ToString();

            if(token == null)
            {
                context.Result = new UnauthorizedResult();
            }

            try
            {
                var parseResult = jwt.Parse(token);

                context.HttpContext.Items["SteamUserStub"] = parseResult;
            }
            catch (TokenNotYetValidException)
            {
                context.Result = new UnauthorizedResult();
            }
            catch (TokenExpiredException)
            {
                context.Result = new UnauthorizedResult();
            }
            catch (SignatureVerificationException)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
