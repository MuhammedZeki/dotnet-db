namespace dotnet_db.Models;


public class Card
{
    public int Id { get; set; }
    public string CustomerId { get; set; } = null!;

    public List<CardItem> CardItems { get; set; } = [];

    public void InceareItem(int cartItemId)
    {
        var item = CardItems.FirstOrDefault(i => i.Id == cartItemId);
        if (item != null)
        {
            item.Quantity += 1;
        }
    }

    public void DecreaseItem(int cartItemId)
    {
        var item = CardItems.FirstOrDefault(i => i.Id == cartItemId);

        if (item == null)
            throw new Exception("Cart Item Not Found");

        if (item.Quantity <= 1)
            CardItems.Remove(item);
        else
            item.Quantity -= 1;
    }


    public void AddItemCart(Product product, int quantity)
    {
        var cart = CardItems.Where(i => i.ProductId == product.Id).FirstOrDefault();

        if (cart != null)
        {
            cart.Quantity += quantity;
        }
        else
        {
            CardItems.Add(new CardItem
            {
                ProductId = product.Id,
                Quantity = quantity
            });
        }
    }

    public void RemoveItem(int cartItemId)
    {
        var cart = CardItems.FirstOrDefault(i => i.Id == cartItemId);
        if (cart == null)
            throw new Exception("Cart Item Not Found");
        CardItems.Remove(cart);
    }

    public double SubTotal()
    {
        return CardItems.Sum(i => i.Product.Price * i.Quantity);
    }

    public double TotalPrice()
    {
        return CardItems.Sum(i => i.Product.Price * i.Quantity) * 1.2;
    }
}


public class CardItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int CardId { get; set; }
    public Card Card { get; set; } = null!;
    public int Quantity { get; set; }
}