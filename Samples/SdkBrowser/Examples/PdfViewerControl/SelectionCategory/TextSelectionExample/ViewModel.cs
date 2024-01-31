using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.PdfViewer;

namespace SDKBrowserMaui.Examples.PdfViewerControl.SelectionCategory.TextSelectionExample
{
    // >> pdfviewer-textselection-viewmodel
    public class ViewModel : NotifyPropertyChangedBase
    {
        public ViewModel()
        {
            this.GetSelectedTextCommand = new DisplaySelectedTextCommand();
        }

        public ICommand GetSelectedTextCommand { get; set; }

        class DisplaySelectedTextCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                PdfViewerSelectionCommandContext context = parameter as PdfViewerSelectionCommandContext;
                return context != null;
            }

            public void Execute(object parameter)
            {
                PdfViewerSelectionCommandContext context = (PdfViewerSelectionCommandContext)parameter;
                var selection = context.PdfViewer.Document.Selection;
                Application.Current.MainPage.DisplayAlert("Selected Text", selection.GetSelectedText(), "OK");

                lock (selection)
                {
                    selection.Clear();
                }
            }
        }
    }

    // << pdfviewer-textselection-viewmodel
}
