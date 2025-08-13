using System;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.BottomSheetControl;

// >> bottomsheet-view-model
public class ViewModel
{
	public ViewModel()
	{
		this.People = new ObservableCollection<Person>()
		{
			new Person { Name = "Michel", Age = 23, Department = "Human Resources", Country = "Mexico", CompanyID = 23213, Position = "Junior Recruiter", Joined = new DateTime(2024, 10, 21) },
			new Person { Name = "Jerry", Age = 23, Department = "Finance", Country = "USA", CompanyID = 7667, Position = "Financial Analyst", Joined = new DateTime(2024, 06, 12) },
			new Person { Name = "Ethan", Age = 51, Department = "Marketing", Country = "Australia", CompanyID = 75676, Position = "Manager SEO", Joined = new DateTime(2002, 05, 01) },
			new Person { Name = "Isabella", Age = 25, Department = "Marketing", Country = "Argentina", CompanyID = 9789, Position = "Regular Marketing specialist", Joined = new DateTime(2023, 11, 11) },
			new Person { Name = "Joshua", Age = 45, Department = "Software Development", Country = "USA", CompanyID = 867, Position = "Manager Software Engineering", Joined = new DateTime(2021, 11, 11) },
			new Person { Name = "Logan", Age = 26, Department = "Human Resources", Country = "UK", CompanyID = 3123, Position = "HR Representative", Joined = new DateTime(2024, 10, 12) },
			new Person { Name = "Anthony", Age = 40, Department = "Innovations", Country = "Belgium", CompanyID = 645, Position = "AI Researcher Scientist", Joined = new DateTime(2023, 12, 21) },
			new Person { Name = "Jack", Age = 21, Department = "Customer Service", Country = "USA", CompanyID = 7858, Position = "Developer Support Engineer", Joined = new DateTime(2025, 03, 11) },
			new Person { Name = "Aaron", Age = 32, Department = "Software Development", Country = "USA", CompanyID = 314, Position = "Full Stack Developer", Joined = new DateTime(2023, 02, 12) },
			new Person { Name = "Elena", Age = 37, Department = "Finance", Country = "France", CompanyID = 6757, Position = "Accountant", Joined = new DateTime(2024, 10, 21) },
			new Person { Name = "Ross", Age = 29, Department = "Marketing", Country = "Spain", CompanyID = 546547, Position = "Marketing Manager", Joined = new DateTime(2022, 07, 12) },
			new Person { Name = "Jane", Age = 46, Department = "Innovations", Country = "UK", CompanyID = 3187, Position = "AI Developer", Joined = new DateTime(2000, 06, 21) },
			new Person { Name = "John", Age = 37, Department = "Customer Service", Country = "Australia", CompanyID = 646, Position = "Support Officer", Joined = new DateTime(2024, 03, 11) },
		};
	}

	public ObservableCollection<Person> People { get; set; }
}
// << bottomsheet-view-model