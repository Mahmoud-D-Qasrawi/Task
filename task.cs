using Serilog;
using System;
using static Stock;

public class Stock
{
    public string Symbol { get; }
    private decimal _price;
    public decimal Price
    {
        get { return _price; }
        set
        {
            if (_price != value)
            {
                _price = value;
                PriceChanged?.Invoke($"{Symbol} Price changed to {_price}");
            }
        }
    }
    public Stock(string symbol, decimal price)
    {
        Symbol = symbol;
        Price = price;
    }
    public delegate void PriceChangedHandler(string msg);
    public event PriceChangedHandler? PriceChanged;
}
class Program
{
    static void Email(string msg)
    {
        Console.WriteLine("Email Alert:" + msg);

    }
    static void SMS(string msg)
    {
        Console.WriteLine("SMS Alert:" + msg);
    }
    static void LOG(string msg)
    {
        Log.Information(msg);
    }
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .CreateLogger();
        var one = new Stock("MSFT", 100);
        PriceChangedHandler EmailNotifier = Email;
        PriceChangedHandler SMSNotifier = SMS;
        PriceChangedHandler Logger = LOG;
        one.PriceChanged += EmailNotifier;
        one.PriceChanged += SMSNotifier;
        one.PriceChanged += Logger;
        one.Price = 60.55m;
    }
}

