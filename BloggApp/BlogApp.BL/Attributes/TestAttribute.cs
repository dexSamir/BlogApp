using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.BL.Attributes;
public class TestAttribute : Attribute, IActionFilter
{
	public TestAttribute()
	{

	}

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        throw new NotImplementedException();
    }
}

