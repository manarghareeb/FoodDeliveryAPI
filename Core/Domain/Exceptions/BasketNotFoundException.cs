namespace Domain.Exceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"basket with id {id} not found")
        {
        }
    }
}
