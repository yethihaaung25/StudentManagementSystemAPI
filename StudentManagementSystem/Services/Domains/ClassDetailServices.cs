using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public class ClassDetailServices : IClassDetailService
    {
        private readonly ApplicationDbContext  _dbcontext;
        public ClassDetailServices(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public void Create(ClassDetailEntity classDetail)
        {
            _dbcontext.ClassDetails.Add(classDetail);
            _dbcontext.SaveChanges();
        }

        public void Delete(string id)
        {
            var classDetail = _dbcontext.ClassDetails.Find(id);
            if(classDetail != null)
            {
                _dbcontext.ClassDetails.Remove(classDetail);
                _dbcontext.SaveChanges();
            }
        }

        public void DeleteByStudentId(string studentId)
        {
            var classDetail = _dbcontext.ClassDetails.Where(s => s.Student_Id == studentId).FirstOrDefault();
            if (classDetail != null)
            {
                _dbcontext.ClassDetails.Remove(classDetail);
                _dbcontext.SaveChanges();
            }
        }

        public ClassDetailEntity GetByStudentId(string studentId)
        {
            return _dbcontext.ClassDetails.Find(studentId);
        }

        public ClassDetailEntity GetByClassDetail(string subjectId, string classId, string studentId) 
        {
            var detail = _dbcontext.ClassDetails.Where(s => s.Subject_Id == subjectId && s.Class_Id == classId && s.Student_Id == studentId);
            return detail.FirstOrDefault();
        }

        public IList<ClassDetailEntity> RetrieveAll(string studentId)
        {
            return _dbcontext.ClassDetails.Where(s => s.Student_Id == studentId).ToList();
        }

        public void Update(ClassDetailEntity classDetail)
        {
            _dbcontext.ClassDetails.Update(classDetail);
            _dbcontext.SaveChanges();
        }

        public IList<ClassDetailEntity> RetrieveAllByClassId()
        {
            return _dbcontext.ClassDetails.GroupBy(c => c.Class_Id).Select(g => g.First()).ToList() ;
        }
    }
}
