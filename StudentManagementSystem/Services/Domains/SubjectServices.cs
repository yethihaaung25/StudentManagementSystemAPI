using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public class SubjectServices : ISubjectService
    {
        private readonly ApplicationDbContext _dbcontext;
        public SubjectServices(ApplicationDbContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public void Create(SubjectEntity subject)
        {
            _dbcontext.Subjects.Add(subject);
            _dbcontext.SaveChanges();
        }

        public SubjectEntity GetById(string id)
        {
            return _dbcontext.Subjects.Where(s => s.ID == id).FirstOrDefault();
        }

        public SubjectEntity GetBySubject(string subjectname)
        {
            return _dbcontext.Subjects.Where(s => s.Name == subjectname).FirstOrDefault();
        }

        public void Update(SubjectEntity subject, string id)
        {
            _dbcontext.ChangeTracker.Clear();
            _dbcontext.Subjects.Update(subject);
            _dbcontext.SaveChanges();
        }
    }
}
