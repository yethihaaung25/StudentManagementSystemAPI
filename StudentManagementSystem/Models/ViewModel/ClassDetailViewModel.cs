using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.ViewModel
{
    [Table("ClassDetails")]
    public class ClassDetailViewModel
    {
        public string StudentId {  get; set; }
        public string ClassName {  get; set; }
        public string[] SubjectName { get; set;}
    }
}
