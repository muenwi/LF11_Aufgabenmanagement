namespace TaskApp.Api.Models
{
    public class RoleModel
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
}
