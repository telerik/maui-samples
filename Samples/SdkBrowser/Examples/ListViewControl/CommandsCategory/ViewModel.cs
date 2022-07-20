﻿using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Windows.Input;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands;

namespace SDKBrowserMaui.Examples.ListViewControl.CommandsCategory
{
    // >> listview-features-commands-viewmodel
    public class ViewModel
    {
        public ViewModel()
        {
            this.Source = new List<string> { "Tom", "Anna", "Peter", "Teodor", "Martin" };
            this.ItemTapCommand = new Command<ItemTapCommandContext>(this.ItemTapped);
        }
        private void ItemTapped(ItemTapCommandContext context)
        {
            var tappedItem = context.Item;
            //add your logic here
            App.DisplayAlert("You've selected " + tappedItem);
        }
        public List<string> Source { get; set; }
        public ICommand ItemTapCommand { get; set; }
    }
    // << listview-features-commands-viewmodel
}
