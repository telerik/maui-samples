using Microsoft.Maui.Devices;
using QSF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace QSF.Services
{
    /// <summary>
    /// The ConfigurationService is responsible for collecting all useful data from config.xml file.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        private const string ResourceLocation = "QSF.config.xml";

        public ConfigurationService()
        {
            this.Configuration = LoadConfiguration(ResourceLocation);
        }

        public Configuration Configuration { get; private set; }

        public IEnumerable<Control> GetControlsConfiguration()
        {
            return this.Configuration.Controls;
        }

        private static Configuration LoadConfiguration(string resource)
        {
            var type = typeof(ConfigurationService);
            var typeInfo = type.GetTypeInfo();
            var assembly = typeInfo.Assembly;

            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                var serializer = new XmlSerializer(typeof(Configuration));
                var configuration = (Configuration)serializer.Deserialize(stream);

                UpdateConfiguration(configuration);
                UpdateControlStatuses(configuration);

                return configuration;
            }
        }

        private static void UpdateControlStatuses(Configuration configuration)
        {
            var count = configuration.Controls.Count;
            for (int index = 0; index < count; index++)
            {
                var control = configuration.Controls[index];
                if (control.Status == StatusType.Normal 
                    && control.Examples.Any(e => e.Status == StatusType.New || e.Status == StatusType.Updated))
                {
                    // If we have updated or new example we want to mark the control as updated as well.
                    control.Status = StatusType.Updated;
                }
            }
        }

        private static void UpdateConfiguration(Configuration configuration)
        {
            var count = configuration.Controls.Count;

            for (int index = count - 1; index >= 0; index--)
            {
                var control = configuration.Controls[index];

                control.Configuration = configuration;

                if (IsExcluded(control.ExcludeFrom))
                {
                    configuration.Controls.RemoveAt(index);
                }
                else
                {
                    UpdateControl(control);

                    if (control.Examples.Count == 0)
                    {
                        configuration.Controls.RemoveAt(index);
                    }
                }
            }
        }

        private static void UpdateControl(Control control)
        {
            var count = control.Examples.Count;

            for (int index = count - 1; index >= 0; index--)
            {
                var example = control.Examples[index];

                if (IsExcluded(example.ExcludeFrom))
                {
                    control.Examples.RemoveAt(index);
                }
            }
        }

        private static bool IsExcluded(string exclusions)
        {
            if (string.IsNullOrEmpty(exclusions))
            {
                return false;
            }

            var platforms = exclusions.Split(',');

            return platforms.Contains(DeviceInfo.Platform.ToString(),
                StringComparer.OrdinalIgnoreCase);
        }
    }
}
