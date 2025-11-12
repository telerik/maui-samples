using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using Telerik.Maui.Controls;

namespace QSF.Examples.CollectionViewControl.DragDropExample;

public class WorkItem : NotifyPropertyChangedBase
{
    private string title;
    private string description;
    private string assignee;
    private string assigneeAvatarColorKey;
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

    public string AssigneeAvatarColorKey
    {
        get => this.assigneeAvatarColorKey;
        set => this.UpdateValue(ref this.assigneeAvatarColorKey, value);
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
