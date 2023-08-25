using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.ItemTemplateSelectorExample
{
    // >> chat-features-itemtemplate-items
    public class ViewModel : NotifyPropertyChangedBase
    {
        private Author me;
        public ViewModel()
        {
            this.Me = new Author() { Name = "human", Avatar = "sampleavatar.png" };
            this.Bot = new Author() { Name = "Bot", Avatar = "samplebot.png" };
            this.Items = new ObservableCollection<SimpleChatItem>();

            // Simulate async data loading
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                this.Items.Add(new SimpleChatItem { Author = this.Bot, Text = "Hello.", Category = MessageCategory.Normal });
                this.Items.Add(new SimpleChatItem { Author = this.Bot, Text = "Here is what you need to know about our updated privacy policy: ...", Category = MessageCategory.Important });
                return false;
            });
        }

        public Author Me
        {
            get
            {
                return this.me;
            }
            set
            {
                if (this.me != value)
                {
                    this.me = value;
                    this.OnPropertyChanged(nameof(this.Me));
                }
            }
        }
        public Author Bot { get; set; }
        public IList<SimpleChatItem> Items { get; set; }
    }
    // << chat-features-itemtemplate-items
}

