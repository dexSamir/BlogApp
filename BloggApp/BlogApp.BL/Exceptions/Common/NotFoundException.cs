


using Microsoft.AspNetCore.Http;

namespace BlogApp.BL.Exceptions.Common;

public class NotFoundException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status404NotFound; 

    public string ErrorMessage { get; }

    public NotFoundException(string msg) : base (msg)
    {
        ErrorMessage = msg;
    }
}
public class NotFoundException<T> : NotFoundException
{
    public NotFoundException() : base(typeof(T).Name + " not found!")
    {
         
    }
}

