using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibrarySystem.Models
{
	[Table("Cart")]
	public class Cart
	{
		public Cart()
		{
			booksinCart = new List<BookCart>();
		}

		[Key]
		public int CartID { get; set; }

		public List<BookCart> booksinCart { get; set; } 
}
}

