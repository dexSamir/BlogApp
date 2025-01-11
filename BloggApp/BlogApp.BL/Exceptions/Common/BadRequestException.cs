using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.Common;
public class BadRequestException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest; 

    public string ErrorMessage { get; }

}

