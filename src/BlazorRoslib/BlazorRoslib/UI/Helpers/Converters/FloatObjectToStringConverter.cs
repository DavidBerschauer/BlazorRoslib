using System;
using MudBlazor;

namespace BlazorRoslib.UI.Helpers.Converters;

public class FloatObjectToStringConverter : MudBlazor.Converter<object, string>
{

    public FloatObjectToStringConverter()
    {
        SetFunc = OnSet;
        GetFunc = OnGet;
    }

    private object OnGet(string value)
    {
        try
        {
            return double.Parse(value);
        }
        catch (Exception e)
        {
            UpdateGetError($"Conversion error: from '{value}':{e.Message}");
            return null;
        }
    }

    private string OnSet(object? arg)
    {
        if (arg == null)
            return "";
        try
        {
            if (arg is double)
                return ((double)arg).ToString();
            else if (arg is double?)
                return ((double?)arg)?.ToString() ?? "";
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
