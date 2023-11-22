namespace ServerApp.Entities;

public class EntityRole
{
    public enum RoleName
    {
        Azubi,
        JuniorDev,
        SeniorDev,
        CEO
    }

    public enum Status
    {
        ToDo,
        Start,
        Stop,
        Done
    }
}
