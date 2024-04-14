namespace WebbappLabb1EF.Models
{
    public class LeaveEmployeeVM
    {
        public IEnumerable<EmployeeWithLeave> Employees { get; set; }
        public IEnumerable<Leave> Leaves { get; set; }
    }
}
