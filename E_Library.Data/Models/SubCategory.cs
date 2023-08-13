namespace E_Library.Data.Models
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}