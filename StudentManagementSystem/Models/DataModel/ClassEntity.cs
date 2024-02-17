using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.DataModel
{
    [Table("Classes")]
    public class ClassEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
