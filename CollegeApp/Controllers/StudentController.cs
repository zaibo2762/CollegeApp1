using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudent")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO()
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Email = item.Email,
            //        Address = item.Address,
            //    };
            //    students.Add(obj);
            //}
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email,
            });
            return Ok(CollegeRepository.Students);
        }
        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"The Student With ID {name} not found");
            }
            return Ok(student);
        }
        [HttpGet("{id:int}")]
        public ActionResult<StudentDTO> GetStudentbyId(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if(student == null)
            {
                return NotFound($"The Student With ID {id} not found");
            }
            var studentDTO = new StudentDTO { 
            Id = student.Id,
            StudentName = student.StudentName,
            Email = student.Email,
            Address = student.Address
            };
            return Ok(student);
        }
        [HttpPost]
        public ActionResult<StudentDTO> Createtudent([FromBody]StudentDTO model)
        {
            if(model == null)
            {
            return BadRequest();
            }
            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student { 
            Id = model.Id,
            StudentName = model.StudentName,
            Email = model.Email,
            Address = model.Address,
            };
            CollegeRepository.Students.Add(student);
            model.Id = student.Id;
            return Ok(model);
        }
        [HttpDelete]
        [Route("{id}", Name = "DeleteStudentByID")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"The Student With ID {id} not found");
            }
            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}

