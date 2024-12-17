namespace RestLibrary.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
