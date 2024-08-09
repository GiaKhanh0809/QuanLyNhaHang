using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NhaH.DAO;
using NhaH.DTO;
using System.Data.SqlClient;

namespace NhaH.Usercontrol.Admin.QuanLyMonAn.Form_MonAn
{
    public partial class UpdateMonAn_Admin : DevExpress.XtraEditors.XtraForm
    {
        DataConnection db = new DataConnection();
        DataTable dt = new DataTable();
        public int IdMonAn { get; set; }
        public UpdateMonAn_Admin()
        {
            InitializeComponent();
        }
        public UpdateMonAn_Admin(int id)
        {
            InitializeComponent();
            this.IdMonAn = id;
        }
        public void LoadDanhMuc()
        {
            MonAnMethod monAn = new MonAnMethod();
            List<DanhMuc> categoryList = monAn.LoadDanhMuc();
            

            comboBox1.DataSource = categoryList;
            comboBox1.DisplayMember = "CategoryName";
            comboBox1.ValueMember = "CategoryId";
           
        }

        private void UpdateMonAn_Admin_Load(object sender, EventArgs e)
        {
            LoadDanhMuc();
            LoadData(IdMonAn);
        }
        public int Update(int id)
        {
            int madanhmuc = (int)comboBox1.SelectedValue;
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "update Dishes set dish_name= N'"+textBox1.Text+"', category_id = '"+madanhmuc+"', Anh_Monan = '"+textBox3.Text+"' , price = '"+ textBox2.Text+"' where dish_id = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    
                    int rowsAffected = cmd.ExecuteNonQuery();
                    connection.Close();

                    return rowsAffected;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(filePath);
                textBox3.Text = filePath;

            }
        }
        public void LoadData(int id)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                string sql = "select * from Dishes where [dish_id] = " + id;

                using (SqlDataAdapter cmd = new SqlDataAdapter(sql, connection))
                {
                    
                    cmd.Fill(dt);
                    cmd.Dispose();
                    
                }
            }
            if (dt.Rows.Count > 0)
            {
                
                textBox1.Text = dt.Rows[0]["dish_name"].ToString();
                textBox3.Text = dt.Rows[0]["Anh_Monan"].ToString();
                textBox2.Text = dt.Rows[0]["price"].ToString();
                pictureBox1.Image = Image.FromFile(textBox3.Text);

            }
            else
            {
                
                MessageBox.Show("Không có dữ liệu cho ID đã cho.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int updateM = Update(IdMonAn);
            if (updateM != 0)
            {
                MessageBox.Show("Cập nhật thành công !");
            }
            else
            {
                MessageBox.Show("Lỗi !");
            }
        }
    }
}