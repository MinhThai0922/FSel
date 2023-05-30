using APICustomer.DatabaseContext;
using APICustomer.Models;
using APICustomer.ViewModel.CustomerViewModel;
using CustomerAPI.Repositories.IRepon;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Repositories.Repon
{
    public class CustomerRepon : ICustomerRepon
    {
        private readonly CustomerDBContext _context;

        public CustomerRepon()
        {
        }

        public CustomerRepon(CustomerDBContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckEmail(string email)
        {
            var result = await _context.Customers.Where(p => p.Email == email).ToListAsync();
            if (result.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckPhoneNumber(string phone)
        {
            var result = await _context.Customers.Where(p => p.PhoneNumber == phone).ToListAsync();
            if (result.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<string> Create(Customer customer)
        {
            try
            {
                _context.Customers.AddRange(customer);
                _context.SaveChanges();
                return "thêm thành công";
            }
            catch (Exception ex)
            {
                return "thêm không thành công";
            }
            //try
            //{
            //    var checkemail = await (CheckEmail(model.Email));
            //    if (checkemail == false) return "Email đã được sử dụng";
            //    var checkphone = await (CheckPhoneNumber(model.PhoneNumber));
            //    if (checkphone == false) return "SDT đã được sử dụng";
            //    Customer customer = new Customer()
            //    {
            //        Id = Guid.NewGuid(),
            //        Address = model.Address,
            //        BirthDay = model.BirthDay,
            //        Email = model.Email,
            //        PhoneNumber = model.PhoneNumber,
            //        FullName = model.FullName
            //    };
            //    await _context.Customers.AddAsync(customer);
            //    await _context.SaveChangesAsync();
            //    return customer.Id.ToString(); ;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
        }

        public async Task<List<Customer>> GetListCustomers()
        {
            var result = await _context.Customers.ToListAsync();
            return result;
        }
        public async Task<bool> CheckUpdateEmail(Guid Id, string email)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.Id == Id);
            var lstCustomer = await _context.Customers.Where(p => p.Email != customer.Email).ToListAsync();
            var result = lstCustomer.Where(p => p.Email == email).ToList();
            if (result.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckUpdatePhoneNumber(Guid Id, string phone)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.Id == Id);
            var lstCustomer = await _context.Customers.Where(p => p.PhoneNumber != customer.PhoneNumber).ToListAsync();
            var result = lstCustomer.Where(p => p.PhoneNumber == phone).ToList();
            if (result.Count() > 0)
            {
                return false;
            }
            return true;
        }
        public async Task<int> Update(Guid Id, UpdateCustomer model)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(Id);
                if (customer == null) return 0;
                var checkemail = await (CheckUpdateEmail(Id, model.Email));
                if (checkemail == false) return 1;
                var checkphone = await (CheckUpdatePhoneNumber(Id, model.PhoneNumber));
                if (checkphone == false) return 2;
                customer.Email = model.Email;
                customer.PhoneNumber = model.PhoneNumber;
                customer.FullName = model.FullName;
                customer.BirthDay = model.BirthDay;
                customer.Address = model.Address;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return 3;
            }
            catch (Exception ex)
            {
                return 4;
            }
        }

        public async Task<int> Delete(Customer customer)
        {
            try
            {
                var customers = await _context.Customers.FindAsync(customer);
                if (customer == null) return 1;
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return 2;
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        public async Task<Customer> GetById(Guid Id)
        {
            var customer = await _context.Customers.FindAsync(Id);
            if (customer == null) return null;
            return customer;
        }

        public async Task<Customer> GetByPhoneNumber(string phonenumber)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(p => p.PhoneNumber == phonenumber);
            if (customer == null) return null;
            return customer;
        }
    }
}
