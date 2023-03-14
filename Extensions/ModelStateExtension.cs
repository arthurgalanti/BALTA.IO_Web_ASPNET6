﻿using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogAPI.Extensions;

public static class ModelStateExtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        var result = new List<string>();
        foreach (var item in modelState.Values)
            result.AddRange(item.Errors.Select(error => error.ErrorMessage));

        return result;
    }
}