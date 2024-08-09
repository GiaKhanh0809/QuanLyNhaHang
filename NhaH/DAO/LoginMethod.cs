using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DAO
{
    public class LoginMethod
    {
        DataConnection db = new DataConnection();
        public LoginMethod()
        {

        }
        public bool Login(string username, string password)
        {          
            using (SqlConnection connection = db.sqlstring())
            {
                using (SqlCommand cmd = new SqlCommand("LoginAccount", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.Add("@inputUsername", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@inputPassword", SqlDbType.VarChar, 255).Value = password;

                    connection.Open();

                    // Execute the stored procedure
                    object result = cmd.ExecuteScalar();

                    connection.Close();
                    if (result != null && result != DBNull.Value)
                    {
                        // Login successful
                        return true;
                    }
                    else
                    {
                        // Login failed
                        return false;
                    }
                }
            }
           
        }
    }
}
