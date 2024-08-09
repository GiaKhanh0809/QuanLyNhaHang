using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaH.Usercontrol.Admin.NhanVien.Form_LuongNhanVien
{
    public partial class LuongChiTiet_Admin : Form
    {
        DataConnection db = new DataConnection();
        public int IDCALAM { get; set; }
        public int IDNHANVIEN { get; set; }
        public LuongChiTiet_Admin()
        {
            InitializeComponent();
        }
        public LuongChiTiet_Admin(int id)
        {
            InitializeComponent();
            this.IDNHANVIEN = id;
        }
        public void LoadData()
        {
            DataTable salaryDataTable = new DataTable();

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                // Thay thế "YourSalaryDetailsQuery" bằng truy vấn SQL của bạn để lấy thông tin bảng lương
                string query = "select shift_id as 'Mã ca làm',start_time as 'Vô ca' , end_time as 'Kết ca',DATEDIFF(HOUR, start_time, end_time) AS 'Số giờ làm' from Shifts where employee_id = " + IDNHANVIEN;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // Đổ dữ liệu vào DataTable
                        adapter.Fill(salaryDataTable);
                    }
                }
            }

            // Hiển thị dữ liệu trong DataGridView
            dataGridView1.DataSource = salaryDataTable;
        }

        private void LuongChiTiet_Admin_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                IDCALAM = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox4.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
            }
        }

        private void btnVoca_Click(object sender, EventArgs e)
        {
            string voca = dateTimePicker1.Value.ToString();
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                string sql = "UPDATE Shifts SET start_time = @StartTime WHERE shift_id = @ShiftID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    // Assuming you have these parameters defined somewhere:
                    // int IDCALAM; // ID of the shift to update
                    // DateTime newStartTime = dateTimePicker1.Value;

                    command.Parameters.AddWithValue("@StartTime", voca);
                    command.Parameters.AddWithValue("@ShiftID", IDCALAM);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Update successful! Rows affected: " + rowsAffected);
                        LoadData();
                    }
                    else
                    {
                        // No rows were updated
                        MessageBox.Show("No rows were updated.");
                    }
                }
            }

        }

        private void btnKetca_Click(object sender, EventArgs e)
        {
            string ketca = dateTimePicker2.Value.ToString();
        }
    }
}
