namespace WebbappLabb1EF.Models
{
    public class AdminOverviewVM
    {
        public IEnumerable<string> Months { get; set; }
        public IEnumerable<AdminOverview> Overviews { get; set; }
        public IEnumerable<Leave> Leaves { get; set; }
        public IEnumerable<LeaveRequest> LeaveRequests { get; set; }
    }
}
