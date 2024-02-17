using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public class StudentServices : IStudentServices
    {
       private readonly ApplicationDbContext _dbcontext;
        public StudentServices(ApplicationDbContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }

        public void Create(StudentEntity student)
        {
            _dbcontext.Students.Add(student);
            _dbcontext.SaveChanges();
        }

        public void Delete(string id)
        {
            var student = _dbcontext.Students.Find(id);
            if(student != null) 
            {
                _dbcontext.Students.Remove(student); 
                _dbcontext.SaveChanges();
            }
        }

        public StudentEntity GetById(string id)
        {
            return _dbcontext.Students.Find(id);
        }

        public StudentEntity GetByEmail(string email)
        {
            return _dbcontext.Students.Where(e => e.Email == email).FirstOrDefault();
        }
    }
}
