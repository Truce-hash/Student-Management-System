using ASPNETCore_DB.Interfaces;
using ASPNETCore_DB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore_DB.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public class StudentController : Controller
    {
        private readonly IStudent _studentRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StudentController(IStudent studentRepo, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            try
            {

                _studentRepo = studentRepo;
                _httpContextAccessor = httpContextAccessor;
                _webHostEnvironment = webHostEnvironment;
            }
            catch (Exception ex)
            {
                throw new Exception("Constructor not initialized - IStudent studentRepo");
            }
            
        }
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            pageNumber = pageNumber ?? 1;
            int pageSize = 3;

            ViewData["CurrentSort"] = sortOrder;
            ViewData["StudentNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else 
            { 
                searchString = currentFilter; 
            }

            ViewData["CurrentFilter"] = searchString;

            ViewResult viewResult =  View();

            try
            {
                viewResult = View(PaginatedList<Student>.Create(_studentRepo.GetStudents(searchString, sortOrder).AsNoTracking(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex) 
            {
                throw new Exception("No student records detected");
            }
                        
            return viewResult;
        }
        public IActionResult Details(string id)
        {
            ViewResult viewDetail = View();
            try
            {
                viewDetail = View(_studentRepo.Details(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail not found");
            }


            return viewDetail;
        }

        [HttpGet]
        public IActionResult Create()
        {
            Student student = new Student();
            string fileName = "DefaultPic.png";
            student.Photo = fileName;
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StudentNumber, FirstName, Surname, EnrollmentDate")] Student student)
 {
            var files = HttpContext.Request.Form.Files;//Handles the file upload from the form
            string webRootPath = _webHostEnvironment.WebRootPath;//Gets the wwwroot path of the project
            string upload = webRootPath + WebConstants.ImagePath;//Combines the wwwroot path with the image path defined in WebConstants to get the full path for uploading images
            string fileName = Guid.NewGuid().ToString();//Generates a unique file name using a GUID to avoid conflicts with existing files
            string extension = Path.GetExtension(files[0].FileName);//Gets the file extension of the uploaded file

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            student.Photo = fileName + extension;

            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepo.Create(student);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Student could not be created");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewResult viewDetail = View();
            try
            {
                viewDetail = View(_studentRepo.Details(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail not found");
            }
            return viewDetail;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            var files = HttpContext.Request.Form.Files;//Handles the file upload from the form
            string webRootPath = _webHostEnvironment.WebRootPath;//Gets the wwwroot path of the project
            string upload = webRootPath + WebConstants.ImagePath;//Combines the wwwroot path with the image path defined in WebConstants to get the full path for uploading images
            string fileName = Guid.NewGuid().ToString();//Generates a unique file name using a GUID to avoid conflicts with existing files
            string extension = Path.GetExtension(files[0].FileName);//Gets the file extension of the uploaded file

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
                student.Photo = fileName + extension;            

            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepo.Edit(student);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail could not be edited");
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            ViewResult viewDetail = View();
            try
            {
                viewDetail = View(_studentRepo.Details(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Student detail not found");
            }
            return viewDetail;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([Bind("StudentNumber, FirstName, Surname, EnrollmentDate")] Student student)
        {
            try 
            {
                _studentRepo.Delete(student);
            }
            catch (Exception ex) 
            {
                throw new Exception("Student could not be deleted");
            }
            
            return RedirectToAction(nameof(Index));
        }


    }
}
