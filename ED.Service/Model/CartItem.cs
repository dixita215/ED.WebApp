using System.ComponentModel.DataAnnotations;

namespace ED.Api.Model
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int UserId { get; set; }
    }
}
