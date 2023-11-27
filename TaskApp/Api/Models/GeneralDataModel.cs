namespace TaskApp.Api.Models
{
    public class GeneralDataModel
    {
        public int PersonalTaskCount { get; set; } = 0;
        public int AssignedViaRoleCount { get; set; } = 0;
        public int TaskDoneCount { get; set; } = 0;
        public int TaskTodoCount { get; set; } = 0;
        public int TaskStartCount { get; set; } = 0;
        public int TaskStopCount { get; set; } = 0;
    }
}
