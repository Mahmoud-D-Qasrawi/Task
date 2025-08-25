using Serilog;
using System;
using static Stock;

public class Stock
{
    public string Symbol { get; set; }
    private double _price;
    public double Price
    {
        get { return this.Price; }  // reading price just returns the field
        set
        {
            if (_price != value)   // only act if it’s actually changing
            {
                _price = value;    // update the field
                PriceChanged?.Invoke($"{Symbol} Price changed to {_price}");
            }
        }


    }
    public Stock(string sympol, double price)
    {
        Symbol = sympol;
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
        one.Price = 60;
    }
}

