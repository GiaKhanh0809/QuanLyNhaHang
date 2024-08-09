using NhaH.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaH.DTO;
using NhaH.Usercontrol.Admin.NhanVien.Form_QLNhanVien;
namespace NhaH.Usercontrol.Admin.NhanVien.Form_QLNhanVien
{
    public partial class UpdateNhanVien : Form
    {
        public int IDnv { get; set; }
        public UpdateNhanVien()
        {
            InitializeComponent();
        }
        public UpdateNhanVien(int id)
        {
            InitializeComponent();
            this.IDnv = id;
        }
        public void LoadDATA()
        {
            NhanVienMethod nhanvien = new NhanVienMethod();
            NhanVienModel nhanVien = nhanvien.getitem(IDnv);
            textBox1.Text = nhanVien.TenNV;
            textBox2.Text = nhanVien.phone;
            textBox3.Text = nhanVien.DiaChi;
            textBox7.Text = Convert.ToString(nhanVien.NgaySinh);
            textBox4.Text = Convert.ToString(nhanVien.Luong);
            textBox5.Text = nhanVien.Username;
            textBox6.Text = nhanVien.password;

        }
        public void Update()
        {
            string ngaysinh = dateTimePicker1.Value.ToString();
            NhanVienMethod nhanvien = new NhanVienMethod();
            int nhanVien = nhanvien.update(IDnv,textBox1.Text,textBox3.Text,textBox4.Text,textBox2.Text,ngaysinh,textBox5.Text,textBox6.Text);
            if (nhanVien != 0)
            {
                MessageBox.Show("Cập nhật thành công !");

            }
            else
            {

                MessageBox.Show("Cập nhật không thành công !");
            }

        }
       
        private void UpdateNhanVien_Load(object sender, EventArgs e)
        {
            LoadDATA();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Update();
            this.Close();
        }
    }
}
