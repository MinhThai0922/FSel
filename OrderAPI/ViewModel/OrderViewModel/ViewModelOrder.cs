namespace OrderAPI.ViewModel.OrderViewModel
{
    public class ViewModelOrder
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<ViewModelOrderDetail>? ListOrderDetail { get; set; }
    }
}
