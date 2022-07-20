using Microsoft.Maui.Controls;
using QSF.Common;
using System;

namespace QSF.Services;

public class ConfigurationAreaService : IConfigurationAreaService
{
    private const string NameFormat = "QSF.Examples.{0}Control.{1}Example.ConfigurationArea";

    public View CreateConfigurationArea(Example example)
    {
        string name = string.Format(NameFormat, example.ControlName, example.Name);
        Type type = Type.GetType(name);

        if (type != null)
        {
            View view = (View)Activator.CreateInstance(type);
            return view;
        }
        else
        {
            return null;
        }
    }
}
