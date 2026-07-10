using TelerikCRM.Maui.ViewModels;

namespace TelerikCRM.Maui.Views;

public partial class ProductEditView
{
    public ProductEditView()
    {
        this.InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (this.BindingContext is ProductEditViewModel editContext)
        {
            Command commitCommand = (Command)this.dataForm.CommitCommand;
#if MACCATALYST || WINDOWS
            editContext.SaveCommand = commitCommand;
#else
            editContext.SaveCommand = new Command(async (parameter) =>
            {
                commitCommand.Execute(null);

                // NOTE: Commented code is applicable for when app is not in read-only mode
                // ((ProductEditViewModel)parameter).SelectedProduct.CopyFrom(editContext.SelectedProduct);
                await editContext.UpdateDatabaseAsync();

                // await DependencyService.Get<INavigationService>().NavigateBackAsync();
            });
#endif
        }
    }
}