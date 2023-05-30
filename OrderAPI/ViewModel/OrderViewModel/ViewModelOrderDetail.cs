namespace OrderAPI.ViewModel.OrderViewModel
{
    public class ViewModelOrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
