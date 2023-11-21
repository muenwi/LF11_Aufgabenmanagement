namespace ServerApp.Entities;

public class EntityTask
{
    public int Id { get; set; }

    public string Name { get; set; }

    public EntityUser User { get; set; }
}
