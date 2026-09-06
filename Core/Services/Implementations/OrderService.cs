using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderModule;

namespace Services.Implementations
{
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string userEmail)
        {
            // Address [Shipping Address]
            var shippingAddress = _mapper.Map<Address>(orderRequest.ShipToAddress);
            // OrderItems => Basket [BasketId] => BasketItems => OrderItems
            var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId) 
                ?? throw new BasketNotFoundException(orderRequest.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id) ??
                    throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItems(item, product));
            }
            var orderRepo = _unitOfWork.GetRepository<Order, Guid>();
            // Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            var orderExist = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentSpecifications(basket.PaymentIntentId));
            if(orderExist != null)
            {
                orderRepo.Delete(orderExist);
            }
            // SubTotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            // Create Order
            var order = new Order(userEmail, shippingAddress, orderItems, deliveryMethod, subTotal, basket.PaymentIntentId);
            // Save DB
            await orderRepo.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            // Map, Return
            return _mapper.Map<OrderResult>(order);
        }

        private OrderItem CreateOrderItems(BasketItem item, Product product)
        {
            var productInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            return new OrderItem(productInOrderItem, product.Price, item.Quantity);
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map <IEnumerable<DeliveryMethodResult>> (deliveryMethods);
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludesSpecifications(id))
                ?? throw new OrderNotFoundException(id);
            return _mapper.Map<OrderResult>(order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            var orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludesSpecifications(userEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(orders);
        }
    }
}
