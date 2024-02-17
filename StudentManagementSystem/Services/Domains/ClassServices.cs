using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.DAO;
using StudentManagementSystem.Models.DataModel;

namespace StudentManagementSystem.Services.Domains
{
    public class ClassServices : IClassService
    {
        private readonly ApplicationDbContext _dbContext;
        public ClassServices(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Create(ClassEntity studentclass)
        {
            _dbContext.Classes.Add(studentclass);
            _dbContext.SaveChanges();
        }

        public ClassEntity GetByName(string name)
        {
            return _dbContext.Classes.Where(c => c.Name == name).FirstOrDefault();
        }

        public ClassEntity GetByClass(string classname)
        {
            return _dbContext.Classes.Where(e => e.Name == classname).FirstOrDefault();
        }

        public ClassEntity GetById(string id)
        {
            return _dbContext.Classes.Find(id);
        }
    }
}
