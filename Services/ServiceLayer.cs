using Models;
using DataAccess;

namespace Services
{
    public class ServiceLayer
    {
    DataAccessLayer DB = new DataAccessLayer();
        public async Task<ProxyUserProfile> RegisterANewUser(ProxyUserProfile user) 
    {
        ProxyUserProfile ret = await DB.RegisterANewUser(user);
        return ret;
    }

     public async Task<ProxyUserProfile> LoginUser(string name, string Password) 
    {
        ProxyUserProfile ret = await DB.LoginUser(name, Password);
        return ret;
    }
    }
}