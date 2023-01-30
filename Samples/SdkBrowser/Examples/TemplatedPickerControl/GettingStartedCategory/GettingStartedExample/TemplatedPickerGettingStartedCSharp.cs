using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls;

namespace SDKBrowserMaui.Examples.TemplatedPickerControl.GettingStartedCategory.GettingStartedExample
{
    public class TemplatedPickerGettingStartedCSharp : RadContentView
    {
        public TemplatedPickerGettingStartedCSharp()
        {
            // >> templatedpicker-getting-started-csharp
            var viewModel = new ColorViewModel();
            var templatedPicker = new RadTemplatedPicker
            {
                Placeholder = "Select a Color"
            };
            var displayTemplate = new ControlTemplate(() =>
            {
                var displayLayout = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition
                        {
                            Width = GridLength.Auto
                        },
                        new ColumnDefinition
                        {
                            Width = GridLength.Star
                        }
                    }
                };
                var displayLabel = new Label();
                var displayBorder = new RadBorder
                {
                    WidthRequest = 20,
                    HeightRequest = 20,
                    CornerRadius = 10,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    BorderColor = Colors.LightGray,
                    BorderThickness = 1
                };
                var styleBinding = new Binding
                {
                    Path = "DisplayLabelStyle",
                    Source = RelativeBindingSource.TemplatedParent
                };
                var textBinding = new Binding
                {
                    Path = "SelectedValue"
                };
                var colorBinding = new Binding
                {
                    Path = "SelectedColor"
                };
                var commandBinding = new Binding
                {
                    Path = "ToggleCommand",
                    Source = RelativeBindingSource.TemplatedParent
                };
                var tapRecognizer = new TapGestureRecognizer();

                Grid.SetColumn(displayLabel, 1);

                tapRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, commandBinding);
                displayBorder.SetBinding(VisualElement.BackgroundColorProperty, colorBinding);
                displayLabel.SetBinding(VisualElement.StyleProperty, styleBinding);
                displayLabel.SetBinding(Label.TextProperty, textBinding);
                displayLayout.Children.Add(displayBorder);
                displayLayout.Children.Add(displayLabel);
                displayLayout.GestureRecognizers.Add(tapRecognizer);

                return displayLayout;
            });
            var selectorTemplate = new ControlTemplate(() =>
            {
                var collectionView = new CollectionView
                {
                    HeightRequest = 240,
                    SelectionMode = SelectionMode.Single,
                };
                var itemsSourceBinding = new Binding
                {
                    Path = "Colors"
                };
                var selectedItemBinding = new Binding
                {
                    Path = "SelectedValue",
                    Source = RelativeBindingSource.TemplatedParent,
                    Mode = BindingMode.TwoWay
                };
                var itemTemplate = new DataTemplate(() =>
                {
                    var itemLayout = new Grid
                    {
                        HeightRequest = 60
                    };
                    var itemBorder = new RadBorder
                    {
                        WidthRequest = 40,
                        HeightRequest = 40,
                        CornerRadius = 20,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        BorderColor = Colors.LightGray,
                        BorderThickness = 2
                    };
                    var itemBinding = new Binding(".");
                    var visualStates = new VisualStateGroupList
                    {
                        new VisualStateGroup
                        {
                            Name = "CommonStates",
                            States =
                            {
                                new VisualState
                                {
                                    Name = "Normal",
                                    Setters =
                                    {
                                        new Setter
                                        {
                                            Property = VisualElement.BackgroundColorProperty,
                                            Value = DeviceInfo.Platform == DevicePlatform.WinUI ? Colors.Transparent : Colors.White
                                        }
                                    }
                                },
                                new VisualState
                                {
                                    Name = "Selected",
                                    Setters =
                                    {
                                        new Setter
                                        {
                                            Property = VisualElement.BackgroundColorProperty,
                                            Value = Colors.WhiteSmoke
                                        }
                                    }
                                }
                            }
                        }
                    };

                    itemBorder.SetBinding(VisualElement.BackgroundColorProperty, itemBinding);
                    itemLayout.Children.Add(itemBorder);
                    VisualStateManager.SetVisualStateGroups(itemLayout, visualStates);

                    return itemLayout;
                });
                var itemsLayout = new GridItemsLayout(ItemsLayoutOrientation.Vertical)
                {
                    Span = 5
                };

                collectionView.ItemTemplate = itemTemplate;
                collectionView.ItemsLayout = itemsLayout;
                collectionView.SetBinding(ItemsView.ItemsSourceProperty, itemsSourceBinding);
                collectionView.SetBinding(SelectableItemsView.SelectedItemProperty, selectedItemBinding);

                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    collectionView.SetBinding(SelectableItemsView.WidthRequestProperty, new Binding { Path = nameof(templatedPicker.Width), Source = templatedPicker });
                    collectionView.BackgroundColor = Colors.Transparent;
                }

                return collectionView;
            });
            var selectionBinding = new Binding
            {
                Path = "SelectedColor"
            };

            templatedPicker.SetBinding(RadTemplatedPicker.SelectedValueProperty, selectionBinding);
            templatedPicker.BindingContext = viewModel;
            templatedPicker.DisplayTemplate = displayTemplate;
            templatedPicker.SelectorTemplate = selectorTemplate;
            // << templatedpicker-getting-started-csharp
            var stackLayout = new VerticalStackLayout();
            stackLayout.WidthRequest = 300;
            stackLayout.HorizontalOptions = LayoutOptions.Center;
            stackLayout.Children.Add(templatedPicker);
            this.Content = stackLayout;
        }
    }
}
