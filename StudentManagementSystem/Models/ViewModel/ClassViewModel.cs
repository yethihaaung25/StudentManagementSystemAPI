using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.ViewModel
{
    public class ClassViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
