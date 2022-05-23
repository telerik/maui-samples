using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System.Windows.Input;
using System.Xml.Serialization;

namespace QSF.Examples.PopupControl.ContextMenuExample
{
    [XmlType("Folder")]
    public class FolderViewModel : ViewModelBase
    {
        private string name;
        private string icon;
        private string path;

        [XmlAttribute("Name")]
        public string Name
        {
            get => this.name;
            set => this.UpdateValue(ref this.name, value);
        }

        public string Icon
        {
            get => this.icon;
            set => this.UpdateValue(ref this.icon, value);
        }

        public string Path
        {
            get => this.path;
            set => this.UpdateValue(ref this.path, value);
        }
    }
}
