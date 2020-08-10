using DutchTreat.Data;
using DutchTreat.Data.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IDutchRepository repo, ILogger<ProductsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(_repo.GetAllProducts());
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }
    }
}