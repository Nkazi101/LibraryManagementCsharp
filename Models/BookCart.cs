using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LibrarySystem.Models
{
	[Table("BookCart")]
	public class BookCart
	{
		public BookCart()
		{
		}
		[Key]
		public int BookCartID { get; set; }


		[ForeignKey("CartID")]
		public int CartID { get; set; }
        [ForeignKey("BookID")]
        public int BookID { get; set; }


		public Cart Cart { get; set; }
		public Book Book { get; set; }
	}
}

