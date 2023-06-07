using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Maui.Controls;
using System.Linq;

namespace SDKBrowserMaui.Examples.TreeViewControl.EventsCategory
{
    // >> treeview-events-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        private ObservableCollection<Data> selectedData;

        public ViewModel()
        {
            this.Source = new ObservableCollection<Data>();
            this.Source.Add(new Data("Data 1")
            {
                Children = new List<Data>()
                {
                    new Data("Data 11")
                    {
                        Children = new List<Data>()
                        {
                            new Data("Data 111"),
                            new Data("Data 112"),
                            new Data("Data 113")
                        }
                    },
                    new Data("Data 12")
                }
            });
            this.Source.Add(new Data("Data 2")
            {
                Children = new List<Data>()
                {
                    new Data("Data 21")
                    {
                        Children = new List<Data>()
                        {
                            new Data("Data 211"),
                            new Data("Data 212"),
                            new Data("Data 213")
                        }
                    },
                    new Data("Data 22")
                    {
                        Children = new List<Data>()
                        {
                            new Data("Data 221"),
                            new Data("Data 222"),
                            new Data("Data 223")
                        }
                    }
                }
            });
            this.Source.Add(new Data("Data 3")
            {
                Children = new List<Data>()
                {
                    new Data("Data 31")
                    {
                        Children = new List<Data>()
                        {
                            new Data("Data 311"),
                            new Data("Data 312"),
                            new Data("Data 313")
                        }
                    },
                    new Data("Data 32")
                    {
                        Children = new List<Data>()
                        {
                            new Data("Data 321"),
                            new Data("Data 322"),
                            new Data("Data 323")
                        }
                    }
                }
            });

            this.SelectedData = new ObservableCollection<Data>();
            this.SelectedData.Add(this.Source.First());

        }
        public ObservableCollection<Data> Source { get; set; }

        public ObservableCollection<Data> SelectedData
        {
            get => this.selectedData;
            set => this.UpdateValue(ref this.selectedData, value);
        }
    }
    // << treeview-events-viewmodel
}
