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



                SqlCommand cm = new SqlCommand($"SELECT ID FROM User_Profiles WHERE UserName = '{Verify.UserName}' AND PassWord = '{Verify.Password}", connection);
                SqlDataReader reader2 = cm.ExecuteReader();
                    if(reader2.HasRows)
                    {
                    while(reader2.Read()) 
                        {
                            int id = (int) reader["Id"];
                    //       Snapshot.acceptValues(User.UserName, User.Password, User.FirstName, User.LastName, 0, id);                       
                        }
                    }
                  







                }


                    catch(SqlException){
                    throw;
                }
                    finally{
                    connection.Close();
                }
                return Card;
   }
    }
}