using Microsoft.Maui.Controls;

namespace QSF.Pages
{
    public partial class SearchPageMobile : QSFPage
    {
        public override Grid SafeAreaGridWithHeader => this.root;

#if IOS
    public override View Acrylic => this.acrylic;
#endif

        public override Grid ContentGrid => this.content;

        public SearchPageMobile()
        {
            this.InitializeComponent();
            this.BaseInitializeComponent();
        }
    }
}
