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
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 90;
        public static int TableHeight = 90;

        private TableDAO() { }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            DataTable data = DataProvider.Instance.ExecuteQuery("GetTableList");

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }
        private string connectionString = "Data Source=DESKTOP-273A8MJ\\SQLEXPRESS;Initial Catalog=QL_NhaHang1;Integrated Security=True";
        public void UpdateTableStatus(int tableID, string status)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                string query = "UPDATE TableStatus SET tableStatus = @status WHERE table_number = @tableID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@tableID", tableID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
