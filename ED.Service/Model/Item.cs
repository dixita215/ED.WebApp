using System.ComponentModel.DataAnnotations;

namespace ED.Api.Model
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
