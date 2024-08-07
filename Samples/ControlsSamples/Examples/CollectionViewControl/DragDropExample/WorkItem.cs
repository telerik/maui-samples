using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.DragDropExample;

public class WorkItem : NotifyPropertyChangedBase
{
    private string title;
    private string description;
    private string assignee;
    private Color assigneeAvatarColor;
    private List<Color> tags;

    public string Title
    {
        get => this.title;
        set => this.UpdateValue(ref this.title, value);
    }

    public string Description
    {
        get => this.description;
        set => this.UpdateValue(ref this.description, value);
    }

    public string Assignee
    {
        get => this.assignee;
        set => this.UpdateValue(ref this.assignee, value);
    }

    public Color AssigneeAvatarColor
    {
        get => this.assigneeAvatarColor;
        set => this.UpdateValue(ref this.assigneeAvatarColor, value);
    }

    public string AssigneeAbbreviation
    {
        get => this.Assignee.Substring(0, 2);
    }

    public List<Color> Tags
    {
        get => this.tags; 
        set => this.UpdateValue(ref this.tags, value);
    }
}
