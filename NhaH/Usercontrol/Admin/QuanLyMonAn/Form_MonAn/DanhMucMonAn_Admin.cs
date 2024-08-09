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

namespace NhaH.Usercontrol.Admin.QuanLyMonAn.Form_MonAn
{
    public partial class DanhMucMonAn_Admin : DevExpress.XtraEditors.XtraForm
    {

        int IDDM;
        public DanhMucMonAn_Admin()
        {
            InitializeComponent();
            
        }
        public void LoadDATA()
        {
            dataGridView1.Rows.Clear();
            MenuDanhMucMethod menuDanhMuc = new MenuDanhMucMethod();
            List<DanhMuc> list = menuDanhMuc.Getds();
            foreach (DanhMuc item in list)
            {
                dataGridView1.Rows.Add(
                    item.CategoryId,
                    item.CategoryName
                    );
            }     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Dữ liệu đang để trống !");
            }
            else
            {
                MenuDanhMucMethod menuDanhMuc = new MenuDanhMucMethod();

                int check = menuDanhMuc.ThemDS(textBox1.Text);
                if (check != 0)
                {
                    MessageBox.Show("Thêm danh mục thành công!");
                   
                    LoadDATA();
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuDanhMucMethod menuDanhMuc = new MenuDanhMucMethod();
            int xoads = menuDanhMuc.XoaDS(IDDM);
            if (xoads!=0)
            {
                MessageBox.Show("Xóa danh mục thành công!");
                LoadDATA();
            }
            else
            {
                MessageBox.Show("Xóa danh mục không thành công!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                int selectedId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                IDDM = selectedId;
                textBox1.Text = selectedRow.Cells["Name"].Value.ToString();
            }
        }

        private void DanhMucMonAn_Admin_Load(object sender, EventArgs e)
        {
            LoadDATA();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Dữ liệu đang để trống !");
            }
            else
            {
                MenuDanhMucMethod menuDanhMuc = new MenuDanhMucMethod();

                int check = menuDanhMuc.Update(IDDM,textBox1.Text);
                if (check != 0)
                {
                    MessageBox.Show("Sửa danh mục thành công!");

                    LoadDATA();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}