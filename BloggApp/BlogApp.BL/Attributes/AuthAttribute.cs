using BlogApp.BL.Constant;
using BlogApp.BL.Exceptions.AuthExceptions;
using BlogApp.BL.Exceptions.UserExceptions;
using BlogApp.Core.Entities;
using BlogApp.Core.Helpers.Enums;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.BL.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthAttribute : Attribute, IAsyncActionFilter
    {
        private int _access;
        public AuthAttribute(Roles role)
        {
            _access = (int)role;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var value = context.HttpContext.User.FindFirst(x => x.Type == ClaimType.Role)?.Value;
            if (value == null)
                throw new AuthorisationException();

            int role = Convert.ToInt32(value);
            if ((role & _access) != _access)
                throw new UserPermissionDeniedException<User>();

            await next(); 
        }
    }
}

