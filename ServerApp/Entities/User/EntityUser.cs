namespace TaskApp.Entities;

public class EntityUser
{
    private string Vorname { get; set; }
    private string Nachname { get; set; }
    private List<object> Aufgaben { get ;set; }
    private List<object> Rollen { get; set; }

    public User(string vorname, string nachname, object? aufgabe, object? rolle){
        Vorname = vorname;
        Nachname = nachname;
        Aufgaben = new List<object>();
        Rollen = new List<object>();
        if(aufgabe != null) Aufgaben.Add(aufgabe);
        if(rolle != null) Rollen.Add(rolle);
    }

    public List<object> AlleRollen (){
        return Rollen;
    }
        
    public List<object> AlleAufgaben (){
        return Aufgaben;
    }
}
