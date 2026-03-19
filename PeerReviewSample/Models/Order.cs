using System;
using System.Collections.Generic;

namespace PeerReviewSample.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<string> Items { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class CreateOrderRequest
    {
        public string CustomerId { get; set; }
        public List<string> Items { get; set; }
    }
}
