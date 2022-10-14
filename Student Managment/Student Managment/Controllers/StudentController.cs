using Dapper;
using Microsoft.AspNetCore.Mvc;
using Student_Managment.Models;
using Student_Managment.Services;
using System.Configuration;
using System.Linq;

namespace Student_Managment.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentServices _studentServices;

        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            _configuration = configuration;
            _studentServices = studentServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            StudentViewModel model = new StudentViewModel();
            model.StudentList = _studentServices.GetStudentList().ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddUpdateStudent(Student student)
        {
            StudentViewModel model = new StudentViewModel();
            student.Created_By = 1;
            string url = Request.Headers["Referer"].ToString();

            string result = string.Empty;
            if (student.Student_Id > 0)
            {
                result = _studentServices.UpdateStudent(student);
            }
            else
            {
                result = _studentServices.InsertStudent(student);
            }
            if (result != string.Empty)
            {
                TempData["Massage"] = result;
                return Redirect(url);
            }
            else
            {
                TempData["Massage"] = result;
                return Redirect(url);
            }
        }
        [HttpPost]
        public IActionResult DeleteRecord(int Student_ID)
        {
            string url = Request.Headers["Referer"].ToString();
            string result = _studentServices.DeleteStudent(Student_ID);
            if(result== "Record Delete Sucessfully")
            {
                return Json(new { Massage = "Record Delete Sucessfully" });
            }
            else
            {
                return Json(new { Massage = "Error" });
            }
        }
    }
}
