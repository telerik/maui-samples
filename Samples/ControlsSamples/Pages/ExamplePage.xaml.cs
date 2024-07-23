using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using QSF.Common;
using QSF.ViewModels;
using QSF.Services;

namespace QSF.Pages
{
    public partial class ExamplePage : QSFPage
    {
        public override Grid SafeAreaGridWithHeader => this.root;

#if IOS
    public override View Acrylic => this.acrylic;
#endif

        public override Grid ContentGrid => this.content;

        public ExamplePage()
        {
            this.InitializeComponent();
            this.BaseInitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            ExampleViewModel vm = (ExampleViewModel)this.BindingContext;
            await vm.NavigationService.NavigateBackAsync();
        }

        private async void OnExampleSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            await DependencyService.Get<INavigationService>().NavigateCommand(e.NewTextValue);
        }
    }
}
