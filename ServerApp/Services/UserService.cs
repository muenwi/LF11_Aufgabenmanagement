namespace ServerApp.Services;

public class UserService
{
    public User GetLoggedInUser()
    {
        var newUser = new User("Test", "Tester", null, null);
        var exampleTasks = new List<TaskItem>
        {
            new TaskItem(1, "Test 1", Aufgabenstatus.Neu, newUser),
            new TaskItem(2, "Test 2", Aufgabenstatus.in_Bearbeitung, newUser),
            new TaskItem(3, "Test 3", Aufgabenstatus.Fertig, newUser),
        };
        return newUser;
    }
}