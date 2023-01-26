namespace Project
{
    public class ServiceModel
    {
        public int Service_ID { get; set; } // unique service ID
        public string Service { get; set; } // type of work to be done - [Assess, Deliver, Install, Repair, Replace]
    }
}