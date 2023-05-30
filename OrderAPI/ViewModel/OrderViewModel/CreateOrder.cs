using OrderAPI.Configuration;
using OrderAPI.ViewModel.OrderDetailViewModel;
using System.ComponentModel.DataAnnotations;

namespace OrderAPI.ViewModel.OrderViewModel
{
    public class CreateOrder
    {
        [Required(ErrorMessage = "Vui lòng nhập Id khách hàng")]
        public Guid CustomerId { get; set; }
        public List<OrderDetailCreates> orderDetail { get; set; }
    }
}
