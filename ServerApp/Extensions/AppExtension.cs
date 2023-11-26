using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Dtos;
using ServerApp.Entities;
using ServerApp.Managers;

namespace ServerApp.Extension;

public static class AppExtension
{
    public static WebApplication AddPostMethodes(this WebApplication app)
    {

        app.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<EntityAppUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return TypedResults.Ok();
        });

        app.MapPost("/task", async ([FromBody] TaskDto task, [FromServices] ITaskManager manager,[FromServices]RoleManager<IdentityRole> roleManager, HttpContext context) =>
        {
            var entity = new EntityTask
            {
                Title = task.Title,
                Description = task.Description,
                StartDate = DateTime.Now,
                Status = task.Status,
            };

            EntityTask createdTask;

            if (string.IsNullOrWhiteSpace(task.UserId))
            {
                if (string.IsNullOrWhiteSpace(task.Role)) throw new ArgumentNullException("The task is not assigned");

                var role = await roleManager.Roles.FirstOrDefaultAsync(x => x.NormalizedName.Equals(task.Role.Trim().ToUpper()));

                if (role is null) throw new ArgumentNullException(nameof(IdentityRole));

                entity.UserId = string.Empty;

                createdTask = await manager.CreateTaskAsync(entity);

                await manager.CreateTask2RoleAsync(createdTask.Id, role.Id);
            }
            else
            {
                entity.UserId = task.UserId;
                createdTask = await manager.CreateTaskAsync(entity);
            }

            if (task is null) throw new ArgumentNullException();

            return TypedResults.Ok();
        });

        app.MapPost("/setup-application/", async ([FromServices]ITaskManager manager, [FromServices]UserManager<EntityAppUser> _userManager, [FromServices]RoleManager<IdentityRole> _roleManager) => {

            foreach(var role in Enum.GetNames(typeof(EntityRole.RoleName))) {

                var entityRole = new IdentityRole{
                    Name = role,
                    NormalizedName = role.Trim().ToUpper(),
                };

                await _roleManager.CreateAsync(entityRole);
            }
        
            var peter = new EntityAppUser() {
                UserName = "Peter",
                Email = "peter@mail.com",
            };

            await _userManager.CreateAsync(peter, "Test123$");

            var admin = new EntityAppUser() {
                UserName = "Admin",
                Email = "admin@mail.com",
            };

            await _userManager.CreateAsync(admin, "Admin123$");

            var petra = new EntityAppUser() {
                UserName = "Petra",
                Email = "petra@mail.com",
            };

            await _userManager.CreateAsync(petra, "Petra123$");

            var klaus = new EntityAppUser() {
                UserName = "Klaus",
                Email = "klaus@mail.com",
            };

            await _userManager.CreateAsync(klaus, "Klaus123$");

            var megan = new EntityAppUser() {
                UserName = "Megan",
                Email = "megan@mail.com",
            };

            await _userManager.CreateAsync(megan, "Megan123$");

            await _userManager.AddToRolesAsync(admin, Enum.GetNames(typeof(EntityRole.RoleName)));
            await _userManager.AddToRoleAsync(petra, "CEO");
            await _userManager.AddToRoleAsync(peter, "Azubi");
            await _userManager.AddToRoleAsync(klaus, "SeniorDev");
            await _userManager.AddToRoleAsync(megan, "JuniorDev");

            return TypedResults.Ok();
        }).AllowAnonymous();

        app.MapPost("/setup-application/tasks", async ([FromServices]ITaskManager manager, [FromServices]UserManager<EntityAppUser> _userManager, [FromServices]RoleManager<IdentityRole> _roleManager) => {

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Forschungsprojekt zum Klimawandel",
                Description = "Führe ein umfassendes Forschungsprojekt zu den Auswirkungen des Klimawandels durch, einschließlich Ursachen, Folgen und möglichen Lösungen. Präsentiere deine Ergebnisse in einem detaillierten Bericht mit relevanten Diagrammen und visuellen Elementen.",
                UserId = "ae1fa0e9-0624-47d8-b6e6-2830cd563d0f",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Literarische Analyse-Essay",
                Description = "Wähle ein klassisches Literaturstück aus, das in diesem Semester studiert wurde, und schreibe einen eingehenden Analyse-Essay. Erforsche Themen, Charaktere und den Schreibstil des Autors, um ein tiefes Verständnis des Textes zu zeigen.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Mathematische Problemlösungsherausforderung",
                Description = "Erstelle eine Reihe von anspruchsvollen Mathematikaufgaben, die kritisches Denken und Problemlösungsfähigkeiten erfordern. Löse jede Aufgabe schrittweise und erkläre die Argumentation hinter jeder Lösung.",
                UserId = "",
                Status = "Start",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Historische Zeitleistenpräsentation",
                Description = "Entwickle eine visuelle Zeitleiste, die einen bestimmten historischen Zeitraum oder ein Ereignis abdeckt. Füge wichtige Daten, bedeutende Persönlichkeiten und Meilensteine ein. Präsentiere deine Zeitleiste vor der Klasse und betone den historischen Kontext.",
                UserId = "",
                Status = "Stop",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Physikexperiment",
                Description = "Entwerfe und führe ein Physikexperiment durch, um ein bestimmtes Konzept zu erforschen. Dokumentiere den Experimentverlauf, notiere Daten und analysiere die Ergebnisse. Fasse deine Erkenntnisse in einem wissenschaftlichen Bericht zusammen.",
                UserId = "",
                Status = "Done",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Debatte über zeitgenössische Themen",
                Description = "Beteilige dich an einer Klassendebatte zu einem aktuellen Thema. Recherchiere verschiedene Perspektiven, bereite Argumente vor und nimm an einer strukturierten Debatte teil, in der du deine Position verteidigst und auf gegnerische Standpunkte eingehst.",
                UserId = "",
                Status = "Done",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Kreatives Schreiben Kurzgeschichte",
                Description = "Verfasse eine Kurzgeschichte, die eine starke Erzählstruktur, Charakterentwicklung und effektive Verwendung literarischer Mittel zeigt. Teile deine Geschichte mit der Klasse und gib Einblicke in deinen kreativen Prozess.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Chemie-Laborbericht",
                Description = "Führe ein Chemieexperiment durch, dokumentiere sorgfältig Verfahren und Beobachtungen. Schreibe einen umfassenden Laborbericht, der den Zweck des Experiments, die Methoden und Schlussfolgerungen sowie unerwartete Ergebnisse umfasst.",
                UserId = "",
                Status = "Start",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Sprachaustausch zu Gesundheit und Wohlbefinden",
                Description = "Paare dich mit einem Mitschüler zusammen, der eine andere Sprache lernt, und führe einen Gesprächsaustausch durch. Bereite eine Liste von Gesprächsthemen vor und übe das Sprechen in beiden Sprachen, um die Sprachkenntnisse zu verbessern.",
                UserId = "",
                Status = "Done",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Geografie-Kartenprojekt",
                Description = "Erstelle eine detaillierte Karte, die geografische Merkmale, kulturelle Aspekte und historische Sehenswürdigkeiten eines ausgewählten Landes hervorhebt. Präsentiere die Karte vor der Klasse und erkläre die Bedeutung jedes Elements.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Gesundheits- und Wellness-Forschungsarbeit",
                Description = "Untersuche ein gesundheitsbezogenes Thema, wie Ernährung, Bewegung oder psychische Gesundheit. Schreibe eine Forschungsarbeit, die das Thema erkundet, wissenschaftliche Studien zitiert und praktische Empfehlungen für einen gesunden Lebensstil gibt.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Technologie-Innovationspräsentation",
                Description = "Recherchiere eine kürzlich erfolgte technologische Innovation und erstelle eine Präsentation, die deren Auswirkungen auf die Gesellschaft, potenzielle Vorteile und ethische Überlegungen herausstellt. Diskutiere, wie die Innovation die Zukunft gestalten könnte.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Musikkompositionsprojekt",
                Description = "Komponiere ein originelles Musikstück unter Verwendung der in der Klasse erlernten musikalischen Konzepte. Führe die Komposition vor der Klasse auf und analysiere kurz die verwendeten musikalischen Elemente.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Wirtschaftsfallstudienanalyse",
                Description = "Analysiere eine real existierende wirtschaftliche Fallstudie unter Berücksichtigung von Faktoren wie Angebot und Nachfrage, Markttrends und staatlicher Intervention. Präsentiere deine Ergebnisse und schlage mögliche Lösungen oder politische Empfehlungen vor.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Psychologie-Experiment und Bericht",
                Description = "Entwickle ein Psychologieexperiment, um einen bestimmten Aspekt menschlichen Verhaltens zu untersuchen. Sammle und analysiere Daten und schreibe dann einen ausführlichen Bericht, der dein Experiment, die Ergebnisse und Schlussfolgerungen zusammenfasst.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Fitnessherausforderung im Sportunterricht",
                Description = "Entwerfe eine Fitnessherausforderung, die verschiedene Übungen und Aktivitäten umfasst. Stelle die Herausforderung der Klasse vor, ermutige Mitschüler zur Teilnahme und verfolge ihren Fortschritt über einen festgelegten Zeitraum.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Geschäftsplanentwicklung",
                Description = "Entwickle einen umfassenden Geschäftsplan für eine hypothetische Geschäftsidee. Füge Details wie Marktanalyse, finanzielle Prognosen und eine Marketingstrategie ein. Präsentiere deinen Geschäftsplan vor der Klasse.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Umweltwissenschaftlicher Exkursionsbericht",
                Description = "Nimm an einer umweltwissenschaftlichen Exkursion teil. Dokumentiere Beobachtungen, sammle Daten und mache Fotos. Schreibe einen detaillierten Bericht, der deine Erfahrungen und ökologischen Erkenntnisse zusammenfasst.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Kochvorführung in der kulinarischen Kunst",
                Description = "Plane und führe eine Kochvorführung durch, die eine bestimmte kulinarische Technik oder ein Gericht zeigt. Gib schrittweise Anweisungen, teile Kochtipps mit und biete Kostproben für deine Mitschüler an.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            await manager.CreateTaskAsync(new EntityTask() {
                Title = "Kunst-Portfolio-Präsentation",
                Description = "Stelle ein Portfolio deiner besten Kunstwerke zusammen, die während des Semesters erstellt wurden. Präsentiere dein Portfolio vor der Klasse, erkläre die Inspiration hinter jedem Werk und die künstlerischen Techniken, die angewendet wurden.",
                UserId = "",
                Status = "Todo",
                StartDate = DateTime.Now,
            });

            return TypedResults.Ok();
        }).AllowAnonymous();

        return app;
    }


    public static WebApplication AddGetMethodes(this WebApplication app)
    {
        app.MapGet("/task/user", async ([FromServices]ITaskManager manager, [FromServices]UserManager<EntityAppUser> _userManager,  ClaimsPrincipal user) =>
        {
            var identityUser = await _userManager.GetUserAsync(user);
        
            if (identityUser is null) throw new ArgumentNullException();
        
            var tasks = await manager.GetTaskByUserAsync(identityUser.Id);
        
            if (tasks is null) throw new ArgumentNullException();
        
            return TypedResults.Json(tasks);
        });
        
        app.MapGet("/task/role", async ([FromServices]ITaskManager manager, [FromServices]UserManager<EntityAppUser> _userManager, [FromServices]RoleManager<IdentityRole> _roleManager, ClaimsPrincipal user) => {
            var identityUser = await _userManager.GetUserAsync(user);

            if (identityUser is null) throw new ArgumentNullException();
        
            var roles = await _userManager.GetRolesAsync(identityUser);

            var tasks = new List<EntityTask>();
        
            var entityRoleList = _roleManager.Roles
                .Where(x => roles.Contains(x.Name))
                .Select(x => x.Id)
                .ToList();

            foreach (var roleId in entityRoleList) {
            
        
                tasks.AddRange(await manager.GetTasksByRoleAsync(roleId));
            }
        
            return TypedResults.Json(tasks);
        });
        
        app.MapGet("/user-names", async ([FromServices]UserManager<EntityAppUser> _userManager) => {
            var users = await _userManager.Users.Select( x => x.UserName).ToListAsync();
        
            return TypedResults.Json(users);
        });
        
        app.MapGet("/all-tasks", async ([FromServices] ITaskManager manager) => {
            var tasks = await manager.GetTasksAsync();
        
            return TypedResults.Json(tasks);
        });

        app.MapGet("/general-data", async ([FromServices] ITaskManager manager, [FromServices]UserManager<EntityAppUser> _userManager,[FromServices]RoleManager<IdentityRole> _roleManager,  ClaimsPrincipal user) => {
            var identityUser = await _userManager.GetUserAsync(user);
        
            if (identityUser is null) throw new ArgumentNullException();
        
            var roles = await _userManager.GetRolesAsync(identityUser);

            var entityRoleList = await _roleManager.Roles.Where(x => roles.Contains(x.Name)).Select(x => x.Id).ToListAsync();

            var tasks = await manager.GetTaskByUserAsync(identityUser.Id);

            var personalTaskCount = tasks.Count;
        
            foreach (var roleId in entityRoleList) {
                var TaskList = await manager.GetTasksByRoleAsync(roleId);

                foreach(var task in TaskList) {
                    if (!tasks.Any(x => x.Id.Equals(task.Id))) tasks.Add(task);
                }
            }

            var taskDoneCount = 0;
            var taskTodoCount = 0;
            var taskStartCount = 0;
            var taskStopCount = 0;

            foreach (var status in Enum.GetNames(typeof(EntityRole.Status))) {
                foreach (var task in tasks) {
                    // Check if current status is set on the task
                    if(!task.Status.Equals(status)) continue;

                    // increment the specific status
                    _ = status switch {
                        "ToDo" => taskTodoCount++,
                        "Start" => taskStartCount++,
                        "Stop" => taskStopCount++,
                        "Done" => taskDoneCount++,
                        _ => 0,
                    };
                }
            }

            var roleTaskCount = tasks.Count - personalTaskCount;

            var generalData = new GeneralDataDto() {
                PersonalTaskCount = personalTaskCount,
                AssignedViaRoleCount = roleTaskCount,
                TaskDoneCount = taskDoneCount,
                TaskTodoCount = taskTodoCount,
                TaskStartCount = taskStartCount,
                TaskStopCount = taskStopCount,
            };

            return TypedResults.Json(generalData);
        });
        

        return app;
    }

    public static WebApplication AddDeleteMethodes(this WebApplication app)
    {
        app.MapDelete("/task", async ([FromServices] ITaskManager manager, HttpContext context) =>
        {
            return TypedResults.Ok();
        });

        return app;
    }

    
    public static WebApplication AddPutMethodes(this WebApplication app)
    {
        app.MapPut("/task", async ([FromServices] ITaskManager manager, [FromBody]TaskDto task) =>
        {
            var entity = await manager.GetTaskAsync(task.Id);
            
            entity.Status = task.Status;
            entity.Title = task.Title;
            entity.Description = task.Description;

            if (string.IsNullOrWhiteSpace(task.UserId)) entity.UserId = string.Empty;

            entity.UserId = task.UserId;

            var updatedTask = await manager.UpdateTaskAsync(entity);

            if (updatedTask is null) throw new ArgumentNullException();

            return TypedResults.Ok();
        });

        return app;
    }
}
