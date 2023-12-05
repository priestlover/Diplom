namespace Diplom.Models.Entity
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? GameId { get; set; }

        public int? BasketId { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
