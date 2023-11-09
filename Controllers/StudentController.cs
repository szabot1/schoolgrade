using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolgrade.Data;
using schoolgrade.Models;

namespace schoolgrade.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
    [HttpGet]
    public IEnumerable<StudentDto> GetAll(SchoolContext ctx)
    {
        return ctx.Students
            .Include(s => s.Grade)
            .Select(s => s.AsDto());
    }

    [HttpGet("data")]
    public IEnumerable<object> GetData(SchoolContext ctx)
    {
        return ctx.Students
            .Include(s => s.Grade)
            .Select(s => new
            {
                s.Name,
                gradeName = s.Grade.Name,
                gradeSection = s.Grade.Section
            })
            .ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<StudentDto> GetById(Guid id, SchoolContext ctx)
    {
        var student = ctx.Students
            .Include(s => s.Grade)
            .SingleOrDefault(s => s.Id == id);

        if (student is null)
        {
            return NotFound();
        }

        return student.AsDto();
    }

    [HttpPost]
    public ActionResult<StudentDto> Create(CreateStudentDto studentDto, SchoolContext ctx)
    {
        var grade = ctx.Grades!.Find(studentDto.GradeId);

        if (grade is null)
        {
            return BadRequest("Invalid grade id.");
        }

        var student = new Student
        {
            Id = Guid.NewGuid(),
            Username = studentDto.Username,
            Name = studentDto.Name,
            Email = studentDto.Email,
            Description = studentDto.Description,
            Grade = grade,
            CreatedDate = DateTimeOffset.UtcNow
        };

        ctx.Students.Add(student);
        ctx.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult<StudentDto> Update(Guid id, UpdateStudentDto studentDto, SchoolContext ctx)
    {
        var student = ctx.Students.Find(id);

        if (student is null)
        {
            return NotFound();
        }

        ctx.Entry(student).CurrentValues.SetValues(studentDto);
        ctx.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<StudentDto> Delete(Guid id, SchoolContext ctx)
    {
        var student = ctx.Students.Find(id);

        if (student is null)
        {
            return NotFound();
        }

        ctx.Students.Remove(student);
        ctx.SaveChanges();

        return NoContent();
    }
}