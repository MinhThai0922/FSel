namespace OrderAPI.ViewModel.OrderViewModel
{
    public class ViewModelCustomer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
