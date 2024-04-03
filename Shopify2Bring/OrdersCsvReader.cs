using System.Globalization;
using CsvHelper;

namespace Shopify2Bring;

public class OrdersCsvReader
{
    public List<Order> Read(string filename)
    {
        using var reader = new StreamReader(filename);

        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        using var httpClient = new HttpClient();

        var records = csv.GetRecords<Order>();

        return records.ToList();

    }
}
