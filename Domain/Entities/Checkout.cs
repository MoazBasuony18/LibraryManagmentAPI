using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagmentAPI.Domain.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }

        [ForeignKey("BookId")]
        public int BookId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
