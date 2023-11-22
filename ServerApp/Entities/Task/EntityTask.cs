using System.ComponentModel.DataAnnotations;

namespace ServerApp.Entities;

public class EntityTask
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Title { get; set; }

    public string Description { get; set; }

    public DateTime StartDate {get; set;}

    public Guid UserId { get; set; }


    public string Status { get; set; }
}
