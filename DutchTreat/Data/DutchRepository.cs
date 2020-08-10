using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _context;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Products
                .OrderBy(p=>p.Title)
                .ToList();
            }
            catch (System.Exception ex)
            {
                
                _logger.LogCritical($"Exception when getting all the products:{ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products
            .Where(predicate=>predicate.Category == category)
            .ToList();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                if(includeItems)
                {
                    return _context
                    .Orders
                    .Include(o=>o.Items)
                    .ThenInclude(i=>i.Product)
                    .ToList();
                } else {
                    return _context
                    .Orders
                    .ToList();
                }
                
            }
            catch (System.Exception ex)
            {
                
                _logger.LogCritical($"Exception when getting all the orders:{ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                return _context.Orders.Include(o=>o.Items).ThenInclude(i=>i.Product).Where(o=>o.Id == id).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                
                _logger.LogCritical($"Exception when getting order by id:{ex}");
                return null;
            }
        }

        public void AddEntity(object order)
        {
            _context.Add(order);
        }        

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}