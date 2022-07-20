using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using QSF.ViewModels;

namespace QSF.Examples.DataGridControl.Common
{
    public class SalesPerson : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private string fullName;
        private int sales;
        private string city;
        private string countryName;
        private string regionName;
        private List<string> regions;
        private List<string> images;
        private List<string> flags;
        private Dictionary<string, string> countriesAndFlags;
        private string image;
        private string phoneNumber;
        private string countryCode;
        private string phoneNumberWithCountryCode;
        private int id;
        private bool isMember;

        private static Random random = new Random();

        public SalesPerson()
        {
            this.regions = new List<string>
            {
                "North",
                "South",
                "Central",
                "East",
                "West"
            };

            this.images = new List<string>
            {
                "person_1.png",
                "person_2.png",
                "person_3.png",
                "person_4.png",
                "person_5.png",
                "person_6.png",
                "person_7.png",
                "person_8.png",
            };

            this.flags = new List<string>
            {
                "flag_1.png",
                "flag_2.png",
                "flag_3.png",
                "flag_4.png",
                "flag_5.png",
                "flag_6.png",
            };

            this.countriesAndFlags = new Dictionary<string, string>
            {
                {"Australia", this.flags[0]},
                {"United Kingdom", this.flags[1]},
                {"Canada", this.flags[2]},
                {"United States", this.flags[3]},
                {"France", this.flags[4]},
                {"Germany", this.flags[5]},
            };

            this.RegionName = this.regions[random.Next(0, this.regions.Count)];
            this.Sales = random.Next(2, 100) * random.Next(100, 999);
            this.Image = this.images[random.Next(0, this.images.Count)];
        }

        public string FirstName
        {
            get => this.firstName;
            set
            {
                if (UpdateValue(ref this.firstName, value))
                {
                    this.UpdateFullName();
                }
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                if (UpdateValue(ref this.lastName, value))
                {
                    this.UpdateFullName();
                }
            }
        }

        public string FullName
        {
            get => this.fullName;
            set => UpdateValue(ref this.fullName, value);
        }

        [XmlElement(ElementName = "BusinessEntityID")]
        public int Id
        {
            get => this.id;
            set
            {
                if (UpdateValue(ref this.id, value))
                {
                    this.UpdateIsMember();
                }
            }
        }

        public string City
        {
            get => this.city;
            set => UpdateValue(ref this.city, value);
        }

        public string CountryName
        {
            get => this.countryName;
            set => UpdateValue(ref this.countryName, value);
        }

        public string CountryFlag
        {
            get => this.countriesAndFlags[this.CountryName];
        }

        public string CountryCode
        {
            get => this.countryCode;
            set
            {
                if (UpdateValue(ref this.countryCode, value))
                {
                    this.UpdatePhoneWithCountryCode();
                }
            }
        }

        public string Image
        {
            get => this.image;
            set => UpdateValue(ref this.image, value);
        }

        public string PhoneNumber
        {
            get => this.phoneNumber;
            set
            {
                if (UpdateValue(ref this.phoneNumber, value))
                {
                    this.UpdatePhoneWithCountryCode();
                }
            }
        }

        public string PhoneNumberWithCountryCode
        {
            get => this.phoneNumberWithCountryCode;
            set => UpdateValue(ref this.phoneNumberWithCountryCode, value);
        }

        public bool IsMember
        {
            get => this.isMember;
            set => UpdateValue(ref this.isMember, value);
        }

        public int Sales
        {
            get => this.sales;
            set => UpdateValue(ref this.sales, value);
        }

        public string RegionName
        {
            get => this.regionName;
            set => UpdateValue(ref this.regionName, value);
        }

        private void UpdatePhoneWithCountryCode()
        {
            this.PhoneNumberWithCountryCode = $"{this.CountryCode}-{this.PhoneNumber}";
        }

        private void UpdateFullName()
        {
            this.FullName = $"{this.FirstName} {this.LastName}";
        }

        private void UpdateIsMember()
        {
            this.IsMember = this.Id % 2 == 0;
        }
    }
}

