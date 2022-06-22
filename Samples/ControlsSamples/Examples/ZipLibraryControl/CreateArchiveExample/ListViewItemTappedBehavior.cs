using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;
using Telerik.Maui.Controls.Compatibility.DataControls;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView;

namespace QSF.Examples.ZipLibraryControl.CreateArchiveExample
{
    public class ListViewItemTappedBehavior : Behavior<RadListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ListViewItemTappedBehavior), null);

        public static readonly BindableProperty InputConverterProperty =
            BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(ListViewItemTappedBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public RadListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(RadListView bindable)
        {
            base.OnAttachedTo(bindable);

            this.AssociatedObject = bindable;
            bindable.BindingContextChanged += this.OnBindingContextChanged;
            bindable.ItemTapped += this.OnListViewItemTapped;
        }

        protected override void OnDetachingFrom(RadListView bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= this.OnBindingContextChanged;
            bindable.ItemTapped -= this.OnListViewItemTapped;

            this.AssociatedObject = null;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }

        private void OnListViewItemTapped(object sender, ItemTapEventArgs e)
        {
            if (this.Command == null)
            {
                return;
            }

            object parameter = e;
            if (this.Converter != null)
            {
                parameter = this.Converter.Convert(e, typeof(object), null, null);
            }

            if (this.Command.CanExecute(parameter))
            {
                this.Command.Execute(parameter);
            }
        }
    }
}
