namespace eventz.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }

        public Category(Guid id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        public Category()
        {
        }
    }
}
