using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public interface IClassService
    {
        void Create(ClassEntity studentclass);
        ClassEntity GetByName(string name);
        ClassEntity GetByClass(string classname);
        ClassEntity GetById(string id);
    }
}
