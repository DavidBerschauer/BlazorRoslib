using System;
using MudBlazor;

namespace BlazorRoslib.UI.Helpers.Converters;

public class IntObjectToStringConverter : MudBlazor.Converter<object, string>
{

    public IntObjectToStringConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;
    }

    private object OnGet(string value)
    {
        try
        {
            return int.Parse(value);
        }
        catch (Exception e)
        {
            UpdateGetError("Conversion error: " + e.Message);
            return 0;
        }
    }

    private string OnSet(object? arg)
    {
        if (arg == null)
            return "";
        try
        {
            if (arg is int)
                return ((int)arg).ToString();
            else if (arg is bool?)
                return ((int?)arg)?.ToString() ?? "";
            else
            {
                UpdateSetError("Unable to convert to int string from type object");
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
