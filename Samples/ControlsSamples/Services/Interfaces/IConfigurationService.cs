using QSF.Common;
using System.Collections.Generic;

namespace QSF.Services
{
    public interface IConfigurationService
    {
        public Configuration Configuration { get; }

        public IEnumerable<Control> GetControlsConfiguration();
    }
}
