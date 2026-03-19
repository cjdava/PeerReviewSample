using System.Collections.Generic;
using PeerReviewSample.Models;

namespace PeerReviewSample.Application
{
    // Violation LOW 1.1: Interface name does not start with 'I'
    public interface OrderServiceInterface
    {
        Order PlaceOrder(string customerId, List<string> items);
        Order GetOrder(string orderId);
    }
}
