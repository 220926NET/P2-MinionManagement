namespace Models
{
    public class ProxyUserProfile
    {
    private string _username = "";
    private string _firstname = "";
    private string _lastname = "";
    private string _password = "";
    public int Id {get; set;} 
    public int TroopCount {get; set;} 
       
    public string UserName {
        get {
            return _username;
        }
        set {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("We need a username");
            }
            else
            {
                _username = value;
            }
        }
    }

    public string FirstName {
        get {
            return _firstname;
        }
        set {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("We need a firstname");
            }
            else
            {
                _firstname = value;
            }
        }
    }

    public string LastName {
        get {
            return _lastname;
        }
        set {
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("We need a lastname");
            }
            else
            {
                _lastname = value;
            }
        }
    }

    public string Password {
        get {
            return _password;
        }
        set { 
            if(String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("We need a password");
            }
            else
            {
                _password = value;
            }
        }
    }
    


    public void acceptValues(string value, string value2, string value3, string value4, int value5, int value6) {
        UserName = value;
        Password = value2;
        FirstName = value3;
        LastName = value4;
        TroopCount = value5;
        Id = value6;
}
 
    public void Verify(string value, string value2) {
        UserName = value;
        Password = value2;
}
}
}