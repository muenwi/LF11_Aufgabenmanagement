namespace Todo_App.Data
{
    public class Todo
    {
        //public RoleEnum.RoleName Role { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public RoleEnum.Status stat{ get; set; }

        //public Todo(RoleEnum.RoleName aRole, string aTitle, string aDescription, RoleEnum.Status aStatus)
        //{
        //    Role = aRole;
        //    Title = aTitle;
        //    Description = aDescription;
        //    stat = aStatus;   
        //}

        public string Role { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public Todo(string aRole, string aTitle, string aDescription, string aStatus)
        {
            Role = aRole;
            Title = aTitle;
            Description = aDescription;
            Status = aStatus;
        }
    }
}
