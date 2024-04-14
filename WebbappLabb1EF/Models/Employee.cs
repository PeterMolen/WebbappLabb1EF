using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebbappLabb1EF.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Must have a name")]
        [StringLength(50, ErrorMessage = "the name cant be longer then 50 signs  ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must have a Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Must have a Phone-number")]
        public string Phone { get; set; }

        //navigation
        public IList<LeaveRequest>? LeaveRequest { get; set; }
    }
}
