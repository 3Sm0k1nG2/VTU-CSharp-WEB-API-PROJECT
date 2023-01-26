namespace Project
{
    public class PaymentModel 
    {
        public int Payment_ID { get; set; } // unique payment ID
        public string Payment { get; set; } // type of payment customer will use - Account, C.O.D, Credit, P.O., Warranty
    }
}
