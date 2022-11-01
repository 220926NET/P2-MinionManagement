using Models;
using Services;

namespace DataAccess
{
    public class DataAccessLayer
    {
    ProxyRegisterUser Register = new ProxyRegisterUser();
    
    ProxyLogin Login = new ProxyLogin();
         public async Task<ProxyUserProfile> RegisterANewUser(ProxyUserProfile user) 
    {
        ProxyUserProfile ret = await Register.RegisterAsync(user);
        return ret;              
    }


     public async Task<ProxyUserProfile> LoginUser(string name, string Password) 
    {
        ProxyUserProfile ret = await Login.LoginAsync(name, Password);
        return ret;              
    }

    }
}