using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModel
{
    [Table("Students")]
    public class StudentEntity
    {
        public string Id { get;set; }
        public string Name { get;set; }
        public string Email {  get;set; }
        public string Password {  get;set; }
    }
}
