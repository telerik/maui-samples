using System.Collections.Generic;
using System.Xml.Serialization;

namespace QSF.Common;

public class Control
{
    public Control()
    {
        this.IsThemable = true;
    }

    public string Name { get; set; }

    public string Icon { get; set; }

    public string DisplayName { get; set; }

    public string DescriptionHeader { get; set; }

    public string ShortDescription { get; set; }

    public string FullDescription { get; set; }

    public string DocumentationURL { get; set; }

    public string FeedbackPortalUrl { get; set; }

    public bool IsNew { get; set; }

    public bool IsCTP { get; set; }

    public bool IsBeta { get; set; }

    public bool IsUpdated { get; set; }

    public bool IsThemable { get; set; }

    public List<Example> Examples { get; set; }

    [XmlIgnore]
    public Configuration Configuration { get; set; }

    public string ExcludeFrom { get; set; }
}
