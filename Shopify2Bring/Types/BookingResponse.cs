public class BookingResponse
{
    public List<Consignment> Consignments { get; set; }
    
    public class Links
    {
        public string Labels { get; set; }
        public string Waybill { get; set; }
        public string Tracking { get; set; }
    }

    public class DateAndTimes
    {
        public long EarliestPickup { get; set; }
        public long ExpectedDelivery { get; set; }
    }

    public class Package
    {
        public string PackageNumber { get; set; }
    }

    public class Confirmation
    {
        public string ConsignmentNumber { get; set; }
        public object ProductSpecificData { get; set; }
        public Links Links { get; set; }
        public DateAndTimes DateAndTimes { get; set; }
        public List<Package> Packages { get; set; }
    }

    public class Consignment
    {
        public string CorrelationId { get; set; }
        public Confirmation Confirmation { get; set; }
        public List<string> Errors { get; set; }
    }


    
}
