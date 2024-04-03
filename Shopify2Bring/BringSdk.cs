using System.Text;
using System.Text.Json;
using Shopify2Bring.Types;

namespace Shopify2Bring;

public class BringSdk
{
    private readonly BringConfig _bringConfig;

    public BringSdk(BringConfig bringConfig)
    {
        _bringConfig = bringConfig;


    }

    public BookingResponse Book(Order order, bool isTest = false)
    {

        var request = new HttpRequestMessage();
        request.Headers.Add("X-Mybring-API-Uid", _bringConfig.ApiUid);
        request.Headers.Add("X-Mybring-API-Key", _bringConfig.ApiKey);
        request.Headers.Add("X-Bring-Client-Url", _bringConfig.ApiClientUrl);

        request.Headers.Add("Accept", "application/json");
        request.RequestUri = new Uri("https://api.bring.com/booking-api/api/booking");
        request.Method = HttpMethod.Post;

        var body = CreateBody(order, isTest);
        var json = JsonSerializer.Serialize(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        Console.WriteLine(json);
        request.Content = content;
        using var httpClient = new HttpClient();
        var response = httpClient.Send(request);

        var responseString = response.Content.ReadAsStringAsync().Result;

        return JsonSerializer.Deserialize<BookingResponse>(responseString,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

    }

    private BookShippingBody CreateBody(Order record, bool isTest)
    {
        var body = new BookShippingBody();
        body.SchemaVersion = 1;
        body.TestIndicator = isTest;

        body.Consignments = new List<Consignment>();
        body.Consignments.Add(new Consignment
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Parties = new Parties
            {
                Recipient = new Party
                {
                    AddressLine = (record.ShippingAddress1 + " " + record.ShippingAddress2).Trim(),
                    City = record.ShippingCity,
                    PostalCode = record.ShippingZip,
                    CountryCode = "NO",
                    Name = record.ShippingName,
                    Contact = new Contact
                    {
                        Name = record.ShippingName,
                        Email = record.Email,
                        PhoneNumber = record.ShippingPhone
                    }


                },
                Sender = new Party
                {
                    AddressLine = "Frydenbergveien 8",
                    City = "Stokke",
                    PostalCode = "3160",
                    CountryCode = "NO",
                    Name = "Betlehem forlag AS",


                },

            },
            Packages = new List<Package>
            {
                new Package
                {
                    WeightInKg = 1,
                    Dimensions = new Dimensions
                    {
                        HeightInCm = 1,
                        LengthInCm = 1,
                        WidthInCm = 1

                    }
                }
            },
            Product = new Product
            {
                Id = "3584",
                CustomerNumber = "20014866055"
            },
            ShippingDateTime = DateTime.Now.ToString("o")
        });

        return body;

    }
}
