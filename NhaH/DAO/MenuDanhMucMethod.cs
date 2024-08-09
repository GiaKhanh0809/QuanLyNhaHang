using NhaH.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DAO
{
    public class MenuDanhMucMethod
    {
        DataConnection db = new DataConnection();
        public MenuDanhMucMethod()
        {

        }
        public List<DanhMuc> Getds ()
        {
            List<DanhMuc> listds = new List<DanhMuc>();
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "select * from MenuCategories";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DanhMuc danhMuc = new DanhMuc
                            {
                                CategoryId = Convert.ToInt32(reader["category_id"]),
                                CategoryName = reader["category_name"].ToString()
                              
                            };

                            listds.Add(danhMuc);
                        }
                    }
                }
                connection.Close();
            }
            return listds;
        }
        public int ThemDS(string categoryName)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "INSERT INTO [dbo].[MenuCategories] ([category_name]) VALUES (@categoryName)";
                using (SqlCommand checkCmd = new SqlCommand(sql, connection))
                {

                    checkCmd.Parameters.AddWithValue("@categoryName", categoryName);
                    int existingCount = (int)checkCmd.ExecuteNonQuery();
                    connection.Close();
                    return existingCount;
                }
                
            }
        }
        public int XoaDS(int id)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "delete MenuCategories where category_id = " + id;
                using (SqlCommand checkCmd = new SqlCommand(sql, connection))
                {                  
                    int existingCount = (int)checkCmd.ExecuteNonQuery();
                    connection.Close();
                    return existingCount;
                }

            }
        }
        public int  Update(int id,string text)
        {
            using(SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "update MenuCategories set category_name = N'"+text+ "' where [category_id] = " + id;
                using (SqlCommand checkCmd = new SqlCommand(sql, connection))
                {
                    int existingCount = (int)checkCmd.ExecuteNonQuery();
                    connection.Close();
                    return existingCount;
                }

            }
        }
    }
}
