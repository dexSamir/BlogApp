using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.UserExceptions;
public class UserPermissionDeniedException : Exception, IBaseException
{

    public int StatusCode => StatusCodes.Status403Forbidden; 

    public string ErrorMessage { get; }

    public UserPermissionDeniedException()
    {
        ErrorMessage = "Permission Denied!";
    }
    public UserPermissionDeniedException(string msg) : base (msg)
    {
        ErrorMessage = msg; 
    }
}

