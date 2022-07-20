using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace QSF.Examples.BorderControl.ConfigurationExample
{
    public partial class ConfigurationView : ContentView
    {
        private ConfigurationViewModel viewModel;

        public ConfigurationView()
        {
            InitializeComponent();
            this.viewModel = new ConfigurationViewModel();
            this.BindingContext = this.viewModel;
        }

        void LeftCornerRadiusSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var cornerRadius = (int)e.NewValue;
            this.viewModel.LeftCornerRadius = cornerRadius;
            this.viewModel.LeftCornerRadiusLabelText = "L: " + cornerRadius;

            var oldBorderCornerRadius = this.viewModel.BorderCornerRadius;
            this.viewModel.BorderCornerRadius = new Thickness(cornerRadius,
                                                              oldBorderCornerRadius.Top,
                                                              oldBorderCornerRadius.Right,
                                                              oldBorderCornerRadius.Bottom);
        }

        void TopCornerRadiusSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var cornerRadius = (int)e.NewValue;
            this.viewModel.TopCornerRadius = (int)e.NewValue;
            this.viewModel.TopCornerRadiusLabelText = "T: " + cornerRadius;
            var oldBorderCornerRadius = this.viewModel.BorderCornerRadius;
            this.viewModel.BorderCornerRadius = new Thickness(oldBorderCornerRadius.Left,
                                                              cornerRadius,
                                                              oldBorderCornerRadius.Right,
                                                              oldBorderCornerRadius.Bottom);
        }

        void RightCornerRadiusSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var cornerRadius = (int)e.NewValue;
            this.viewModel.RightCornerRadius = (int)e.NewValue;
            this.viewModel.RightCornerRadiusLabelText = "R: " + cornerRadius;
            var oldBorderCornerRadius = this.viewModel.BorderCornerRadius;
            this.viewModel.BorderCornerRadius = new Thickness(oldBorderCornerRadius.Left,
                                                              oldBorderCornerRadius.Top,
                                                              cornerRadius,
                                                              oldBorderCornerRadius.Bottom);
        }

        void BottomCornerRadiusSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var cornerRadius = (int)e.NewValue;
            this.viewModel.BottomCornerRadius = (int)e.NewValue;
            this.viewModel.BottomCornerRadiusLabelText = "B: " + cornerRadius;
            var oldBorderCornerRadius = this.viewModel.BorderCornerRadius;
            this.viewModel.BorderCornerRadius = new Thickness(oldBorderCornerRadius.Left,
                                                              oldBorderCornerRadius.Top,
                                                              oldBorderCornerRadius.Right,
                                                              cornerRadius);
        }

        void BorderThicknessSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            var cornerRadius = (int)e.NewValue;
            this.viewModel.BorderThickness = new Thickness(cornerRadius);
        }
    }
}
