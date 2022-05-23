using System;
using QSF.Common;
using Microsoft.Maui.Controls;

namespace QSF.Services
{
    /// <summary>
    /// The ExampleService is responsible for managing the examples section in the app.
    /// </summary>
    public class ExampleService : IExampleService
    {
        private const string NameFormat = "QSF.Examples.{0}Control.{1}Example.{2}";

        public object CreateExample(Example example)
        {
            var controlName = example.ControlName;
            var name = string.Format(NameFormat, controlName, example.Name, example.Page);
            var type = Type.GetType(name);
            View view = (View)Activator.CreateInstance(type);

            return view;
        }
    }
}
