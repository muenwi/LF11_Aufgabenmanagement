using System.ComponentModel.DataAnnotations;

namespace ServerApp.Entities;

public class EntityTask2Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoleId {get; set;}

    [Required]
    public int TaskId {get; set;}
}
