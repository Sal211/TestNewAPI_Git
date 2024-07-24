using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testNewAPI.ServicesResponse;
using testNewAPI.DTOS;
using testNewAPI.Models;
using testNewAPI.Services.Contract;
using Microsoft.AspNetCore.Authorization;

namespace testNewAPI.Controllers
{
    [Route("AC/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        private IActionResult HandleError(string param)
        {
            ModelState.AddModelError("", param);
            return StatusCode(500, ModelState);
        }
        private  IActionResult Issuccess<T>(ServiceResponse<T> status, string param)
        {
            if (!status.Success && status.Message == "ErrorRespo")return  HandleError($"Some thing went wrong in respository layer when {param}  student");
            if (!status.Success && status.Message.Contains("server error")) return HandleError("Internal server error");
            if (!status.Success && status.Message == "Not Found") return NotFound("Student NotFound!!!");
            if (!status.Success && status.Message.Contains("connection error.")) return HandleError("Database connection error.");
            if (!status.Success && status.Message == "Error") return HandleError($"Some thing went wrong in Service layer when {param} student");
            return Ok(status);
         }

        [HttpPost("PostStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentDto newStudent)
        {
            if (newStudent == null || !ModelState.IsValid) return BadRequest(ModelState);
            var status = await _studentService.CreateStudentAsync(newStudent);
             return Issuccess(status, "adding");
         }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllStudent()
        {
            var status = await _studentService.GetAllStudentAsyn();
            return Issuccess(status, "getting");
        }
        [HttpPost("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudent([FromBody] StudentDto updateStudent)
        {
            if (updateStudent == null || !ModelState.IsValid) return BadRequest(ModelState);
            var status = await _studentService.UpdateStudentAsync(updateStudent);
            return Issuccess(status, "deleting");
        }
        [HttpPost("DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent([FromBody] int Id)
        {
            var status = await _studentService.DeleteStudentAsync(Id);
            return Issuccess(status, "deleting");
        }
        [HttpGet("GetStudentByID/{Id}" , Name = "GetStudentByID")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var status = await _studentService.GetStudentByIdAsync(Id);          
            return Issuccess(status, "getting");
        }

    }
}
