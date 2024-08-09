using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using NhaH.DAO;
using NhaH.DTO;
using NhaH.Usercontrol.Admin.NhanVien.Form_QLNhanVien;

namespace NhaH
{
    public partial class QLNhanVien : DevExpress.XtraEditors.XtraUserControl
    {
        public int IDNHANVIEN { get; set; }
        DataConnection db = new DataConnection();
        public QLNhanVien()
        {
            InitializeComponent();
        }
        public void LoadDATA()
        {
            NhanVienMethod nhanvien = new NhanVienMethod();
            List<NhanVienModel> list = nhanvien.getds();
            dataGridView1.Rows.Clear();
            foreach (NhanVienModel mon in list)
            {
                dataGridView1.Rows.Add(
                    mon.ID,
                    mon.TenNV,
                    mon.NgaySinh,
                     mon.phone,
                    mon.DiaChi,
                    mon.NgayVaoLam
                   
                    
                );

            }
        }

        private void QLNhanVien_Load(object sender, EventArgs e)
        {
            LoadDATA();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                IDNHANVIEN = Convert.ToInt32(selectedRow.Cells["Column1"].Value.ToString());
                textBox1.Text = selectedRow.Cells["Column2"].Value.ToString();
                textBox2.Text = selectedRow.Cells["Column7"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Column4"].Value.ToString();
                textBox4.Text = selectedRow.Cells["Column3"].Value.ToString();
                textBox5.Text = selectedRow.Cells["Column6"].Value.ToString();

                

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AddNhanVien nhanVien = new AddNhanVien();
            nhanVien.ShowDialog();
            LoadDATA();
        }

      

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateNhanVien nhanVien = new UpdateNhanVien(IDNHANVIEN);
            nhanVien.ShowDialog();
            LoadDATA();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            NhanVienMethod nhanVien = new NhanVienMethod();
            int xoa = nhanVien.Delete(IDNHANVIEN);
            if (xoa != 0 )
            {
                MessageBox.Show("Xóa thành công !");
                LoadDATA();
            }
            else
            {
                MessageBox.Show("Xóa không thành công !");
            }
        }
    }
}
