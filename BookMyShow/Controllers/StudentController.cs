using BookMyShow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookMyShow.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        OnlineCourseDbContext onlineCourseDbContext;
        public StudentController(ILogger<StudentController> logger)
        {
           onlineCourseDbContext = new OnlineCourseDbContext();
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = onlineCourseDbContext.Students.ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            onlineCourseDbContext.Students.Add(student);
            onlineCourseDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = onlineCourseDbContext.Students.Where(x=>x.StudentId == id).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            Student updateStudent = onlineCourseDbContext.Students.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            updateStudent.Name = student.Name;
            updateStudent.PhoneNumber = student.PhoneNumber;
            updateStudent.Address = student.Address;

            onlineCourseDbContext.SaveChanges();

            return RedirectToAction("Detail", new {id = student.StudentId});
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Student student = onlineCourseDbContext.Students.Where(x => x.StudentId == id).FirstOrDefault();
            return View(student);
        }



        [HttpPost]
        public IActionResult Delete(Student student)
        {
            Student deleteStudent = onlineCourseDbContext.Students.Where(x => x.StudentId == student.StudentId).FirstOrDefault();
            onlineCourseDbContext.Students.Remove(deleteStudent);
            onlineCourseDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = onlineCourseDbContext.Students.Where(x => x.StudentId == id).FirstOrDefault();
            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
