using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public interface IStudentServices
    {
        void Create(StudentEntity student);
        void Delete(string id);
        StudentEntity GetById(string id);
        StudentEntity GetByEmail(string email);
    }
}
