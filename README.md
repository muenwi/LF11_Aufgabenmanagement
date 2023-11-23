# LF11_Aufgabenmanagement

Anweisungen zum starten der Anwendung:

1. Datenbank migrieren und aufsetzen

    Anweisungen um die Datenbanka aufzusetzen!

    Directory: <PATH_TO_REPO>/ServerApp

    1. dotnet ef migrations add InitialCreateServer --context ServerAppDbContext
    2. dotnet ef database update InitialCreateServer --context ServerAppDbContext
  
    Directory <PATH_TO_REPO>/TaskApp
   1. dotnet ef migrations add InitialCreateTask --context TaskAppDbContext
    2. dotnet ef database update InitialCreateTask --context TaskAppDbContext

3. Wie lädt man Tailwind?

    1. In der TaskApp "npm i" (shorthand for install) ausführen
    2. Danach könnt ihr einfach folgendes ausführen: "npx Tailwindcss -i wwwroot/css/app.css -o wwwroot/css/app.min.css --watch"
        -> Das lädt die Styles in die Anwendung die ihr braucht und schaut gleichzeitig bei Änderungen danach, ob neue Styles hinzugefügt werden müssen

4. Die ServerApp und die TaskApp starten
    Hier müsste für die VS-User einfach nur das Ausführen des .csproj files sein.

    Wenn man es über VS-Code ausführen will, hat man zwei Optionen:
    1. Man benutzt die C# Extension und führt darüber die .csproject aus
    2. Oder man benutzt die dotnet cli <<Path_To_Project>dotnet run>
