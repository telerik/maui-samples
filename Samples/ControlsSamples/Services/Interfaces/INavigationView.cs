using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSF.Services.Interfaces
{
    internal interface INavigationView
    {
        void OnAppearing();
        void OnDisappearing();
    }
}
