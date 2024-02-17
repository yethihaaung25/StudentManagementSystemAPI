using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModel
{
    public class SubjectViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
