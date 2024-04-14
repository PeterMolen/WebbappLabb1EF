using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebbappLabb1EF.Models
{
    public class LeaveRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } 

        //navigation
        [Required]
        [ForeignKey("Employee")]
        public int FkEmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required]
        [ForeignKey("Leave")]
        public int FkLeaveId { get; set; }
        public Leave? Leave { get; set; }
    }
}
