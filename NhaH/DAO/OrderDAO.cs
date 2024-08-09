using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NhaH.DTO;

namespace NhaH.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrderDAO();
                return instance;
            }
        }

        private OrderDAO() { }

        // Kết nối chuỗi kết nối CSDL
        private string connectionString = "Data Source=DESKTOP-273A8MJ\\SQLEXPRESS;Initial Catalog=QL_NhaHang1;Integrated Security=True";

        // Lấy thông tin hóa đơn và chi tiết đặt hàng theo số bàn
        public List<OrderDetail> GetOrderDetailsByTable(int tableNumber)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Sử dụng Store Procedure để lấy thông tin hóa đơn và chi tiết đặt hàng
                    using (SqlCommand command = new SqlCommand("GetOrderDetailsByTable", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TableNumber", tableNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderDetail orderDetail = new OrderDetail();
                                orderDetail.OrderID = Convert.ToInt32(reader["order_id"]);
                                orderDetail.DishID = Convert.ToInt32(reader["dish_id"]);
                                orderDetail.DishName = reader["dish_name"].ToString();
                                orderDetail.Quantity = Convert.ToInt32(reader["quantity"]);
                                orderDetail.PriceDish = Convert.ToDecimal(reader["priceDish"]);

                                orderDetails.Add(orderDetail);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (ném hoặc ghi vào log)
                Console.WriteLine(ex.Message);
            }

            return orderDetails;
        }

        public void UpdateOrderStatus(int tableID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string query = "UPDATE Orders SET status = @status WHERE table_number = @tableID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", "Paid");
                    command.Parameters.AddWithValue("@tableID", tableID);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateOrderStatus(int tableID, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming you have an "Orders" table with a column "status"
                string query = "UPDATE Orders SET status = @status WHERE table_number = @tableID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@tableID", tableID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<OrderDetail> GetUnpaidOrderDetailsByTable(int tableID)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetUnpaidOrderDetailsByTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TableID", tableID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                OrderID = Convert.ToInt32(reader["order_id"]),
                                DishID = Convert.ToInt32(reader["dish_id"]),
                                DishName = reader["dish_name"].ToString(),
                                Quantity = Convert.ToInt32(reader["quantity"]),
                                PriceDish = Convert.ToDecimal(reader["priceDish"])
                            };

                            orderDetails.Add(orderDetail);
                        }
                    }
                }
            }

            return orderDetails;
        }

    }
}
