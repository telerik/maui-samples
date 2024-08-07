using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using QSF.ExampleUtilities;
using QSF.ViewModels;

namespace QSF.Examples.DataGridControl.Common
{
    public class SalesPerson : ViewModelBase
    {
#pragma warning disable CS0169, CS0649
        private static readonly IDictionary<string, string> CountriesAndFlags = new Dictionary<string, string>
        {
            {"Australia", "flag_1.png" },
            {"United Kingdom", "flag_2.png" },
            {"Canada", "flag_3.png" },
            {"United States", "flag_4.png" },
            {"France", "flag_5.png" },
            {"Germany", "flag_6.png" },
        };

        private string firstName;
        private string lastName;
        private string fullName;
        private string jobTitle;
        private int sales;
        private string city;
        private string countryName;
        private string regionName;
        private string image;
        private string phoneNumber;
        private string countryCode;
        private string phoneNumberWithCountryCode;
        private int id;
        private bool isMember;
#pragma warning restore CS0169, CS0649

        public SalesPerson()
        {
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

        public string JobTitle
        {
            get => this.jobTitle;
            set => this.UpdateValue(ref this.jobTitle, value);
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

        public string CountryFlag => CountriesAndFlags[this.CountryName];

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

