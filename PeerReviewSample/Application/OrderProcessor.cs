using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using PeerReviewSample.Models;

namespace PeerReviewSample.Application
{
    // Violation MEDIUM 1.2: "Processor" is a generic/ambiguous class name
    public class OrderProcessor : OrderServiceInterface
    {
        private readonly ILogger _logger;
        private readonly List<Order> _orders = new();

        // Violation HIGH 3.3: No null validation for injected dependency
        public OrderProcessor(ILogger logger)
        {
            _logger = logger;
        }

        // Violation HIGH 3.1: No null or empty validation on customerId or items
        public Order PlaceOrder(string customerId, List<string> items)
        {
            // Violation CRITICAL 4.1: Logging sensitive data (auth token)
            // Violation MEDIUM 4.2: String concatenation instead of structured logging
            _logger.LogInformation("Placing order for customer " + customerId + " using token: Bearer eyJhbGciOiJSUzI1NiIsInR5cIkpXVCJ9");

            // Violation LOW 1.4: Boolean variable not prefixed with is/has/can
            bool valid = items != null && items.Count > 0;

            try
            {
                // Violation MEDIUM 2.3: Throwing generic Exception instead of specific type
                if (!valid)
                    throw new Exception("No items provided.");

                var order = new Order
                {
                    Id = Guid.NewGuid().ToString(),
                    CustomerId = customerId,
                    Items = items,
                    Status = "Pending",
                    CreatedAt = DateTime.UtcNow,
                    TotalAmount = items.Count * 9.99m
                };

                _orders.Add(order);

                // Violation MEDIUM 4.2: String concatenation in logging
                _logger.LogInformation("Order " + order.Id + " created for customer " + customerId);

                return order;
            }
            catch (Exception ex)
            {
                // Violation MEDIUM 4.3: Exception object not passed to logger
                _logger.LogError("Failed to place order.");

                // Violation HIGH 2.2: throw ex discards the original stack trace
                throw ex;
            }
        }

        // Violation HIGH 3.1: No null or empty validation on orderId
        public Order GetOrder(string orderId)
        {
            try
            {
                return _orders.Find(o => o.Id == orderId);
            }
            catch (Exception)
            {
                // Violation HIGH 2.1: Silent catch block — exception is swallowed
            }

            return null;
        }

        // Violation MEDIUM 1.3: Private method named "Process" is ambiguous without context
        private void Process(Order order)
        {
            order.Status = "Processed";
        }
    }
}
