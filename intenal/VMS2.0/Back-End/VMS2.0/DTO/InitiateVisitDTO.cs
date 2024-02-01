namespace VMS2._0.DTO
{
    public class InitiateVisitDTO
    {
        public string UserID { get; set; }
        public string VisitorEmail { get; set; }
        public string? VisitorNumber { get; set; }  
        public string? VisitorName { get; set; }
        public string? message { get; set; }
        public string Purpose { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public DateTime ExpectedDepart { get; set; }
    }


}
