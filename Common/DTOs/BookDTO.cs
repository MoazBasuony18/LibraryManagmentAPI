namespace LibraryManagmentAPI.Common.DTOs
{
    public class BookDTO:CreateBookDTO
    {
        public int Id { get; set; }
        //public ICollection<CheckoutDTO> checkoutDTOs { get; set; }

    }
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
    }
    public class UpdateBookDTO:CreateBookDTO
    {
        public int Id { get; set; }

    }
}
