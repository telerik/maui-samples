﻿using QSF.Common;
using System.Threading.Tasks;

namespace QSF.Services
{
    public interface INavigationService
    {
        public Task NavigateToAsync<TViewModel>(params object[] arguments);

        public Task NavigateToExampleAsync(Example example);

        public Task NavigateToRootAsync();

        public Task NavigateBackAsync();
    }
}