using System;
using System.Collections.Generic;
using System.Linq;

// Клас Товар
class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public Product(string name, double price, string description, string category)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;
    }

    public override string ToString()
    {
        return $"Назва: {Name}, Ціна: {Price}, Опис: {Description}, Категорія: {Category}";
    }
}

// Клас Користувач
class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Order> PurchaseHistory { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        PurchaseHistory = new List<Order>();
    }

    public void AddToPurchaseHistory(Order order)
    {
        PurchaseHistory.Add(order);
    }
}

// Клас Замовлення
class Order
{
    public List<Product> Products { get; set; }
    public List<int> Quantities { get; set; }
    public double TotalPrice { get; set; }
    public string Status { get; set; }

    public Order(List<Product> products, List<int> quantities)
    {
        Products = products;
        Quantities = quantities;
        TotalPrice = CalculateTotalPrice();
        Status = "В обробці"; // Початковий статус - в обробці.
    }

    private double CalculateTotalPrice()
    {
        double totalPrice = 0;
        for (int i = 0; i < Products.Count; i++)
        {
            totalPrice += Products[i].Price * Quantities[i];
        }
        return totalPrice;
    }

    public override string ToString()
    {
        return $"Загальна вартість: {TotalPrice}, Статус: {Status}";
    }
}

// Інтерфейс ISearchable для пошуку товарів
interface ISearchable
{
    List<Product> SearchByPrice(double maxPrice);
    List<Product> SearchByCategory(string category);
    // Додайте інші методи для розширеного пошуку.
}

// Клас Магазин
class Store : ISearchable
{
    public List<Product> Products { get; set; }
    public List<User> Users { get; set; }
    public List<Order> Orders { get; set; }

    public Store()
    {
        Products = new List<Product>();
        Users = new List<User>();
        Orders = new List<Order>();
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public void RegisterUser(User user)
    {
        Users.Add(user);
    }

    public void PlaceOrder(User user, List<Product> products, List<int> quantities)
    {
        Order order = new Order(products, quantities);
        user.AddToPurchaseHistory(order);
        Orders.Add(order);
    }

    public List<Product> SearchByPrice(double maxPrice)
    {
        return Products.Where(product => product.Price <= maxPrice).ToList();
    }

    public List<Product> SearchByCategory(string category)
    {
        return Products.Where(product => product.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Додайте інші методи для розширеного пошуку та управління даними.

    public static void Main()
    {
        Store store = new Store();

        Product product1 = new Product("Товар 1", 10.0, "Опис товару 1", "Категорія 1");
        Product product2 = new Product("Товар 2", 15.0, "Опис товару 2", "Категорія 2");

        store.AddProduct(product1);
        store.AddProduct(product2);

        User user1 = new User("user1", "password1");
        User user2 = new User("user2", "password2");

        store.RegisterUser(user1);
        store.RegisterUser(user2);

        List<Product> productsToOrder = new List<Product> { product1, product2 };
        List<int> quantities = new List<int> { 2, 3 };
        store.PlaceOrder(user1, productsToOrder, quantities);

        Console.WriteLine("Користувач 1 історія покупок:");
        foreach (var order in user1.PurchaseHistory)
        {
            Console.WriteLine(order);
        }
    }
}
