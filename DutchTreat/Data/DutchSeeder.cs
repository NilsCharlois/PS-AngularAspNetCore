using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DutchTreat.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _hosting;

        public DutchSeeder(DutchContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                if(!File.Exists(filePath)){
                    throw new System.Exception("Art.Json not found");
                }
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);
                var order = _context.Orders.Where(o=>o.Id == 1).FirstOrDefault();
                if(order != null){
                    order.Items = new List<OrderItem>(){
                        new OrderItem(){
                            Product = products.First(),
                            Quantity= 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }
                _context.SaveChanges();
            }
        }
    }
}