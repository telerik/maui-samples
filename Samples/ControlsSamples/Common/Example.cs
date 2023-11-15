using System.Xml.Serialization;

namespace QSF.Common;

public class Example
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    [XmlIgnore]
    public string Icon => TelerikControlsIcons.GetExampleIcon(this);

    public string CodeUrl { get; set; }

    public string Page { get; set; }

    public string Description { get; set; }

    public string ExcludeFrom { get; set; }

    public string ControlName { get; set; }

    public bool IsConfigurable { get; set; }

    public StatusType Status { get; set; }
}
