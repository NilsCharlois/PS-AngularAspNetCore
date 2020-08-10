using System;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Collections.Generic;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository _repo;
        private readonly ILogger<OrdersController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repo, ILogger<OrdersController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var results = _repo.GetAllOrders(includeItems);
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repo.GetOrderById(id);
                if(order == null){
                    return NotFound();
                }
                return Ok(_mapper.Map<Order, OrderViewModel>(order));
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    // build Order object from passed ViewModel object
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    
                    // override OrderDate if not specified
                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate =DateTime.Now;
                    }
                    _repo.AddEntity(newOrder);
                    if(_repo.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
                    }   
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogCritical($"Failed to save new order: {ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}