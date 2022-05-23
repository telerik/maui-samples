using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;
using QSF.Common;

namespace QSF.Services
{
    /// <summary>
    /// The ControlsService is responsible for managing the controls section in the app.
    /// </summary>
    public class ControlsService : IControlsService
    {
        public IEnumerable<Control> GetAllControls()
        {
            IConfigurationService configurationService = DependencyService.Get<IConfigurationService>();
            var controls = configurationService.GetControlsConfiguration().OrderBy(p => p.DisplayName);

            return controls;
        }

        public Control GetControlByName(string controlName)
        {
            var controls = this.GetAllControls();

            return controls.Where(p => p.Name == controlName).FirstOrDefault();
        }

        public Example GetControlExample(string controlName, string exampleName)
        {
            var control = this.GetControlByName(controlName);
            return control.Examples.Where(p => p.Name == exampleName).FirstOrDefault();
        }
    }
}
