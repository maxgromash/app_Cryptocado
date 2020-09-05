using System.Runtime.Serialization;

namespace App5.Models
{
    [DataContract]
    public class Operation
    {
        public Operation(string type, string currency, double amount, double price)
        {
            Type = type;
            Currency = currency;
            Amount = amount;
            Price = price;
        }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public double Price { get; set; }
    }
}