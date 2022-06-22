using Microsoft.Maui.Graphics;
using Telerik.Maui.Controls.Compatibility.Primitives;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Telerik.Maui.Controls;

namespace QSF.Examples.BadgeViewControl.FirstLookExample
{
    public class User : NotifyPropertyChangedBase
    {
        private string unreadMessagesText;
        private Color highLightedTextColor = Application.Current.RequestedTheme == AppTheme.Light ? Color.FromArgb("#0E88F2") : Color.FromArgb("#42A5F5");
        private Color defaultTextColor = Application.Current.RequestedTheme == AppTheme.Light ? Color.FromArgb("#99000000") : Color.FromArgb("#99FFFFFF");

        public string Name { get; set; }

        public string LastMessageReceived { get; set; }

        public string ImageSourcePath { get; set; }

        public BadgeType ActivityStatus { get; set; }

        public string UnreadMessagesText
        {
            get => this.unreadMessagesText;
            set => UpdateValue(ref this.unreadMessagesText, value);
        }

        public string LastMessageReceivedDate { get; set; }

        public Color LastMessageReceivedDateColor
        {
            get
            {
                return this.UnreadMessagesText != null ? this.highLightedTextColor : this.defaultTextColor;
            }
        }

        public FontAttributes MessageFontAttributes
        {
            get
            {
                return this.UnreadMessagesText != null ? FontAttributes.Bold : FontAttributes.None;
            }
        }

        public bool IsVisibleUnreadMessages
        {
            get
            {
                return this.UnreadMessagesText != null;
            }
        }
    }
}
