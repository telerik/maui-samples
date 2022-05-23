using QSF.ViewModels;

namespace QSF.Examples.SpreadStreamProcessingControl.FirstLookExample
{
    public class CourseViewModel : ViewModelBase
    {
        private int progress;
        private string courseName;
        private string university;

        public CourseViewModel(string courseName, string university, int progress)
        {
            this.CourseName = courseName;
            this.University = university;
            this.Progress = progress;
        }

        public string CourseName
        {
            get
            {
                return this.courseName;
            }
            private set
            {
                if (this.courseName != value)
                {
                    this.courseName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string University
        {
            get
            {
                return this.university;
            }
            private set
            {
                if (this.university != value)
                {
                    this.university = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public int Progress
        {
            get
            {
                return this.progress;
            }
            private set
            {
                if (this.progress != value)
                {
                    this.progress = value;
                    this.OnPropertyChanged();
                }
            }
        }
    }
}
