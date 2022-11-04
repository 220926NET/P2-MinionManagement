using Microsoft.Data.SqlClient;
using Models;

namespace DataAccess
{
    public class ProxyLogin
    {
           SqlConnection connection = new SqlConnection("");
           public async Task<ProxyUserProfile> LoginAsync(string name, string Password) {
ProxyUserProfile Verify = new ProxyUserProfile();
   await Task.Delay(1000);
   try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT * FROM User_Login WHERE UserName = '{name}' AND PassWord = '{Password}';", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    if(reader.HasRows)
                    {
                    while(reader.Read()) 
                        {
                            string username = (string) reader["UserName"];
                            string password = (string) reader["PassWord"];            
                    Verify.Verify(username, password);
                    }
                    }
                  connection.Close();   
                  connection.Open();
                SqlCommand cm = new SqlCommand($"SELECT * FROM User_Profiles WHERE UserName = '{Verify.UserName}'", connection);
                SqlDataReader reader2 = cm.ExecuteReader();
                    if(reader2.HasRows)
                    {
                    while(reader2.Read()) 
                        {
                            int id = (int) reader2["Id"];
                            string UserName = (string) reader2["UserName"];
                            string FirstName = (string) reader2["FirstName"];
                            string LastName = (string) reader2["LastName"];
                            int troop = (int) reader2["TroopCount"];
                    Verify.storeInformation(UserName, FirstName, LastName, troop, id);                       
                        }
                    }
                }
                    catch(SqlException){
                    throw;
                }
                    finally{
                    connection.Close();
                }
                return Verify;
   }
    }
}
