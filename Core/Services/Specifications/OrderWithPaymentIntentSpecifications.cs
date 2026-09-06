using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntent) : base(o => o.PaymentIntentId == paymentIntent)
        {
        }
    }
}
