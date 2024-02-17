using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public interface ISubjectService
    {
        void Create(SubjectEntity subject);
        SubjectEntity GetById(string id);
        SubjectEntity GetBySubject(string subjectname);
        void Update(SubjectEntity subject, string id);
    }
}
