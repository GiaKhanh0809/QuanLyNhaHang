using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using NhaH.Usercontrol.Admin.NhanVien.Form_LuongNhanVien;

namespace NhaH.Usercontrol.Admin
{
    public partial class LuongNhanVien_Admin : UserControl
    {
        public int IDNHANVIEN { get; set; }
        DataConnection db = new DataConnection();
        public LuongNhanVien_Admin()
        {
            InitializeComponent();
        }
        public void LoadDATA()
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("ChiTietLuongTheoCa", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Create a DataTable to store the results
                        DataTable dataTable = new DataTable();

                        // Use a SqlDataAdapter to fill the DataTable
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }

                        // Bind the DataTable to a DataGridView or any other control you want to display the results
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LuongNhanVien_Admin_Load(object sender, EventArgs e)
        {
            LoadDATA();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                IDNHANVIEN = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox4.Text = selectedRow.Cells[3].Value.ToString();
                textBox2.Text = Convert.ToDecimal(selectedRow.Cells[2].Value).ToString("#,##0");
                textBox3.Text = Convert.ToDecimal(selectedRow.Cells[4].Value).ToString("#,##0");





            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuongChiTiet_Admin luongNhanVien = new LuongChiTiet_Admin(IDNHANVIEN);
            luongNhanVien.ShowDialog();
            LoadDATA();
        }
    }
}
