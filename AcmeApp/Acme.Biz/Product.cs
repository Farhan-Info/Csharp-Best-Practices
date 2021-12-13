using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;

        #region Constructer

        public Product()
        {
            Console.WriteLine("Product instance created");
            //this.productvendor = new vendor();
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }
        public Product(int productId, string productName, string description) :this()
        {
           this.ProductName = productName;
           this.Description = description;
           this.ProductId = productId;
           if (ProductName.StartsWith("Bulk"))
           {
                this.MinimumPrice = 9.99m;
           }

            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        #endregion

        #region Property
        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        public decimal Cost { get; set; }

        /// <summary>
        /// Calculate the Suggested retail price
        /// <paramref name="markupPercent"/>Percent used to mark up cost.</param>
        /// </summary>
        public decimal CalculateSuggestedPrice(decimal markupPercent) => 
            this.Cost + (this.Cost * markupPercent / 100);

        private string productName;

        public string ProductName
        {
            get {
                var formamatedValue = productName?.Trim();
                return formamatedValue; 
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }
            }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get 
            { //lazy Loading - for creating an object for some times when needed

                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor; 
            }
            set { productVendor = value; }
        }

        internal string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;

        public string ProductCode => this.Category + "-" + this.SequenceNumber;

        public string ValidationMessage { get; private set; }

        #endregion


        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("message from Product");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product", this.productName, "sales@abc.com");

            //static method of static class defined "unsing"
            var result = LogAction("saying Hello");

            return "Hello " + ProductName + " (" + ProductId + "): " + Description + " Available on: " + AvailabilityDate?.ToShortDateString() ;
        }

        public override string ToString()
        {
            return this.ProductName + " (" + this.productId +")";
        }
    }
}
