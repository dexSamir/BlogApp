using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.Common; 

public class ExistException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status409Conflict;

    public string ErrorMessage { get; }

    public ExistException(string msg) : base(msg)
    {
        ErrorMessage= msg; 
    }
}
public class ExistException<T> : ExistException
{
    public ExistException() : base(typeof(T).Name + " is already exists!")
    {

    }
}

