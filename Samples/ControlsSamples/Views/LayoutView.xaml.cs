using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System.Collections.Generic;
using Telerik.Maui;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
[ContentProperty(nameof(Children))]
public partial class LayoutView : ContentView
{
    public static readonly BindableProperty OrientationProperty = BindableProperty.Create(
        nameof(Orientation), typeof(ScrollOrientation), typeof(LayoutView), ScrollOrientation.Vertical,
        propertyChanged: (b, o, n) => ((LayoutView)b).OnOrientationChanged());

    public static readonly BindableProperty LayoutProperty = BindableProperty.Create(
        nameof(Layout), typeof(Layout), typeof(LayoutView),
        propertyChanged: (b, o, n) => ((LayoutView)b).OnLayoutChanged((Layout)o));

    private readonly VerticalStackLayout verticalStackLayout;
    private readonly ScrollView scrollView;

    public LayoutView()
    {
        this.InitializeComponent();

        this.verticalStackLayout = new VerticalStackLayout();
        this.scrollView = new ScrollView();
        this.Children = this.CreateDefaultChildren();
    }

    public new IList<IView> Children { get; }

    public ScrollOrientation Orientation
    {
        get { return (ScrollOrientation)this.GetValue(OrientationProperty); }
        set { this.SetValue(OrientationProperty, value); }
    }

    new public Layout Layout
    {
        get { return (Layout)this.GetValue(LayoutProperty); }
        set { this.SetValue(LayoutProperty, value); }
    }

    private Layout ActualLayout { get { return this.Layout ?? this.verticalStackLayout; } }

    private IList<IView> CreateDefaultChildren()
    {
        ObservableItemCollection<IView> children = new ObservableItemCollection<IView>();
        children.ItemAdded += this.Children_ItemAdded;
        children.ItemRemoved += this.Children_ItemRemoved;
        return children;
    }

    private void OnOrientationChanged()
    {
        this.UpdateContent();
        this.scrollView.Orientation = this.Orientation;
    }

    private void OnLayoutChanged(Layout oldLayout)
    {
        oldLayout?.Children.Clear();
        this.ActualLayout.Children.Clear();

        this.UpdateContent();

        foreach (IView view in this.Children)
        {
            this.ActualLayout.Children.Add(view);
        }
    }

    private void UpdateContent()
    {
        if (this.Orientation == ScrollOrientation.Neither)
        {
            this.scrollView.Content = null;
            this.Content = this.ActualLayout;
        }
        else
        {
            this.Content = this.scrollView;
            this.scrollView.Content = this.ActualLayout;
        }
    }

    private void Children_ItemAdded(object sender, ObservableItemCollectionChangedEventArgs<IView> args)
    {
        this.ActualLayout.Children.Insert(args.Index, args.Item);
    }

    private void Children_ItemRemoved(object sender, ObservableItemCollectionChangedEventArgs<IView> args)
    {
        this.ActualLayout.Children.RemoveAt(args.Index);
    }
}