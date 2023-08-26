namespace LibraryManagmentAPI.Common.DTOs
{
    public class CheckoutDTO:CreateCheckoutDTO
    {
        public int Id { get; set; }
    }
    public class CreateCheckoutDTO
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
    }

}
