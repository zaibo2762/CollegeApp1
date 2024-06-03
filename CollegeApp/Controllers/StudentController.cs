using CollegeApp.Models;
using CollegeApp.MyLogging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
       private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger)
        {
                _logger = logger;
        }
        [HttpGet]
        [Route("All", Name = "GetAllStudent")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudent()
        {
            _logger.LogInformation("Get Student MEthod Started");
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
                _logger.LogWarning("Bad Requst");
                return BadRequest();
            }
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
            {
                _logger.LogError("Student Not Found with NAme");
                return NotFound($"The Student With Name {name} not found");
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
        [HttpPut]
        public ActionResult UpdateStudent([FromBody] StudentDTO model)
        {
            if(model == null | model.Id <= 0)
            {
                return BadRequest();
            }
            var existingStudent = CollegeRepository.Students.Where(s=> s.Id == model.Id).FirstOrDefault();
            if(existingStudent == null) {
                return NotFound();
            }
            
            existingStudent.Id = model.Id;
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;
            return NoContent();

        }

        [HttpPatch]
        public ActionResult UpdateStudentPartial( int id,[FromBody] JsonPatchDocument<StudentDTO> patchdocument)
        {
            if (patchdocument == null | id <= 0)
            {
                return BadRequest();
            }
            var existingStudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            if (existingStudent == null)
            {
                return NotFound();
            }
            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                StudentName = existingStudent.StudentName,
                Email = existingStudent.Email,
                Address = existingStudent.Address,
            };
            patchdocument.ApplyTo(studentDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            existingStudent.Id = studentDTO.Id;
            existingStudent.StudentName = studentDTO.StudentName;
            existingStudent.Email = studentDTO.Email;
            existingStudent.Address = studentDTO.Address;
            return NoContent();

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

