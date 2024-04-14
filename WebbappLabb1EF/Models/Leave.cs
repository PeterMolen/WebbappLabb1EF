using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebbappLabb1EF.Models
{
    public class Leave
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeaveId { get; set; }

        [Required(ErrorMessage = "Leave must have a name")]
        [DisplayName("Leave")]
        [StringLength(60, ErrorMessage = "The Leave, cant be longer then 60 signs")]
        public string TypeOfLeave { get; set; }

        //navigation
        public IList<LeaveRequest>? LeaveRequest { get; set; }
    }
}
