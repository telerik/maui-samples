#if ANDROID
using Android.App;
using Android.Widget;

namespace QSF.Services;

partial class ToastMessageService
{
    public void ShortAlert(string message)
    {
        var toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
        toast.Show();
    }
}
#endif