using Domain.Entities.OrderModule;

namespace Services.Specifications
{
    internal class OrderWithIncludesSpecifications : BaseSpecifications<Order, Guid>
    {
        // Get Order By Id ==> [criteria => id == o.Id], [Includes => DeliveryMethod, OrderItems]
        public OrderWithIncludesSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
        }
        // Get All Orders By Email ==> [criteria => email == o.Email], [Includes => DeliveryMethod, OrderItems]
        public OrderWithIncludesSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }
    }
}
