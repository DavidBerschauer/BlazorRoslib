using System;
using MudBlazor;

namespace BlazorRoslib.UI.Helpers.Converters;

public class StringObjectToStringConverter : MudBlazor.Converter<object, string>
{

    public StringObjectToStringConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;
    }

    private object OnGet(string value)
    {
        return value;
    }

    private string OnSet(object? arg)
    {
        if (arg == null)
            return "";
        try
        {
            if (arg is string argstring)
                return argstring;
            else
            {
                UpdateSetError("Unable to convert to string from type object");
                return "";
            }
        }
        catch (FormatException e)
        {
            UpdateSetError("Conversion error: " + e.Message);
            return "";
        }
    }
}
