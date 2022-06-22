using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using QSF.Layouts;
using System.Collections.Generic;
using Telerik.Maui;

namespace QSF.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
[ContentProperty(nameof(Children))]
public partial class UniformView : ContentView
{
    public static readonly BindableProperty LayoutModeProperty = BindableProperty.Create(
        nameof(LayoutMode), typeof(UniformLayoutMode), typeof(UniformView), 
        defaultValueCreator: b => ((UniformView)b).CreateDefaultLayoutMode(), 
        propertyChanged: (b, o, n) => ((UniformView)b).OnLayoutModeChanged());

    public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
        nameof(Spacing), typeof(double), typeof(UniformView), 0.0,
        propertyChanged: (b, o, n) => ((UniformView)b).OnSpacingChanged());

    public UniformView()
    {
        this.InitializeComponent();

        this.Children = this.CreateDefaultChildren();
        this.UpdateLayoutModeAndControlTemplate();
    }

    public new IList<IView> Children { get; }

    public UniformLayoutMode LayoutMode
    {
        get { return (UniformLayoutMode)this.GetValue(LayoutModeProperty); }
        set { this.SetValue(LayoutModeProperty, value); }
    }

    public double Spacing
    {
        get { return (double)this.GetValue(SpacingProperty); }
        set { this.SetValue(SpacingProperty, value); }
    }

    private IList<IView> CreateDefaultChildren()
    {
        ObservableItemCollection<IView> children = new ObservableItemCollection<IView>();
        children.ItemAdded += this.Children_ItemAdded;
        children.ItemRemoved += this.Children_ItemRemoved;
        return children;
    }

    private UniformLayoutMode CreateDefaultLayoutMode()
    {
        return UniformLayout.DefaultLayoutMode;
    }

    private void OnLayoutModeChanged()
    {
        this.UpdateLayoutModeAndControlTemplate();
    }

    private void OnSpacingChanged()
    {
        this.UpdateLayoutSpacing();
    }

    private void UpdateLayoutModeAndControlTemplate()
    {
        this.layout.LayoutMode = this.LayoutMode;
        this.UpdateControlTemplate();
    }

    private void UpdateLayoutSpacing()
    {
        if (this.Content is UniformLayout uniformLayout)
        {
            uniformLayout.Spacing = this.Spacing;
        }
    }

    private void Children_ItemAdded(object sender, ObservableItemCollectionChangedEventArgs<IView> args)
    {
        this.layout.Children.Insert(args.Index, args.Item);
    }

    private void Children_ItemRemoved(object sender, ObservableItemCollectionChangedEventArgs<IView> args)
    {
        this.layout.Children.RemoveAt(args.Index);
    }

    private void UpdateControlTemplate()
    {
        if (this.LayoutMode == UniformLayoutMode.HorizontalStack)
        {
            this.ControlTemplate = (ControlTemplate)this.Resources["HorizontalStack_ControlTemplate"];
        }
        else if (this.LayoutMode == UniformLayoutMode.VerticalStack)
        {
            this.ControlTemplate = (ControlTemplate)this.Resources["VerticalStack_ControlTemplate"];
        }
        else
        {
            this.ControlTemplate = null;
        }
    }
}