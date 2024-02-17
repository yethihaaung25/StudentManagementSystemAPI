using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.DataModel;
using StudentManagementSystem.Models.ViewModel;
using StudentManagementSystem.Services.Domains;
using System.Collections;

namespace StudentManagementSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/class")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IClassDetailService _classDetailService;
        private readonly IStudentServices   _studentServices;
        public ClassController(IClassService classService,ISubjectService subjectService, IClassDetailService classDetailService, IStudentServices studentServices) 
        {
            _classService = classService;
            _subjectService = subjectService;
            _classDetailService = classDetailService;
            _studentServices = studentServices;
        }

        // student class added
        [Authorize]
        [HttpPost]
        [Route("studentclass")]
        public JsonResult StudentClass(ClassViewModel classViewModel) 
        {
            var classname = _classService.GetByClass(classViewModel.Name);
            if (classname != null)
            {
                return Json(new { Message = "Your Class Name is already exist. Try another one!!!" });
            }
            ClassEntity classEntity = new ClassEntity()
            {
                Id = Guid.NewGuid().ToString(),
                Name = classViewModel.Name,
            };
            _classService.Create(classEntity);
            return Json(new { Message = "Class added successfully." });
        }

        // added subject for subject table
        [Authorize]
        [HttpPost]
        [Route("studentsubject")]
        public JsonResult StudentSubject(SubjectViewModel subjectViewModel)
        {
            var subjectname = _subjectService.GetBySubject(subjectViewModel.Name);
            if (subjectname != null)
            {
                return Json(new { Message = "Your Subject Name is already exist. Try another one!!!" });
            }
            SubjectEntity subjectEntity = new SubjectEntity()
            {
                ID = Guid.NewGuid().ToString(),
                Name = subjectViewModel.Name,
            };
            _subjectService.Create(subjectEntity);
            return Json(new { Message = "Subject added successfully." });
        }

        // update subject
        [Authorize]
        [HttpPut("{id}")]
        [Route("studentsubjectupdate/{id}")]
        public JsonResult StudentSubjectUpdate(Guid id,SubjectViewModel subjectViewModel)
        {
            var updateSubjectData = _subjectService.GetById(id.ToString());
            if(updateSubjectData != null)
            {
                var subjectname = _subjectService.GetBySubject(subjectViewModel.Name);
                if (subjectname != null)
                {
                    return Json(new { Message = "Your Subject Name is already exist. Try another one!!!" });
                }
                SubjectEntity subjectEntity = new SubjectEntity()
                {
                    ID = updateSubjectData.ID,
                    Name = subjectViewModel.Name,
                };
                _subjectService.Update(subjectEntity,id.ToString());
                return Json(new { Message = "Subject updated successfully." });
            }
            return Json(new { Message = "Subject Not Found." });
        }

        // added class, student, subject
        [Authorize]
        [HttpPost]
        [Route("studentclassdetail")]
        public JsonResult StudentClassDetails(ClassDetailViewModel classDetailViewModel)
        {
            var studentId = _studentServices.GetById(classDetailViewModel.StudentId);
            var classId = _classService.GetByName(classDetailViewModel.ClassName);
            for (int i = 0; i < classDetailViewModel.SubjectName.Length; i++)
            {
                var subjectId = _subjectService.GetBySubject(classDetailViewModel.SubjectName[i]);  
                if (subjectId == null || classId == null || studentId == null)
                {
                    return Json(new { Message = "Student Name or Class Name or Subject Name is missing!!!" });
                }

                var classDetail = _classDetailService.GetByClassDetail(subjectId.ID, classId.Id, studentId.Id);
                if (classDetail != null)
                {
                    return Json(new { Message = "Already exists the class and subject you will attend!!!" });
                }
                ClassDetailEntity classDetailEntity = new ClassDetailEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Class_Id = classId.Id,
                    Subject_Id = subjectId.ID,
                    Student_Id = studentId.Id,
                };
                _classDetailService.Create(classDetailEntity);
            }
            return Json(new { Message = "You have successfully registered the class and subject you will attend." });
        }

        // delete subject
        [Authorize]
        [HttpDelete("{id}")]
        [Route("subjectdelete")]
        public JsonResult ClassDetailSubjectRemove(ClassDetailViewModel classDetailViewModel)
        {
            var studentId = _studentServices.GetById(classDetailViewModel.StudentId);
            var classId = _classService.GetByName(classDetailViewModel.ClassName);
            for (int i = 0; i < classDetailViewModel.SubjectName.Length; i++)
            {
                var subjectId = _subjectService.GetBySubject(classDetailViewModel.SubjectName[i]);
                if (subjectId == null || classId == null || studentId == null)
                {
                    return Json(new { Message = "Student Name or Class Name or Subject Name is missing!!!" });
                }

                var classDetail = _classDetailService.GetByClassDetail(subjectId.ID, classId.Id, studentId.Id);
                if (classDetail == null)
                {
                    return Json(new { Message = "Your deleted subject is not found!!!" });
                }
                _classDetailService.Delete(classDetail.Id);
            }
            return Json(new { Message = "You have successfully removed the class and subject you will attend." });
        }

        // delete student
        [Authorize]
        [HttpDelete]
        [Route("studentdelete")]
        public JsonResult StudentDelete(StudentGetIdViewModel studentDeleteViewModel)
        {
            var studentId = _studentServices.GetById(studentDeleteViewModel.StudentId);
            if (studentId == null)
            {
                return Json(new { Message = "Student Not Found!!!" });
            }

            var classDetail = _classDetailService.RetrieveAll(studentId.Id);
            if (classDetail == null)
            {
                return Json(new { Message = "Your deleted student is not found!!!" });
            }
            for (int i = 0; i < classDetail.Count; i++)
            {
                _classDetailService.Delete(classDetail[i].Id);
            }

            _studentServices.Delete(studentId.Id);
            return Json(new { Message = "Successfully Deleted!!!" });
        }

        // student list
        [Authorize]
        [HttpGet]
        [Route("studentlist")]
        public JsonResult GetStudentsWithSubjects(StudentGetIdViewModel studentGetIdViewModel)
        {
            List<object> list = new List<object>();
            string classname = "";
            var currentStudent = _studentServices.GetById(studentGetIdViewModel.StudentId);
            var studentList = _classDetailService.RetrieveAll(currentStudent.Id).ToList();
            var classList = _classDetailService.RetrieveAllByClassId();
            for(int j = 0; j < classList.Count; j++)
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 0; i < studentList.Count; i++)
                {
                    if (studentList[i].Class_Id == studentList[j].Class_Id)
                    {
                        var subject = _subjectService.GetById(studentList[i].Subject_Id);
                        var className = _classService.GetById(studentList[i].Class_Id);
                        classname = className.Name;
                        arrayList.Add(subject.Name);
                    }
                }
                var response = new
                {
                    StudentName = currentStudent.Name,
                    ClassName = classname,
                    SubjectName = arrayList
                };
                
                list.Add(response);
                
            }

            return Json(new { Result = list });
        }

    }
}
