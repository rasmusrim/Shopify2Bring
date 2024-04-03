using System;
using System.Collections.Generic;

public class BookShippingBody
{
    public List<Consignment> Consignments { get; set; }
    public int SchemaVersion { get; set; }
    public bool TestIndicator { get; set; }
}

public class Consignment
{
    public string CorrelationId { get; set; }
    public List<Package> Packages { get; set; }
    public Parties Parties { get; set; }
    public Product Product { get; set; }
    public string ShippingDateTime { get; set; }
}

public class Package
{
    public string CorrelationId { get; set; }
    public Dimensions Dimensions { get; set; }
    public double WeightInKg { get; set; }
}

public class Dimensions
{
    public int HeightInCm { get; set; }
    public int LengthInCm { get; set; }
    public int WidthInCm { get; set; }
}

public class Parties
{
    public Party Consignee { get; set; }
    public Party Consignor { get; set; }
    public Party Recipient { get; set; }
    public Party Sender { get; set; }
}

public class Party
{
    public string AdditionalAddressInfo { get; set; }
    public string AddressLine { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public Contact Contact { get; set; }
    public string CountryCode { get; set; }
    public string Name { get; set; }
    public string PostalCode { get; set; }
    public string Reference { get; set; }
    public string VatNumber { get; set; }
}

public class Contact
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
}

public class Product
{
    public string CustomerNumber { get; set; }
    public string Id { get; set; }
    public string IncotermRule { get; set; }
    public List<string> AdditionalServices { get; set; }
}
