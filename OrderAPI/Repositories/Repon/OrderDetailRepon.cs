using Microsoft.EntityFrameworkCore;
using OrderAPI.DatabaseContext;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.ViewModel.OrderDetailViewModel;

namespace OrderAPI.Repositories.Repon
{
    public class OrderDetailRepon : IOrderDetailRepon
    {
        private readonly OrderDbContext _context;
        private readonly IOrderRepon _orderRepon;


        public OrderDetailRepon(OrderDbContext context, IOrderRepon orderRepon)
        {
            _context = context;
            _orderRepon = orderRepon;
        }
        public async Task<int> Create(OrderDetail orderDetail)
        {
            try
            {
                var order = await _context.Orders.FindAsync(orderDetail.OrderId);
                if (order == null) return 0;
                //var checkorderdetail = await _context.OrderDetails.FirstOrDefaultAsync(p => p.OrderId == create.OrderId && p.ProductName == create.ProductName && p.UnitPrice == create.UnitPrice);
                //if (checkorderdetail != null)
                //{
                //    checkorderdetail.Quantity += create.Quantity;
                //    _context.OrderDetails.Update(checkorderdetail);
                //    await _context.SaveChangesAsync();
                //    await _orderRepon.UpdateTotalPrice(create.OrderId);
                //    return 1;
                //}
                await _context.OrderDetails.AddAsync(orderDetail);
                await _context.SaveChangesAsync();
                await _orderRepon.UpdateTotalPrice(orderDetail.OrderId);
                return 2;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        public async Task<int> CheckOrderDetail(OrderDetail orderDetail)
        {
            var checkorderdetail = await _context.OrderDetails.FirstOrDefaultAsync(p => p.OrderId == orderDetail.OrderId && p.ProductName == orderDetail.ProductName && p.UnitPrice == orderDetail.UnitPrice);
            if (checkorderdetail != null)
            {
                checkorderdetail.Quantity += orderDetail.Quantity;
                _context.OrderDetails.Update(checkorderdetail);
                await _context.SaveChangesAsync();
                await _orderRepon.UpdateTotalPrice(orderDetail.OrderId);
                return 1;
            }
            return 0;
        }
        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(Guid Id)
        {
            var listOrderDetail = await _context.OrderDetails.Where(x => x.OrderId == Id).ToListAsync();
            return listOrderDetail;
        }
        public async Task<OrderDetail> GetByIdOrderDetail(Guid Id)
        {
            return await _context.OrderDetails.Where(p => p.OrderId == Id).FirstAsync();
        }

        

        
    }
}
