namespace Domain.Exceptions
{
    public sealed class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid id) : base($"Order with id {id} not found")
        {
        }
    }
}
