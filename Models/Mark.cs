using System.ComponentModel.DataAnnotations;

namespace schoolgrade.Models;

public record Mark
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public Guid StudentId { get; init; }

    public Student Student { get; init; } = null!;

    [Required]
    public string Subject { get; init; } = null!;

    [Required]
    public int Score { get; init; }

    public string? Description { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
}

public record CreateMarkDto
{
    [Required]
    public Guid StudentId { get; init; }

    [Required]
    public string Subject { get; init; } = null!;

    [Required]
    public int Score { get; init; }

    public string? Description { get; init; }
}

public record UpdateMarkDto
{
    [Required]
    public Guid Id { get; init; }

    [Required]
    public string Subject { get; init; } = null!;

    [Required]
    public int Score { get; init; }

    public string? Description { get; init; }
}

public record DeleteMarkDto
{
    [Required]
    public Guid Id { get; init; }
}

public record MarkDto
{
    public Guid Id { get; init; }
    public Guid StudentId { get; init; }
    public string Subject { get; init; } = null!;
    public int Score { get; init; }
    public string? Description { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}

public static class MarkExtensions
{
    public static MarkDto AsDto(this Mark mark)
    {
        return new MarkDto
        {
            Id = mark.Id,
            StudentId = mark.StudentId,
            Subject = mark.Subject,
            Score = mark.Score,
            Description = mark.Description,
            CreatedDate = mark.CreatedDate
        };
    }
}