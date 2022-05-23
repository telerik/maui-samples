using QSF.ViewModels;
using System;
using System.Linq;
using System.Collections.Generic;
using Telerik.Barcode;

namespace QSF.Examples.BarcodeControl.SwissQRCodeExample
{
    public class SwissQRCodeViewModel : ExampleViewModel
    {
        private string value;
        private bool isValid;
        private string errorMessage;
        private string ibanText = "CH26 8914 4336 1779 7439 4";
        private string codeCurrencyString = "CHF";
        private string creditorName = "Max Muster & Sohne";
        private string creditorCountry = "CH";
        private string creditorZipCode = "2501";
        private string creditorCity = "Biel";
        private string creditorStreet = "Vallerstrasse";
        private string creditorHouseNumber = "28";
        private string unstructuredMessage = "Invoice - #CN-17";
        private string billingInformation;
        private string debtorName = "Pia Ruth Schnyder";
        private string debtorCountry = "CH";
        private string debtorZipCode = "3372";
        private string debtorCity = "Wanzwil";
        private string debtorStreet = "Tösstalstrasse";
        private string debtorHouseNumber = "143";
        private decimal amount = 100.00M;
        public List<Item> items;

        public SwissQRCodeViewModel()
        {
            this.items = new List<Item>();
            this.items.Add(new Item(1, "Toner cartridge FS-1920", "pcs.", 1, 70.00d));
            this.items.Add(new Item(2, "Toner cartridge FS-5000", "pcs.", 1, 30.00d));

            this.billingInformation = this.items[0].ToString() + ";" + this.items[1].ToString();

            this.GenerateValue();
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public double Total
        {
            get
            {
                return this.items.Select(i => i.Total).Sum();
            }
        }

        public bool IsValid
        {
            get
            {
                return this.isValid;
            }
            set
            {
                if (this.isValid != value)
                {
                    this.isValid = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public string IbanText
        {
            get
            {
                return this.ibanText;
            }
            set
            {
                if (this.ibanText != value)
                {
                    this.ibanText = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CodeCurrency
        {
            get
            {
                return this.codeCurrencyString;
            }
            set
            {
                if (this.codeCurrencyString != value)
                {
                    this.codeCurrencyString = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorName
        {
            get
            {
                return this.creditorName;
            }
            set
            {
                if (this.creditorName != value)
                {
                    this.creditorName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorCountry
        {
            get
            {
                return this.creditorCountry;
            }
            set
            {
                if (this.creditorCountry != value)
                {
                    this.creditorCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorZipCode
        {
            get
            {
                return this.creditorZipCode;
            }
            set
            {
                if (this.creditorZipCode != value)
                {
                    this.creditorZipCode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorCity
        {
            get
            {
                return this.creditorCity;
            }
            set
            {
                if (this.creditorCity != value)
                {
                    this.creditorCity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorStreet
        {
            get
            {
                return this.creditorStreet;
            }
            set
            {
                if (this.creditorStreet != value)
                {
                    this.creditorStreet = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string CreditorHouseNumber
        {
            get
            {
                return this.creditorHouseNumber;
            }
            set
            {
                if (this.creditorHouseNumber != value)
                {
                    this.creditorHouseNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string UnstructuredMessage
        {
            get
            {
                return this.unstructuredMessage;
            }
            set
            {
                if (this.unstructuredMessage != value)
                {
                    this.unstructuredMessage = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string BillingInformation
        {
            get
            {
                return this.billingInformation;
            }
            set
            {
                if (this.billingInformation != value)
                {
                    this.billingInformation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorName
        {
            get
            {
                return this.debtorName;
            }
            set
            {
                if (this.debtorName != value)
                {
                    this.debtorName = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorCountry
        {
            get
            {
                return this.debtorCountry;
            }
            set
            {
                if (this.debtorCountry != value)
                {
                    this.debtorCountry = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorZipCode
        {
            get
            {
                return this.debtorZipCode;
            }
            set
            {
                if (this.debtorZipCode != value)
                {
                    this.debtorZipCode = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorCity
        {
            get
            {
                return this.debtorCity;
            }
            set
            {
                if (this.debtorCity != value)
                {
                    this.debtorCity = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorStreet
        {
            get
            {
                return this.debtorStreet;
            }
            set
            {
                if (this.debtorStreet != value)
                {
                    this.debtorStreet = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string DebtorHouseNumber
        {
            get
            {
                return this.debtorHouseNumber;
            }
            set
            {
                if (this.debtorHouseNumber != value)
                {
                    this.debtorHouseNumber = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public decimal Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (this.amount != value)
                {
                    this.amount = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public List<Item> OrderItems
        {
            get
            {
                return this.items;
            }
        }

        private void GenerateValue()
        {
            AdditionalInformation additionalInfo = new AdditionalInformation(this.unstructuredMessage, this.billingInformation);

            Contact debtor = new Contact(this.debtorName,
                                         new StructuredAddress(this.debtorCountry,
                                               this.debtorZipCode,
                                               this.debtorCity,
                                               this.debtorStreet,
                                               this.debtorHouseNumber));
            var amount = this.amount;

            SwissQRCodeValueStringBuilder qRCodeValue = new SwissQRCodeValueStringBuilder(
             new Iban(this.ibanText, IbanType.IBAN),
             (SwissQRCodeCurrency)Enum.Parse(typeof(SwissQRCodeCurrency), this.codeCurrencyString, true),
             new Contact(this.creditorName,
                         new StructuredAddress(this.creditorCountry,
                                               this.creditorZipCode,
                                               this.creditorCity,
                                               this.creditorStreet,
                                               this.creditorHouseNumber)),
             new Reference(ReferenceType.NON, string.Empty),

             additionalInfo,
             debtor,
             amount,
             null);

            var errors = qRCodeValue.Validate();
            if (!string.IsNullOrEmpty(errors))
            {
                this.isValid = false;
                this.errorMessage = errors;
            }
            else
            {
                this.isValid = true;
                this.errorMessage = string.Empty;
                this.value = qRCodeValue.BuildValue();
            }
        }
    }
}

