using Microsoft.AspNetCore.Mvc.Filters;

namespace Unistream.TestTask.Server.Controllers;

public class FixQueryJsonStringFilterAttribute : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        //меняем значение + на валидное для json %2b
        var queryString = context.HttpContext.Request.QueryString;
        var value = queryString.Value?.Replace("+", "%2b").Replace("\"", "\\\"");
        context.HttpContext.Request.QueryString = new QueryString(value);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}


