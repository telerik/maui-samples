using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Maui.Controls;
using QSF.Examples.TreeViewControl.Common;
using QSF.ExampleUtilities;
using QSF.ViewModels;
using Telerik.Maui.Controls.TreeView;

namespace QSF.Examples.TreeViewControl.ConfigurationExample;

public class ConfigurationViewModel : ConfigurationExampleViewModel
{
    private ObservableCollection<FolderNode> folders;

    // TreeView Settings
    private SelectionMode selectionMode = SelectionMode.Single;
    private TreeViewCheckBoxMode checkBoxMode = TreeViewCheckBoxMode.None;
    private double levelIndentation;

    // TreeViewItem Settings
    private bool isItemExpandButtonVisible = true;
    private bool isItemImageVisible;
    private double itemSpacing;

    public ConfigurationViewModel()
    {
#if __ANDROID__
        this.LevelIndentation = 48;
#endif

#if __IOS__
        this.LevelIndentation = 44;
#endif

#if MACCATALYST
        this.LevelIndentation = 24;
#endif

#if WINDOWS
        this.LevelIndentation = 26;
#endif

        this.Folders = DataGenerator.GetItems<ObservableCollection<FolderNode>>(DataSourcePaths.FileExplorerPath);
    }

    public IEnumerable<SelectionMode> SelectionModes { get; } = Enum.GetValues(typeof(SelectionMode)).Cast<SelectionMode>();

    public IEnumerable<TreeViewCheckBoxMode> CheckBoxModes { get; } = Enum.GetValues(typeof(TreeViewCheckBoxMode)).Cast<TreeViewCheckBoxMode>();

    [XmlArray("Folders")]
    [XmlArrayItem("Folder")]
    public ObservableCollection<FolderNode> Folders
    {
        get { return this.folders; }
        set { this.UpdateValue(ref this.folders, value); }
    }

    public SelectionMode SelectionMode
    {
        get { return this.selectionMode; }
        set { this.UpdateValue(ref this.selectionMode, value); }
    }

    public TreeViewCheckBoxMode CheckBoxMode
    {
        get { return this.checkBoxMode; }
        set { this.UpdateValue(ref this.checkBoxMode, value); }
    }

    public double LevelIndentation
    {
        get { return this.levelIndentation; }
        set { this.UpdateValue(ref this.levelIndentation, value); }
    }

    public bool IsItemExpandButtonVisible
    {
        get { return this.isItemExpandButtonVisible; }
        set { this.UpdateValue(ref this.isItemExpandButtonVisible, value); }
    }

    public bool IsItemImageVisible
    {
        get { return this.isItemImageVisible; }
        set { this.UpdateValue(ref this.isItemImageVisible, value); }
    }

    public double ItemSpacing
    {
        get { return this.itemSpacing; }
        set { this.UpdateValue(ref this.itemSpacing, value); }
    }
}