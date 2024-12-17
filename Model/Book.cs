namespace RestLibrary.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Bookstand { get; set; }
        public int Shelf { get; set; }
        public List<Author> Authors { get; set; }
    }
}
