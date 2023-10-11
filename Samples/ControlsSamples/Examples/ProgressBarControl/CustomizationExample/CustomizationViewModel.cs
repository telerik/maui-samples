using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using QSF.ViewModels;
using System;

namespace QSF.Examples.ProgressBarControl.CustomizationExample;

public class CustomizationViewModel : ExampleViewModel
{
    private string uploadButtonText = "Upload";
    private string statusText = "Proceed to cloud upload";
    private int uploadProgress;
    private bool isUploadInProgress;

    private readonly IDispatcherTimer timer = App.Current.Dispatcher.CreateTimer();

    public CustomizationViewModel()
    {
        this.UploadCommand = new Command(Upload, CanUpload);
    }

    public Command UploadCommand { get; }

    public int UploadProgress
    {
        get { return this.uploadProgress; }
        set { this.UpdateValue(ref this.uploadProgress, value); }
    }

    public string UploadButtonText
    {
        get { return this.uploadButtonText; }
        set { this.UpdateValue(ref this.uploadButtonText, value); }
    }

    public string StatusText
    {
        get { return this.statusText; }
        set { this.UpdateValue(ref this.statusText, value); }
    }

    private bool CanUpload()
    {
        return this.isUploadInProgress == false;
    }

    private void Upload()
    {
        this.ResetUpload();

        this.timer.Interval = TimeSpan.FromMilliseconds(20);

        this.timer.Tick += (s, e) =>
        {
            this.UploadProgress++;
            this.StatusText = "Uploading your files to cloud... " + this.UploadProgress + "%";

            if (this.UploadProgress == 100)
            {
                this.timer.Stop();
                this.CompleteUpload();
            }
        };

        App.Current.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(2), this.timer.Start);
    }

    public void ResetUpload()
    {
        this.UploadProgress = 0;
        this.isUploadInProgress = true;
        this.UploadCommand.ChangeCanExecute();
        this.UploadButtonText = "Uploading...";
        this.StatusText = "Uploading your files to cloud...";
    }

    public void CompleteUpload()
    {
        this.isUploadInProgress = false;
        this.UploadCommand.ChangeCanExecute();
        this.UploadButtonText = "Upload more files";
        this.StatusText = "Successfully uploaded your files to cloud.";
    }
}
