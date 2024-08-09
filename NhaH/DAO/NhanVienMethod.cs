using NhaH.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaH.DAO
{
    public class NhanVienMethod
    {
        DataConnection db = new DataConnection();
        public NhanVienMethod()
        {

        }
        public List<NhanVienModel> getds()
        {
            List<NhanVienModel> listnhanViens = new List<NhanVienModel>();
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "select [employee_id],[employee_name],[position],Phone, Ngaysinh ,[hire_date]from Employees";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NhanVienModel nhanvien = new NhanVienModel
                            {

                                ID = Convert.ToInt32(reader["employee_id"].ToString()),
                                TenNV = reader["employee_name"].ToString(),
                                DiaChi = reader["position"].ToString(),
                                phone = reader["Phone"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["Ngaysinh"].ToString()),
                                NgayVaoLam = Convert.ToDateTime(reader["hire_date"].ToString()),




                            };

                            listnhanViens.Add(nhanvien);
                        }
                    }
                }
            }
            return listnhanViens;
        }
        public NhanVienModel getitem(int id)
        {
            NhanVienModel nhanvien = new NhanVienModel(); // Create a single instance to store the result
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "select [employee_name],Phone,[position], Ngaysinh ,salary,[username],[password]from Employees where employee_id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            nhanvien.TenNV = reader["employee_name"].ToString();
                            nhanvien.phone = reader["Phone"].ToString();
                            nhanvien.DiaChi = reader["position"].ToString();
                            nhanvien.NgaySinh = Convert.ToDateTime(reader["Ngaysinh"]);
                            nhanvien.Luong = Convert.ToDecimal(reader["salary"].ToString());
                            nhanvien.Username = reader["username"].ToString();
                            nhanvien.password = reader["password"].ToString();


                        }
                    }
                }
            }
            return nhanvien;
        }
        public int update(int id, string ten, string diachi, string luong, string phone, string ngaysinh, string user, string pass)
        {

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("UpdateEmployee", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Set the parameters
                    cmd.Parameters.AddWithValue("@manv", id); // Replace with the actual employee ID
                    cmd.Parameters.AddWithValue("@tennv", ten);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@salary", luong);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@ngaysinh", DateTime.Parse(ngaysinh));
                    cmd.Parameters.AddWithValue("@username", user);
                    cmd.Parameters.AddWithValue("@password", pass);
                    int kq = cmd.ExecuteNonQuery();
                    return kq;
                }
            }

        }
        public int Add(string ten, string diachi, string luong, string phone, string ngaysinh, string user, string pass)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("AddNhanVien", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tennv", ten);
                    cmd.Parameters.AddWithValue("@diachi", diachi);
                    cmd.Parameters.AddWithValue("@salary", luong);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@ngaysinh", DateTime.Parse(ngaysinh));
                    cmd.Parameters.AddWithValue("@username", user);
                    cmd.Parameters.AddWithValue("@password", pass);
                    int kq = cmd.ExecuteNonQuery();
                    return kq;
                }
            }

        }
        public int Delete(int id)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                string sql = "delete Employees where employee_id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;

                }
            }

        }
    }
}

