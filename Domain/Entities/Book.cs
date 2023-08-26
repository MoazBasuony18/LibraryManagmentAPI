namespace LibraryManagmentAPI.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public ICollection<Checkout> Checkouts { get; set; }
    }
}
