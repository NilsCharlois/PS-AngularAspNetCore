using System;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace DutchTreat.Controllers
{
    [Route("api/orders/{orderid}/items")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersItemsController : ControllerBase
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersItemsController(IDutchRepository repo, ILogger<OrdersController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            try
            {
                var order = _repo.GetOrderById(orderId);
                if(order != null)
                {
                    return Ok(_mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int orderId, int id)
        {
            try
            {
                var order = _repo.GetOrderById(orderId);
                if(order != null)
                {
                    var item = order.Items.Where(i=>i.Id == id).FirstOrDefault();
                    if(item != null)
                    {
                        return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                    }                    
                }
                return NotFound();
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
    }
}