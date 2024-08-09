using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaH.Usercontrol.Admin.QuanLyMonAn.Form_MonAn;
using NhaH.DAO;
using NhaH.DTO;
using System.Data.SqlClient;
using System.IO;

namespace NhaH.Usercontrol.Admin.QuanLyMonAn
{
    public partial class MonAn_Admin : UserControl
    {
        int IDMonAn;
        DataConnection db = new DataConnection();
        public MonAn_Admin()
        {
            InitializeComponent();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThemMonAn();
            pictureBox1.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XoaMonAn(IDMonAn);
            int madanhmuc = (int)comboBox2.SelectedValue;
            LoadMon(madanhmuc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DanhMucMonAn_Admin danhMucMonAn_Admin = new DanhMucMonAn_Admin();
            danhMucMonAn_Admin.ShowDialog();
            LoadDanhMuc();
            

        }

        public void LoadMon(int id)
        {
            MonAnMethod monAn = new MonAnMethod();
            List<MonAn> listmonan = monAn.LoadMon(id);
            dataGridView1.Rows.Clear();

            foreach (MonAn mon in listmonan)
            {
                // Tạo đường dẫn đầy đủ bằng cách kết hợp đường dẫn của ứng dụng với tên file
                string fullPath = Path.Combine(Application.StartupPath, mon.ImageMon);


                if (File.Exists(fullPath))
                {

                    Image img = Image.FromFile(fullPath);


                    dataGridView1.Rows.Add(
                        mon.IDmon,
                        mon.TenMonAn,
                        mon.GiaMon,
                        img,
                        mon.IDCategory
                    );
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }
        public void LoadMonAll()
        {
            MonAnMethod monAn = new MonAnMethod();
            List<MonAn> listmonan = monAn.LoadMonAll();
            dataGridView1.Rows.Clear();

            foreach (MonAn mon in listmonan)
            {
                // Tạo đường dẫn đầy đủ bằng cách kết hợp đường dẫn của ứng dụng với tên file
                string fullPath = Path.Combine(Application.StartupPath, mon.ImageMon);


                if (File.Exists(fullPath))
                {

                    Image img = Image.FromFile(fullPath);


                    dataGridView1.Rows.Add(
                        mon.IDmon,
                        mon.TenMonAn,
                        mon.GiaMon,
                        img,
                        mon.IDCategory
                    );
                }
                else
                {
                    MessageBox.Show("Lỗi");
                }
            }
        }
        public void LoadDanhMuc()
        {
            MonAnMethod monAn = new MonAnMethod();
            List<DanhMuc> categoryList = monAn.LoadDanhMuc();
            List<DanhMuc> categoryList2 = monAn.LoadDanhMuc();

            comboBox1.DataSource = categoryList;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryId";
            comboBox2.DataSource = categoryList2;
            comboBox2.DisplayMember = "CategoryName";
            comboBox2.ValueMember = "CategoryId";
        }
        public void ThemMonAn()
        {
            try
            {
                int madanhmuc = (int)comboBox1.SelectedValue;
                string tenmon = textBox1.Text;
                string giamon = textBox2.Text;

                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Convert the image to a byte array
                    //byte[] b = imgtobyte(pictureBox1.Image);

                    string query = "INSERT INTO Dishes (dish_name, category_id, Anh_Monan, price) VALUES (@dishName, @categoryId, @anhMonan, @price)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@dishName", tenmon);
                        cmd.Parameters.AddWithValue("@categoryId", madanhmuc);
                        cmd.Parameters.AddWithValue("@anhMonan", textBox3.Text); // Assuming Anh_Monan is a VARBINARY(MAX) column
                        cmd.Parameters.AddWithValue("@price", giamon);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Món ăn đã được thêm thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }
        public void XoaMonAn(int id)
        {
            MonAnMethod monAn = new MonAnMethod();
            int xoamon = monAn.Xoa(id);
            if (xoamon != 0)
            {
                MessageBox.Show("Món ăn đã được xóa thành công!");
            }
            else
            {
                MessageBox.Show("Xóa không thành công!");
            }
        }
        private void MonAn_Admin_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
            LoadMon(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int madanhmuc = (int)comboBox1.SelectedValue;
            LoadMon(madanhmuc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
                textBox3.Text = filePath;
             
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedId = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
                IDMonAn = selectedId;
                if (IDMonAn != 0)
                {
                    textBox1.Text = selectedRow.Cells["Column2"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["Column3"].Value.ToString();
                    MonAn monAn = getanh(selectedId);
                    string fullPath = Path.Combine(Application.StartupPath, monAn.ImageMon);
                    textBox3.Text = fullPath;
                    pictureBox1.Image = Image.FromFile(fullPath);
                }
                else
                {
                    textBox1.Text = null;
                    textBox2.Text = null;
         
                    pictureBox1.Image = null;
                }
                
            }
        }
        public MonAn getanh(int selectedId)
        {
            MonAn dish = new MonAn();
            using (SqlConnection connection = db.sqlstring())
            {
                string sql = "select Anh_Monan from Dishes where dish_id = " + selectedId;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dish.ImageMon = reader["Anh_Monan"].ToString();
                        }                      
                    }
                }

            }
            return dish;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateMonAn_Admin updateMonAn_Admin = new UpdateMonAn_Admin(IDMonAn);
            updateMonAn_Admin.ShowDialog();
            LoadMon(1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadMonAll();
        }
    }
}
