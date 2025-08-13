using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKBrowserMaui.Common;

namespace SDKBrowserMaui.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(params object[] arguments);

        Task NavigateToRootAsync(bool isAnimated = true);

        Task NavigateBackAsync(bool isAnimated = true);

        Task NavigateToExampleAsync<TViewModel>(Example example, bool popToMain = false, bool isAnimated = true);
    }
}
