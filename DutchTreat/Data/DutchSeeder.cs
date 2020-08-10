using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DutchTreat.Data.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _context;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchContext context,
            IWebHostEnvironment hosting,
            UserManager<StoreUser> userManager)
        {
            _context = context;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("test@test.com");

            if(user == null)
            {
                user = new StoreUser(){
                    FirstName = "Nils",
                    LastName = "Charlois",
                    Email = "test@test.com",
                    UserName = "test@test.com"
                };

                var result = await _userManager.CreateAsync(user, "Aileen08032016!");
                if(result != IdentityResult.Success)
                {
                    throw new System.InvalidOperationException("Could not create new user in seeder");
                }
            }

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
                    order.User = user;
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