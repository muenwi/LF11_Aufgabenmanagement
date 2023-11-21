namespace ServerApp.Services;

public class UserService
{
    public User GetLoggedInUser()
    {
        var newUser = new User("Test", "Tester", null, null);
        var exampleTasks = new List<TaskItem>
        {
            new TaskItem(1, "Test 1", "Neu", newUser),
            new TaskItem(2, "Test 2", "in Bearbeitung", newUser),
            new TaskItem(3, "Test 3", "Fertig", newUser),
        };
        return newUser;
    }
}