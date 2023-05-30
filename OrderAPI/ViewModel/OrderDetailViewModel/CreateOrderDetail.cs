using System.ComponentModel.DataAnnotations;

namespace OrderAPI.ViewModel.OrderDetailViewModel
{
    public class CreateOrderDetail
    {
        [Required(ErrorMessage = "Vui lòng nhập Id hóa đơn")]
        public Guid OrderId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public int Quantity { get; set; }
    }
}
