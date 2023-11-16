using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolgrade.Data;
using schoolgrade.Models;

namespace schoolgrade.Controllers;

[ApiController]
[Route("api/grades")]
public class GradeController : ControllerBase
{
    [HttpGet]
    public IEnumerable<GradeDto> GetAll(SchoolContext ctx)
    {
        return ctx.Grades
            .Select(g => g.AsDto());
    }

    [HttpGet("{id}")]
    public ActionResult<GradeDto> GetById(Guid id, SchoolContext ctx)
    {
        var grade = ctx.Grades
            .SingleOrDefault(g => g.Id == id);

        if (grade is null)
        {
            return NotFound();
        }

        return grade.AsDto();
    }

    [HttpPost]
    public ActionResult<GradeDto> Create(CreateGradeDto gradeDto, SchoolContext ctx)
    {
        var grade = new Grade
        {
            Id = Guid.NewGuid(),
            Name = gradeDto.Name,
            Section = gradeDto.Section,
            Description = gradeDto.Description,
            CreatedDate = DateTimeOffset.UtcNow
        };

        ctx.Grades.Add(grade);
        ctx.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = grade.Id }, grade.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult<GradeDto> Update(Guid id, UpdateGradeDto gradeDto, SchoolContext ctx)
    {
        var grade = ctx.Grades.Find(id);

        if (grade is null)
        {
            return NotFound();
        }

        ctx.Entry(grade).CurrentValues.SetValues(gradeDto);
        ctx.SaveChanges();

        return grade.AsDto();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id, SchoolContext ctx)
    {
        var grade = ctx.Grades.Find(id);

        if (grade is null)
        {
            return NotFound();
        }

        ctx.Grades.Remove(grade);
        ctx.SaveChanges();

        return NoContent();
    }

    [HttpGet("{id}/students")]
    public IEnumerable<StudentDto> GetStudents(Guid id, SchoolContext ctx)
    {
        return ctx.Students
            .Include(s => s.Grade)
            .Where(s => s.GradeId == id)
            .Select(s => s.AsDto());
    }

    [HttpGet("{id}/students/names")]
    public ActionResult GetStudentNames(Guid id, SchoolContext ctx)
    {
        var grade = ctx.Grades
            .Include(g => g.Students)
            .Where(g => g.Id == id)
            .Select(g => new
            {
                GradeName = g.Name,
                GradeSection = g.Section,
                StudentNames = g.Students.Select(s => s.Name)
            })
            .FirstOrDefault();

        if (grade is null)
        {
            return NotFound();
        }

        return Ok(grade);
    }
}