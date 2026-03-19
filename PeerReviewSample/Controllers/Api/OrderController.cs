using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using PeerReviewSample.Application;
using PeerReviewSample.Models;

namespace PeerReviewSample.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderProcessor _orderProcessor;

        // Violation HIGH 3.3: No null validation on injected logger
        // Violation HIGH 6: OrderProcessor is manually instantiated instead of injected
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
            _orderProcessor = new OrderProcessor(logger); // Direct instantiation — violates DI rule
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            // Violation HIGH 3.1: No null check on request before accessing members
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest("Order must contain at least one item.");

            // Violation HIGH 5.1: Discount calculation is business logic — belongs in service layer
            decimal discount = 0;
            if (request.Items.Count > 10)
            {
                discount = 0.20m;
            }
            else if (request.Items.Count > 5)
            {
                discount = 0.10m;
            }

            var order = _orderProcessor.PlaceOrder(request.CustomerId, request.Items);

            // Violation HIGH 5.1: Applying business calculation directly in controller
            order.TotalAmount = order.TotalAmount * (1 - discount);

            // Violation MEDIUM 4.2: String concatenation instead of structured logging
            _logger.LogInformation("Order " + order.Id + " placed with discount " + discount);

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrder(string orderId)
        {
            // Violation HIGH 3.1: No null or empty validation on orderId path parameter
            var order = _orderProcessor.GetOrder(orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
