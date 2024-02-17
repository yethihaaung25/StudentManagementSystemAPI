using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public interface IClassDetailService
    {
        void Create(ClassDetailEntity classDetail);
        IList<ClassDetailEntity> RetrieveAll(string studentId);
        void Delete(string id);
        void DeleteByStudentId(string studentId);
        ClassDetailEntity GetByStudentId(string id);
        ClassDetailEntity GetByClassDetail(string subjectId, string classId, string studentId);
        IList<ClassDetailEntity> RetrieveAllByClassId();
        void Update(ClassDetailEntity classDetail);
    }
}
