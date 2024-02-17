using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModel
{
    public class StudentGetIdViewModel
    {
        [Required]
        public string StudentId {  get; set; }
    }
}
