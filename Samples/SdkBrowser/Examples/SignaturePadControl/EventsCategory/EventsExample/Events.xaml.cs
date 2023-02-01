using Microsoft.Maui.Controls;
using System;


namespace SDKBrowserMaui.Examples.SignaturePadControl.EventsCategory.EventsExample;

public partial class Events : ContentView
{
	public Events()
	{
		InitializeComponent();
	}
    // >> signaturepad-strokestarted-event
    private void RadSignaturePad_StrokeStarted(object sender, EventArgs e)
    {
        this.timeStampLabel.Text = DateTime.Now.ToString();
        this.logInfo.Text = "";

    }
    // << signaturepad-strokestarted-event

    // >> signaturepad-strokecompleted-event
    private void RadSignaturePad_StrokeCompleted(object sender, EventArgs e)
    {
        this.timeStampLabel.Text = DateTime.Now.ToString();
    }
    // << signaturepad-strokecompleted-event

    // >> signaturepad-cleared-event
    private void RadSignaturePad_Cleared(object sender, EventArgs e)
    {
        this.logInfo.Text = "Cleared";
        this.timeStampLabel.Text = "";
    }
    // << signaturepad-cleared-event
}