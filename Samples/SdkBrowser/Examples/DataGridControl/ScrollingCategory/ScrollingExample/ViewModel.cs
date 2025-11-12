using System;
using System.Collections.ObjectModel;

namespace SDKBrowserMaui.Examples.DataGridControl.ScrollingCategory.ScrollingExample;

// >> datagrid-scrolling-viewmodel
public class ViewModel
{
	public ViewModel()
	{
		this.Shops = new ObservableCollection<Shop>();
		
		string[] shopNames = {
			"Tech Haven", "Fashion Forward", "Garden Center", "Book Nook", "Coffee Corner", 
			"Sports Zone", "Pet Paradise", "Home Depot", "Music Store", "Jewelry Box",
			"Pharmacy Plus", "Auto Parts", "Grocery Mart", "Bakery Bliss", "Flower Shop",
			"Electronics World", "Toy Kingdom", "Fitness First", "Beauty Salon", "Hardware Store",
			"Bike Shop", "Art Gallery", "Gift Shop", "Wine Cellar", "Clothing Co",
			"Shoe Store", "Watch Works", "Camera Corner", "Game Zone", "Antique Shop",
			"Health Foods", "Ice Cream", "Pizza Place", "Burger Bar", "Sushi Spot",
			"Taco Time", "Sandwich Shop", "Donut Delight", "Candy Corner", "Fruit Market",
			"Fish Market", "Meat Shop", "Cheese Shop", "Tea House", "Smoothie Bar",
			"Yogurt Shop", "Pancake House", "Waffle Works", "Bagel Barn", "Pastry Palace"
		};

		string[] addresses = {
			"123 Main St", "456 Oak Ave", "789 Elm St", "321 Park Blvd", "654 First Ave",
			"987 Second St", "147 Third Ave", "258 Cedar St", "369 Maple Ave", "741 Pine St",
			"852 Washington St", "963 Lincoln Ave", "159 Broadway", "357 Market St", "753 Church St",
			"486 Union St", "219 State St", "675 Center Ave", "531 Hill St", "864 Valley Rd",
			"195 River Dr", "428 Lake St", "736 Forest Ave", "582 Garden Ln", "317 Spring St",
			"691 Summer Ave", "245 Winter St", "893 Fall Rd", "164 North St", "758 South Ave",
			"426 East St", "139 West Blvd", "681 High St", "294 Low Ave", "537 Grand St",
			"812 Noble Ave", "476 Royal St", "623 King Blvd", "385 Queen St", "719 Prince Ave",
			"241 Duke St", "568 Earl Ave", "834 Baron St", "197 Count Rd", "453 Lord Ave",
			"786 Manor St", "322 Castle Ave", "679 Palace Blvd", "145 Crown St", "598 Throne Ave"
		};

		string[] cities = {
			"New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
			"Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
			"Austin", "Jacksonville", "Fort Worth", "Columbus", "Charlotte",
			"San Francisco", "Indianapolis", "Seattle", "Denver", "Washington",
			"Boston", "El Paso", "Nashville", "Detroit", "Oklahoma City",
			"New York", "Los Angeles", "Chicago", "Houston", "Phoenix",
			"Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose",
			"Austin", "Jacksonville", "Fort Worth", "Columbus", "Charlotte",
			"San Francisco", "Indianapolis", "Seattle", "Denver", "Washington",
			"Boston", "El Paso", "Nashville", "Detroit", "Oklahoma City"
		};

		string[] states = {
			"NY", "CA", "IL", "TX", "AZ", "PA", "TX", "CA", "TX", "CA",
			"TX", "FL", "TX", "OH", "NC", "CA", "IN", "WA", "CO", "DC",
			"MA", "TX", "TN", "MI", "OK", "NY", "CA", "IL", "TX", "AZ",
			"PA", "TX", "CA", "TX", "CA", "TX", "FL", "TX", "OH", "NC",
			"CA", "IN", "WA", "CO", "DC", "MA", "TX", "TN", "MI", "OK"
		};

		string[] zipCodes = {
			"10001", "90210", "60601", "77001", "85001", "19101", "78201", "92101", "75201", "95101",
			"73301", "32201", "76101", "43201", "28201", "94101", "46201", "98101", "80201", "20001",
			"02101", "79901", "37201", "48201", "73101", "10002", "90211", "60602", "77002", "85002",
			"19102", "78202", "92102", "75202", "95102", "73302", "32202", "76102", "43202", "28202",
			"94102", "46202", "98102", "80202", "20002", "02102", "79902", "37202", "48202", "73102"
		};

		string[] phoneNumbers = {
			"(212) 555-0001", "(323) 555-0002", "(312) 555-0003", "(713) 555-0004", "(602) 555-0005",
			"(215) 555-0006", "(210) 555-0007", "(619) 555-0008", "(214) 555-0009", "(408) 555-0010",
			"(512) 555-0011", "(904) 555-0012", "(817) 555-0013", "(614) 555-0014", "(704) 555-0015",
			"(415) 555-0016", "(317) 555-0017", "(206) 555-0018", "(303) 555-0019", "(202) 555-0020",
			"(617) 555-0021", "(915) 555-0022", "(615) 555-0023", "(313) 555-0024", "(405) 555-0025",
			"(212) 555-0026", "(323) 555-0027", "(312) 555-0028", "(713) 555-0029", "(602) 555-0030",
			"(215) 555-0031", "(210) 555-0032", "(619) 555-0033", "(214) 555-0034", "(408) 555-0035",
			"(512) 555-0036", "(904) 555-0037", "(817) 555-0038", "(614) 555-0039", "(704) 555-0040",
			"(415) 555-0041", "(317) 555-0042", "(206) 555-0043", "(303) 555-0044", "(202) 555-0045",
			"(617) 555-0046", "(915) 555-0047", "(615) 555-0048", "(313) 555-0049", "(405) 555-0050"
		};

		string[] categories = {
			"Electronics", "Fashion", "Home & Garden", "Books", "Food & Beverage",
			"Sports", "Pets", "Health", "Automotive", "Entertainment",
			"Electronics", "Fashion", "Home & Garden", "Books", "Food & Beverage",
			"Sports", "Pets", "Health", "Automotive", "Entertainment",
			"Electronics", "Fashion", "Home & Garden", "Books", "Food & Beverage",
			"Sports", "Pets", "Health", "Automotive", "Entertainment",
			"Electronics", "Fashion", "Home & Garden", "Books", "Food & Beverage",
			"Sports", "Pets", "Health", "Automotive", "Entertainment",
			"Electronics", "Fashion", "Home & Garden", "Books", "Food & Beverage",
			"Sports", "Pets", "Health", "Automotive", "Entertainment"
		};

		string[] managers = {
			"John Smith", "Mary Johnson", "Robert Brown", "Patricia Davis", "Michael Wilson",
			"Linda Moore", "William Taylor", "Elizabeth Anderson", "David Thomas", "Barbara Jackson",
			"Richard White", "Susan Harris", "Joseph Martin", "Jessica Thompson", "Thomas Garcia",
			"Sarah Martinez", "Christopher Robinson", "Nancy Clark", "Daniel Rodriguez", "Lisa Lewis",
			"Matthew Lee", "Betty Walker", "Anthony Hall", "Helen Allen", "Mark Young",
			"John Smith", "Mary Johnson", "Robert Brown", "Patricia Davis", "Michael Wilson",
			"Linda Moore", "William Taylor", "Elizabeth Anderson", "David Thomas", "Barbara Jackson",
			"Richard White", "Susan Harris", "Joseph Martin", "Jessica Thompson", "Thomas Garcia",
			"Sarah Martinez", "Christopher Robinson", "Nancy Clark", "Daniel Rodriguez", "Lisa Lewis",
			"Matthew Lee", "Betty Walker", "Anthony Hall", "Helen Allen", "Mark Young"
		};

		double[] revenues = {
			125000.50, 234500.25, 345600.75, 456700.00, 567800.50, 678900.25, 789000.75, 890100.00, 901200.50, 912300.25,
			823400.75, 734500.00, 645600.50, 556700.25, 467800.75, 378900.00, 289000.50, 190100.25, 291200.75, 392300.00,
			493400.50, 594500.25, 695600.75, 796700.00, 897800.50, 998900.25, 899000.75, 800100.00, 701200.50, 602300.25,
			503400.75, 404500.00, 305600.50, 206700.25, 307800.75, 408900.00, 509000.50, 610100.25, 711200.75, 812300.00,
			913400.50, 814500.25, 715600.75, 616700.00, 517800.50, 418900.25, 319000.75, 220100.00, 321200.50, 422300.25
		};

		int[] employeeCounts = {
			15, 22, 18, 35, 12, 28, 41, 19, 33, 26,
			14, 31, 25, 17, 39, 23, 16, 29, 36, 21,
			13, 27, 32, 20, 38, 24, 11, 30, 37, 34,
			10, 40, 45, 42, 8, 44, 7, 43, 6, 46,
			5, 47, 9, 48, 4, 49, 3, 50, 2, 1
		};

		double[] squareFootages = {
			1500.5, 2200.0, 1800.75, 3500.25, 1200.0, 2800.5, 4100.75, 1900.25, 3300.0, 2600.5,
			1400.75, 3100.25, 2500.0, 1700.5, 3900.75, 2300.25, 1600.0, 2900.5, 3600.75, 2100.25,
			1300.0, 2700.5, 3200.75, 2000.25, 3800.0, 2400.5, 1100.75, 3000.25, 3700.0, 3400.5,
			1000.75, 4000.25, 4500.0, 4200.5, 800.75, 4400.25, 700.0, 4300.5, 600.75, 4600.25,
			500.0, 4700.5, 900.75, 4800.25, 400.0, 4900.5, 300.75, 5000.25, 200.0, 100.5
		};

		bool[] activeStatuses = {
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, true, true, true, true, true, true, true,
			true, true, true, false, true, false, true, false, true, false
		};

		var baseDate = new DateTime(2020, 1, 1);
		
		for (int i = 0; i < 50; i++)
		{
			var shop = new Shop
			{
				Id = i + 1,
				Name = shopNames[i],
				Address = addresses[i],
				City = cities[i],
				State = states[i],
				ZipCode = zipCodes[i],
				PhoneNumber = phoneNumbers[i],
				Email = $"{shopNames[i].Replace(" ", "").ToLower()}@email.com",
				Manager = managers[i],
				OpeningDate = baseDate.AddDays(i * 30),
				Revenue = revenues[i],
				EmployeeCount = employeeCounts[i],
				SquareFootage = squareFootages[i],
				Category = categories[i],
				IsActive = activeStatuses[i]
			};
			
			this.Shops.Add(shop);
		}
	}

	public ObservableCollection<Shop> Shops { get; set; }
}
// << datagrid-scrolling-viewmodel
