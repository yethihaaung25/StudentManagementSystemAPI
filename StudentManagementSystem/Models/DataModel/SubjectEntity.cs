using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModel
{
    [Table("Subjects")]
    public class SubjectEntity
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
