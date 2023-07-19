using Microsoft.Maui.Controls;
using QSF.Services;
using QSF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Telerik.Documents.SpreadsheetStreaming;

namespace QSF.Examples.SpreadStreamProcessingControl.FirstLookExample
{
    public class FirstLookViewModel : ExampleViewModel
    {
        private const string DocumentTitle = "MY COURSES";
        private const string TitleColumnHeader = "TITLE";
        private const string UniversityColumnHeader = "UNIVERSITY";
        private const string ProgressColumnHeader = "PROGRESS";
        private const int CoursesCount = 30;

        private static readonly string[] CourseNames = new string[] { "Data Science", "Machine Learning", "Big Data", "Product Management", "Business Foundations", "Python for Everybody", "Finance", "Manufacturing Engineering",
                                                                      "Architecture", "Art and Design", "Biological Sciences", "Chemical Engineering", "Chemistry", "Marketing", "Robotics"};
        private static readonly string[] Universities = new string[] { "John Hopkins University", "University of Washington", "University of California", "University of Pennsylvania", "University of Michigan", "Harvard University", "Stanford University" };

        private ObservableCollection<CourseViewModel> courses;
        private ICommand generateSpreadsheetCommand;

        public FirstLookViewModel()
        {
            this.GenerateSpreadsheetCommand = new Command(this.GenerateSpreadsheet);
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

        public ICommand GenerateSpreadsheetCommand
        {
            get
            {
                return this.generateSpreadsheetCommand;
            }
            private set
            {
                if (this.generateSpreadsheetCommand != value)
                {
                    this.generateSpreadsheetCommand = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private IEnumerable<CourseViewModel> GenerateCourses()
        {
            Random rnd = new Random();

            for (int i = 0; i < CoursesCount; i++)
            {
                int courseIndex = rnd.Next(0, CourseNames.Length);
                int universityIndex = rnd.Next(0, Universities.Length);
                int progress = rnd.Next(0, 101);

                yield return new CourseViewModel(CourseNames[courseIndex], Universities[universityIndex], progress);
            }
        }

        private async void GenerateSpreadsheet(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (IWorkbookExporter workbookExporter = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Xlsx, stream))
                {
                    using (IWorksheetExporter worksheetExporter = workbookExporter.CreateWorksheetExporter("Courses"))
                    {
                        ExportColumnWidths(worksheetExporter);
                        ExportDocumentTitleRow(worksheetExporter);
                        ExportDocumentHeaderRow(worksheetExporter);
                        ExportData(worksheetExporter);

                        worksheetExporter.MergeCells(0, 0, 0, 2);
                    }
                }

                await DependencyService.Get<IFileViewerService>().View(stream, "my_courses.xlsx");
            }
        }

        private void ExportColumnWidths(IWorksheetExporter worksheetExporter)
        {
            int firstColumnCharactersCount = 0;
            int secondColumnCharactersCount = 0;
            int thirdColumnCharactersCount = 15;

            foreach (CourseViewModel course in this.Courses)
            {
                firstColumnCharactersCount = Math.Max(firstColumnCharactersCount, course.CourseName.Length);
                secondColumnCharactersCount = Math.Max(secondColumnCharactersCount, course.University.Length);
            }

            using (IColumnExporter columnExporter = worksheetExporter.CreateColumnExporter())
            {
                columnExporter.SetWidthInCharacters(firstColumnCharactersCount);
            }

            using (IColumnExporter columnExporter = worksheetExporter.CreateColumnExporter())
            {
                columnExporter.SetWidthInCharacters(secondColumnCharactersCount);
            }

            using (IColumnExporter columnExporter = worksheetExporter.CreateColumnExporter())
            {
                columnExporter.SetWidthInCharacters(thirdColumnCharactersCount);
            }
        }

        private static void ExportDocumentTitleRow(IWorksheetExporter worksheetExporter)
        {
            using (IRowExporter documentTitleRowExporter = worksheetExporter.CreateRowExporter())
            {
                using (ICellExporter cellExporter = documentTitleRowExporter.CreateCellExporter())
                {
                    cellExporter.SetValue(DocumentTitle);
                    cellExporter.SetFormat(new SpreadCellFormat
                    {
                        Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(10, 144, 208)),
                        ForeColor = new SpreadThemableColor(new SpreadColor(255, 255, 255)),
                        FontSize = 16,
                        FontFamily = new SpreadThemableFontFamily("Arial"),
                        HorizontalAlignment = SpreadHorizontalAlignment.Center
                    });
                }
            }
        }

        private static void ExportDocumentHeaderRow(IWorksheetExporter worksheetExporter)
        {
            using (IRowExporter headerRowExporter = worksheetExporter.CreateRowExporter())
            {
                SpreadCellFormat format = new SpreadCellFormat
                {
                    Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(220, 220, 220)),
                    FontSize = 14,
                    FontFamily = new SpreadThemableFontFamily("Arial"),
                };

                SpreadCellFormat lastCellFormat = new SpreadCellFormat
                {
                    Fill = format.Fill,
                    FontSize = format.FontSize,
                    FontFamily = format.FontFamily,
                    HorizontalAlignment = SpreadHorizontalAlignment.Right
                };

                using (ICellExporter cellExporter = headerRowExporter.CreateCellExporter())
                {
                    cellExporter.SetValue(TitleColumnHeader);
                    cellExporter.SetFormat(format);
                }

                using (ICellExporter cellExporter = headerRowExporter.CreateCellExporter())
                {
                    cellExporter.SetValue(UniversityColumnHeader);
                    cellExporter.SetFormat(format);
                }

                using (ICellExporter cellExporter = headerRowExporter.CreateCellExporter())
                {
                    cellExporter.SetValue(ProgressColumnHeader);
                    cellExporter.SetFormat(lastCellFormat);
                }
            }
        }

        private void ExportData(IWorksheetExporter worksheetExporter)
        {
            foreach (CourseViewModel course in this.Courses)
            {
                using (IRowExporter rowExporter = worksheetExporter.CreateRowExporter())
                {
                    using (ICellExporter cellExporter = rowExporter.CreateCellExporter())
                    {
                        cellExporter.SetValue(course.CourseName);
                    }

                    using (ICellExporter cellExporter = rowExporter.CreateCellExporter())
                    {
                        cellExporter.SetValue(course.University);
                    }

                    using (ICellExporter cellExporter = rowExporter.CreateCellExporter())
                    {
                        cellExporter.SetValue((double)course.Progress / 100);
                        cellExporter.SetFormat(new SpreadCellFormat
                        {
                            NumberFormat = "0 %",
                            HorizontalAlignment = SpreadHorizontalAlignment.Right
                        });
                    }
                }
            }
        }
    }
}
