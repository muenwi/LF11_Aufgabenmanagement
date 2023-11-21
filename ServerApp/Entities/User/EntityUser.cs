namespace ServerApp.Entities;

public class EntityUser
{
    public string Vorname { get; set; }
    public string Nachname { get; set; }
    public List<EntityTask> Tasks { get; set; }
    public List<EntityRole> Rollen { get; set; }

    public User(string vorname, string nachname, List<EntityTask>? tasks, EntityRole? rolle)
    {
        Vorname = vorname;
        Nachname = nachname;
        Tasks = new List<EntityTask>();
        Rollen = new List<EntityRole>();
        if (tasks?.Count > 0) Tasks.AddRange(tasks);
        if (rolle != null) Rollen.Add(rolle);
    }
}
