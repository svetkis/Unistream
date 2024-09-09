using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;
using Unistream.TestTask.Common;

namespace Unistream.TestTask.Server.Controllers;

public class InsertTransactionModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        InsertTransactionModel model = null;

        var jsonValue = bindingContext.ValueProvider
            .GetValue("insert")
            .FirstOrDefault();

        if (jsonValue != null)
        {
            model = JsonSerializer.Deserialize<InsertTransactionModel>(jsonValue);
        }


        bindingContext.Result = ModelBindingResult.Success(model);
        return Task.CompletedTask;
    }
}


