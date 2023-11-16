using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using schoolgrade.Data;
using schoolgrade.Models;

namespace schoolgrade.Controllers;

[ApiController]
[Route("api/marks")]
public class MarkController : ControllerBase
{
    [HttpGet]
    public IEnumerable<MarkDto> GetAll(SchoolContext ctx)
    {
        return ctx.Marks
            .Select(m => m.AsDto());
    }

    [HttpGet("{id}")]
    public ActionResult<MarkDto> GetById(Guid id, SchoolContext ctx)
    {
        var mark = ctx.Marks
            .SingleOrDefault(m => m.Id == id);

        if (mark is null)
        {
            return NotFound();
        }

        return mark.AsDto();
    }

    [HttpPost]
    public ActionResult<MarkDto> Create(CreateMarkDto markDto, SchoolContext ctx)
    {
        var mark = new Mark
        {
            Id = Guid.NewGuid(),
            StudentId = markDto.StudentId,
            Subject = markDto.Subject,
            Score = markDto.Score,
            Description = markDto.Description,
            CreatedDate = DateTimeOffset.UtcNow
        };

        ctx.Marks.Add(mark);
        ctx.SaveChanges();

        return CreatedAtAction(nameof(GetById), new { id = mark.Id }, mark.AsDto());
    }

    [HttpPut("{id}")]
    public ActionResult<MarkDto> Update(Guid id, UpdateMarkDto markDto, SchoolContext ctx)
    {
        var mark = ctx.Marks.Find(id);

        if (mark is null)
        {
            return NotFound();
        }

        ctx.Entry(mark).CurrentValues.SetValues(markDto);
        ctx.SaveChanges();

        return mark.AsDto();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id, SchoolContext ctx)
    {
        var mark = ctx.Marks.Find(id);

        if (mark is null)
        {
            return NotFound();
        }

        ctx.Marks.Remove(mark);
        ctx.SaveChanges();

        return NoContent();
    }
}