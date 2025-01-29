using System;
using BlogApp.BL.Exceptions.Common;
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

public class UserPermissionDeniedException<T> : UserPermissionDeniedException
{
    public UserPermissionDeniedException() : base(typeof(T).Name + " has no permission!")
    {

    }
}

