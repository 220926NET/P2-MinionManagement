using Microsoft.Data.SqlClient;
using Models;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class ProxyRegisterUser
    {
    SqlConnection connection = new SqlConnection("");

    public async Task<ProxyUserProfile> RegisterAsync(ProxyUserProfile User) {



PasswordHasher<ProxyUserProfile> P = new PasswordHasher<ProxyUserProfile>();

P.HashPassword

ProxyUserProfile Snapshot = new ProxyUserProfile();
await Task.Delay(1000);
try
        {
          
            connection.Open();
            SqlCommand cmv = new SqlCommand("INSERT INTO User_Login (UserName, PassWord) VALUES (@Username, @PassWord)", connection);
            cmv.Parameters.AddWithValue("@Username", User.UserName);
            cmv.Parameters.AddWithValue("@PassWord", User.Password);
            cmv.ExecuteNonQuery();



SqlCommand cmd = new SqlCommand($"INSERT INTO User_Profiles (UserName, FirstName, LastName, TroopCount) VALUES (@UserName, @FirstName, @LastName, @TroopCount)", connection);
cmd.Parameters.AddWithValue("@Username", User.UserName);
cmd.Parameters.AddWithValue("@FirstName", User.FirstName);
cmd.Parameters.AddWithValue("@LastName", User.LastName);            
cmd.Parameters.AddWithValue("@TroopCount", 0);
cmd.ExecuteNonQuery();





 //           SqlCommand cmd = new SqlCommand($"INSERT INTO User_Profiles (UserName, FirstName, LastName, TroopCount) VALUES (@UserName, @FirstName, @LastName, @TroopCount)", connection);
 //           cmd.Parameters.AddWithValue("@UserName", User.UserName);
       

             SqlCommand cm = new SqlCommand($"SELECT ID FROM User_Profiles WHERE UserName = '{User.UserName}'", connection);
                SqlDataReader reader = cm.ExecuteReader();
                    if(reader.HasRows)
                    {
                    while(reader.Read()) 
                        {
                            int id = (int) reader["Id"];
                           Snapshot.acceptValues(User.UserName, User.Password, User.FirstName, User.LastName, 0, id);                       
                        }
                    }
                  
        }
        catch(SqlException)
        {
            throw;
        }
        finally
        {
            
            connection.Close();
        }
  
 /*
  ProxyUserProfile SnapShot = new ProxyUserProfile {
    UserName = User.UserName,
    FirstName = User.FirstName,
    LastName = User.FirstName,
    Password = User.Password,
    TroopCount = 0
  };
  */
  return Snapshot;
  }   
}
}
