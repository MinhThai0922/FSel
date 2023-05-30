using Microsoft.EntityFrameworkCore;
using OrderAPI.DatabaseContext;
using OrderAPI.Models;
using OrderAPI.Repositories.IRepon;
using OrderAPI.ViewModel.OrderViewModel;

namespace OrderAPI.Repositories.Repon
{
    public class OrderRepon : IOrderRepon
    {
        private readonly OrderDbContext _context;

        public OrderRepon(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
        public async Task<Order> GetById(Guid Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            return order;
        }

        public async Task<Order> GetByIdOrder(Guid Id)
        {
            var order = await _context.Orders.FindAsync(Id);
            if (order == null) return null;
            var listorderdetail = await _context.OrderDetails.Where(p => p.OrderId == Id).ToListAsync();

            ViewOrder view = new ViewOrder();
            ViewModelOrder orderobj = new ViewModelOrder();

            List<ViewModelOrderDetail> lstOrderDetail = new List<ViewModelOrderDetail>();
            orderobj.Id = order.Id;
            orderobj.CustomerId = order.CustomerId;
            orderobj.OrderDate = order.OrderDate;
            orderobj.TotalPrice = order.TotalPrice;
            view.OrderObj = orderobj;
            if (listorderdetail.Count == 0)
            {
                view.OrderObj.ListOrderDetail = null;
            }
            else
            {
                foreach (var item in listorderdetail)
                {
                    ViewModelOrderDetail detail = new ViewModelOrderDetail();
                    detail.Id = item.Id;
                    detail.OrderId = item.OrderId;
                    detail.Quantity = item.Quantity;
                    detail.UnitPrice = item.UnitPrice;
                    detail.ProductName = item.ProductName;
                    lstOrderDetail.Add(detail);
                };
                view.OrderObj.ListOrderDetail = lstOrderDetail;
            }

            return null;
        }
        public async Task<List<Order>> GetByIdKhachHang(Guid Id)
        {
            return await _context.Orders.Where(p => p.CustomerId == Id).ToListAsync();
        }
        public async Task<string> Create(CreateOrder create)
        {
            try
            {
                Order order = new Order()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = create.CustomerId,
                    OrderDate = DateTime.Now,
                    TotalPrice = 0,
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                try
                {
                    foreach (var item in create.orderDetail)
                    {
                        OrderDetail orderDetail = new OrderDetail()
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                        };
                        await _context.OrderDetails.AddAsync(orderDetail);
                        await _context.SaveChangesAsync();
                    }
                    await UpdateTotalPrice(order.Id);
                    _context.Orders.AddRange(order);
                    await _context.SaveChangesAsync();
                    return order.Id.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public async Task UpdateTotalPrice(Guid OrderId)
        {
            var listOrderDeTail = await _context.OrderDetails.Where(p => p.OrderId == OrderId).ToListAsync();
            decimal total = 0;
            foreach (var item in listOrderDeTail)
            {
                total += item.UnitPrice * item.Quantity;
            }
            var order = await GetById(OrderId);
            order.TotalPrice = total;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}

