using NhaH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaH.DAO
{
    public class MonAnMethod
    {
        DataConnection db = new DataConnection();

        public MonAnMethod()
        {

        }
        public List<MonAn> LoadMon(int idcate)
        {         
            List<MonAn> listmonan = new List<MonAn>();
            using (SqlConnection connection = db.sqlstring())
            {
                using (SqlCommand cmd = new SqlCommand("LoadDishes", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameter
                    cmd.Parameters.Add("@idcate", SqlDbType.Int).Value = idcate;

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonAn dish = new MonAn
                            {
                                IDmon = Convert.ToInt32(reader["dish_id"]),
                                TenMonAn = reader["dish_name"].ToString(),
                                IDCategory = Convert.ToInt32(reader["category_id"]),
                                ImageMon = reader["Anh_Monan"].ToString(), // Load image from file
                                GiaMon = Convert.ToDouble(reader["price"])
                            };

                            listmonan.Add(dish);
                        }
                    }
                }
            }
            return listmonan;
        }
        public List<MonAn> LoadMonAll()
        {
            List<MonAn> listmonan = new List<MonAn>();
            using (SqlConnection connection = db.sqlstring())
            {
                using (SqlCommand cmd = new SqlCommand("GetDishesAll", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                  
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MonAn dish = new MonAn
                            {
                                IDmon = Convert.ToInt32(reader["dish_id"]),
                                TenMonAn = reader["dish_name"].ToString(),
                                IDCategory = Convert.ToInt32(reader["category_id"]),
                                ImageMon = reader["Anh_Monan"].ToString(), // Load image from file
                                GiaMon = Convert.ToDouble(reader["price"])
                            };

                            listmonan.Add(dish);
                        }
                    }
                }
            }
            return listmonan;
        }
        public List<DanhMuc> LoadDanhMuc()
        {
            List<DanhMuc> categoryList = new List<DanhMuc>();

            // Assuming db.sqlstring() returns the connection string
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                // SQL query to retrieve categories
                string query = "SELECT category_id, category_name FROM MenuCategories";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DanhMuc category = new DanhMuc
                            {
                                CategoryId = Convert.ToInt32(reader["category_id"]),
                                CategoryName = reader["category_name"].ToString()
                            };

                            categoryList.Add(category);
                        }
                    }
                }
            }
            return categoryList;
        }
        public int Xoa(int id)
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    string sql = "delete Dishes where dish_id = " + id;
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // Assuming 'id' is the parameter value
                        cmd.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                return 0;
            }

        }
    }
}
