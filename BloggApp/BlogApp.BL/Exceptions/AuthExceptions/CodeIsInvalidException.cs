using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.AuthExceptions;
public class CodeIsInvalidException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest; 

    public string ErrorMessage { get; }

    public CodeIsInvalidException()
    {
        ErrorMessage = "Gonderilen kod yanlisdir!";
    }
    public CodeIsInvalidException(string msg) : base(msg)
    {
        ErrorMessage = msg; 
    }
}

