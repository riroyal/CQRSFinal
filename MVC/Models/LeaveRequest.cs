using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string EmployeeEmail { get; set; }
    }
}
