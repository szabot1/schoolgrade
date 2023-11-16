using System.ComponentModel.DataAnnotations;

namespace schoolgrade.Models;

public record Student
{
    [Key]
    public Guid Id { get; init; }
    public Guid? GradeId { get; init; }

    public Grade Grade { get; init; } = null!;

    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    public string? Description { get; init; }

    public IList<Mark> Marks { get; } = new List<Mark>();

    public DateTimeOffset CreatedDate { get; init; }
}

public record StudentDto
{
    public Guid Id { get; init; }
    public GradeDto Grade { get; init; } = null!;
    public string? Username { get; init; }
    public string? Name { get; init; }
    public string? Email { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}

public record CreateStudentDto
{
    public Guid GradeId { get; init; }

    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    public string? Description { get; init; }
}

public record UpdateStudentDto
{
    public Guid GradeId { get; init; }

    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    public string? Description { get; init; }
}

public record DeleteStudentDto
{
    public Guid Id { get; init; }
}

public static class StudentExtensions
{
    public static StudentDto AsDto(this Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            Grade = student.Grade.AsDto(),
            Username = student.Username,
            Name = student.Name,
            Email = student.Email,
            Description = student.Description,
            CreatedDate = student.CreatedDate
        };
    }
}