using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKBrowserMaui.Services
{
    public interface IBackdoorService
    {
        Task<string> NavigateToExampleAsync(string controlName, string exampleName);

        Task NavigateToSearchAsync();

        Task NavigateBackAsync();

        bool TrySetTheme(string themeName, string themeSwatch);
    }
}
