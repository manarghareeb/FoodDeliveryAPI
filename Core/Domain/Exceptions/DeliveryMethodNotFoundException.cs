namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id) : Exception($"The delivery method with id {id} not found")
    {
    }
}
