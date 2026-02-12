namespace dotnet_db.Models;

public enum OrderStatus
{
    Pending = 0,
    Shipping = 1,
    Completed = 2,
    Cancelled = 3
}
public class Order
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; set; }
    public string Username { get; set; } = null!;
    public string Fullname { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? OrderNote { get; set; }
    public double TotalPrice { get; set; }
    public List<OrderItem> OrderItems { get; set; } = [];

    public double SubTotal()
    {
        return OrderItems.Sum(i => i.Price * i.Quantity);
    }
    public double Total()
    {
        return OrderItems.Sum(i => i.Price * i.Quantity) * 1.2;
    }

}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public double Price { get; set; }

}