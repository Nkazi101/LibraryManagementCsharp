using System;
namespace LibrarySystem.Models
{
	public class Payment
	{
		public Payment()
		{
		}

		public string CardNumber { get; set; }
		public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvc { get; set; }
        public decimal Amount { get; set; }
        

    }
}

