using Microsoft.Maui.Controls;
using QSF.Examples.PdfProcessingControl.FirstLookExample;
using QSF.Examples.SpreadStreamProcessingControl.FirstLookExample;
using QSF.Services;
using QSF.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Telerik.AppUtils.Services;
using Telerik.Documents.Primitives;
using Telerik.Windows.Documents.Fixed.FormatProviders.Pdf;
using Telerik.Windows.Documents.Fixed.Model;
using Telerik.Windows.Documents.Fixed.Model.ColorSpaces;
using Telerik.Windows.Documents.Fixed.Model.Editing;
using Telerik.Windows.Documents.Fixed.Model.Editing.Flow;
using Telerik.Windows.Documents.Fixed.Model.Editing.Tables;

namespace QSF.Examples.DataGridControl.ExportToPdfExample;

public class ExportToPdfViewModel : ExampleViewModel
{
	private Random random;
	private FixedContentEditor pageEditor;
	private const string TitleColumnHeader = "TITLE";
	private const string UniversityColumnHeader = "UNIVERSITY";
	private const string ProgressColumnHeader = "PROGRESS";
	private const int CoursesCount = 30;
	private const string FileName = "my_courses.pdf";
	private static readonly string[] CourseNames = new string[] { "Data Science", "Machine Learning", "Big Data", "Product Management", "Business Foundations", "Python for Everybody", "Finance", "Manufacturing Engineering",
																	  "Architecture", "Art and Design", "Biological Sciences", "Chemical Engineering", "Chemistry", "Marketing", "Robotics"};
	private static readonly string[] Universities = new string[] { "John Hopkins University", "University of Washington", "University of California", "University of Pennsylvania", "University of Michigan", "Harvard University", "Stanford University" };

	private ObservableCollection<CourseViewModel> courses;
	private ICommand generatePdfCommand;

	public ExportToPdfViewModel()
	{
		this.random = DependencyService
			.Get<ITestingService>()
			.Random(30);
		this.GeneratePdfCommand = new Command(this.GeneratePdf);
		this.Courses = new ObservableCollection<CourseViewModel>(this.GenerateCourses());
	}

	public ObservableCollection<CourseViewModel> Courses
	{
		get
		{
			return this.courses;
		}
		private set
		{
			if (this.courses != value)
			{
				this.courses = value;
				this.OnPropertyChanged();
			}
		}
	}

	public ICommand GeneratePdfCommand
	{
		get
		{
			return this.generatePdfCommand;
		}
		private set
		{
			if (this.generatePdfCommand != value)
			{
				this.generatePdfCommand = value;
				this.OnPropertyChanged();
			}
		}
	}

	private IEnumerable<CourseViewModel> GenerateCourses()
	{
		for (int i = 0; i < CoursesCount; i++)
		{
			int courseIndex = this.random.Next(0, CourseNames.Length);
			int universityIndex = this.random.Next(0, Universities.Length);
			int progress = this.random.Next(0, 101);

			yield return new CourseViewModel(CourseNames[courseIndex], Universities[universityIndex], progress);
		}
	}

	private async void GeneratePdf(object obj)
	{
		RadFixedDocument document = this.CreateDocument();

		using (MemoryStream stream = new MemoryStream())
		{
			PdfFormatProvider pdfProvider = new PdfFormatProvider();
			pdfProvider.Export(document, stream, TimeSpan.FromMinutes(1));
			await DependencyService.Get<IFileViewerService>().View(stream, FileName);
		}
	}

	public RadFixedDocument CreateDocument()
	{
		RadFixedDocument document = new RadFixedDocument();
		RadFixedPage page = document.Pages.AddPage();
		page.Size = new Size(ExampleDocumentSizes.PageWidth, ExampleDocumentSizes.PageHeight);

		this.pageEditor = new FixedContentEditor(page);
		this.DrawTableContent(this.pageEditor);

		return document;
	}

	private void DrawTableContent(FixedContentEditor editor)
	{
		RgbColor headerColor = new RgbColor(51, 51, 51);
		RgbColor bordersColor = new RgbColor(205, 205, 205);
		RgbColor alternatingRowColor = new RgbColor(243, 243, 243);

		Telerik.Windows.Documents.Fixed.Model.Editing.Border border = new Telerik.Windows.Documents.Fixed.Model.Editing.Border(1, Telerik.Windows.Documents.Fixed.Model.Editing.BorderStyle.Single, bordersColor);

		Table table = new Table();
		table.Borders = new TableBorders(border);
		table.LayoutType = TableLayoutType.FixedWidth;
		table.DefaultCellProperties.Borders = new TableCellBorders(border, border, border, border);
		table.DefaultCellProperties.Padding = new Telerik.Documents.Primitives.Thickness(4);

		TableRow headerRow = table.Rows.AddTableRow();

		List<string> columns = new List<string> { TitleColumnHeader, UniversityColumnHeader, ProgressColumnHeader };

		foreach (string	column in columns)
		{
			TableCell headerCell = headerRow.Cells.AddTableCell();
			headerCell.Background = headerColor;

			Block block = headerCell.Blocks.AddBlock();
			block.GraphicProperties.FillColor = RgbColors.White;
			block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Center;
			block.InsertText(column);
	
		}

		for (int i = 0; i < this.Courses.Count; i++)
		{
			RgbColor rowColor = i % 2 == 0 ? alternatingRowColor : RgbColors.White;
			CourseViewModel course = this.Courses[i];

			TableRow courseRow = table.Rows.AddTableRow();

			TableCell cell = courseRow.Cells.AddTableCell();
			cell.Background = rowColor;

			Block block = cell.Blocks.AddBlock();
			block.InsertText(course.CourseName);

			cell = courseRow.Cells.AddTableCell();
			cell.Background = rowColor;

			block = cell.Blocks.AddBlock();
			block.InsertText(course.University);

			cell = courseRow.Cells.AddTableCell();
			cell.Background = rowColor;

			block = cell.Blocks.AddBlock();
			block.HorizontalAlignment = Telerik.Windows.Documents.Fixed.Model.Editing.Flow.HorizontalAlignment.Right;
			block.InsertText(string.Format("{0:P0}", (double)course.Progress / 100));
		}

		table.Draw(editor, new Rect(40, 40, ExampleDocumentSizes.PageWidth - 80, double.PositiveInfinity));
	}
}