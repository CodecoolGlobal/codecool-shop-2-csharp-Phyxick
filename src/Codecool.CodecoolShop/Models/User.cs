using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZipcode { get; set; }
        public string BillingCity { get; set; }
        public string BillingStreet { get; set; }
        public string BillingHouseNumber { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZipcode { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingHouseNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVVCode { get; set; }
    }
}
