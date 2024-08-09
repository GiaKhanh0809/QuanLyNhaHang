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

namespace NhaH.Usercontrol.Admin.NhanVien.Form_QLNhanVien
{
    public partial class AddNhanVien : Form
    {
        public AddNhanVien()
        {
            InitializeComponent();
        }
        public void Add()
        {
            try
            {
                string ngaysinh = dateTimePicker1.Value.ToString();
                NhanVienMethod nhanvien = new NhanVienMethod();
                int nhanVien = nhanvien.Add(textBox1.Text, textBox4.Text, textBox3.Text, textBox2.Text, ngaysinh, textBox6.Text, textBox5.Text);
                if (nhanVien != 0)
                {
                    MessageBox.Show("Thêm nhân viên thành công !");
                }
             
            }
            catch
            {
                MessageBox.Show("Thêm nhân không thành công !");
            }
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" ||
                textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                Add();
                this.Close();
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
