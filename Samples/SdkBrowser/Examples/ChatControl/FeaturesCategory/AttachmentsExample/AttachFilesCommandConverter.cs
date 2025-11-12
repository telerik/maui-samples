using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Chat;

namespace SDKBrowserMaui.Examples.ChatControl.FeaturesCategory.AttachmentsExample;

// >> chat-attached-file-command-converter

/// <summary>
/// A custom converter that converts from chat specific objects to business objects, 
/// so that the ViewModel does not have to handle chat specific classes.
/// </summary>
public class AttachFilesCommandConverter : IValueConverter
{
	private static AttachFilesCommandConverter instance;

	public static AttachFilesCommandConverter Instance => instance ??= new AttachFilesCommandConverter();

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is ICommand command)
		{
			return new AttachFilesCommand(command);
		}
		else
		{
			return value;
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

	class AttachFilesCommand : ICommand
	{
		private ICommand command;

		public AttachFilesCommand(ICommand command)
		{
			this.command = command;
			this.command.CanExecuteChanged += (s, e) => this.CanExecuteChanged?.Invoke(this, e);
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			if (parameter is IList<IFileInfo> filesToAttach)
			{
				List<AttachedFileData> attachedFileDatas = ToAttachedFileDatas(filesToAttach);
				return this.command.CanExecute(attachedFileDatas);
			}
			else
			{
				return false;
			}
		}

		public void Execute(object parameter)
		{
			if (parameter is IList<IFileInfo> filesToAttach)
			{
				List<AttachedFileData> attachedFileDatas = ToAttachedFileDatas(filesToAttach);
				this.command.Execute(attachedFileDatas);

				if (attachedFileDatas.Count == 0)
				{
					// Instruct the RadChat to not attempt to auto add files.
					filesToAttach.Clear();
				}
			}
			else
			{
				throw new InvalidOperationException($"The command parameter must be of type {nameof(IList<IFileInfo>)}.");
			}
		}

		private static List<AttachedFileData> ToAttachedFileDatas(IList<IFileInfo> filesToAttach)
		{
			return new List<AttachedFileData>(filesToAttach.Select(AttachedFileConverter.CreateAttachedFileData));
		}
	}
}
// << chat-attached-file-command-converter
