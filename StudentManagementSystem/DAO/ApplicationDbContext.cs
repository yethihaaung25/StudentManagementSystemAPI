using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.DataModel;
using System;

namespace StudentManagementSystem.DAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<StudentEntity>Students { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<ClassDetailEntity> ClassDetails { get; set; }
    }
}
