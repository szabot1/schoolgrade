using System.ComponentModel.DataAnnotations;

namespace schoolgrade.Models;

public record Grade
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    [MaxLength(10)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(5)]
    public string? Section { get; init; }

    public string? Description { get; init; }

    public IList<Student> Students { get; } = new List<Student>();

    public DateTimeOffset CreatedDate { get; init; }
}

public record GradeDto
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Section { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}

public record CreateGradeDto
{
    [Required]
    [MaxLength(10)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(5)]
    public string? Section { get; init; }

    public string? Description { get; init; }
}

public record UpdateGradeDto
{
    [Required]
    [MaxLength(10)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(5)]
    public string? Section { get; init; }

    public string? Description { get; init; }
}

public record DeleteGradeDto
{
    public Guid Id { get; init; }
}

public static class GradeExtensions
{
    public static GradeDto AsDto(this Grade grade)
    {
        return new GradeDto
        {
            Id = grade.Id,
            Name = grade.Name,
            Section = grade.Section,
            Description = grade.Description,
            CreatedDate = grade.CreatedDate
        };
    }
}