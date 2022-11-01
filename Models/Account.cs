namespace Models;

public class Account{
    
    // Generate unique Account Number by DB
    int? Number{get;}
    public int UserID{get;set;}

    // Initial Amount set to 200
    public double Amonut{get;} = 200;
    public string AccountType{get;set;}


}