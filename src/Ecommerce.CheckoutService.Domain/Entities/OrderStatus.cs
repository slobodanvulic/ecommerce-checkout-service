using System.Text.Json.Serialization;

namespace Ecommerce.CheckoutService.Domain.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Draft = 0,
    Ready = 1,
    Shipping = 2,
    Completed = 3
}
