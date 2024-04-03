using CsvHelper.Configuration.Attributes;

public class Order
{
    [Name("Email")]
    public string Email { get; set; }

    [Name("Shipping Name")]
    public string ShippingName { get; set; }

    [Name("Shipping Address1")]
    public string ShippingAddress1 { get; set; }

    [Name("Shipping Address2")]
    public string ShippingAddress2 { get; set; }

    [Name("Shipping Zip")]
    public string ShippingZip { get; set; }
    [Name("Shipping City")]
    public string ShippingCity { get; set; }

    [Name("Shipping Phone")]
    public string ShippingPhone { get; set; }
    
}