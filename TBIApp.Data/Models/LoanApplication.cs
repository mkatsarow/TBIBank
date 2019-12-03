namespace TBIApp.Data.Models
{
    public class LoanApplication
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public LoanApplicationStatus Status { get; set; }
        public string CardId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
     

    }
}
