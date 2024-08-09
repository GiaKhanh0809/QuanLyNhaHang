using DevExpress.XtraBars;
using NhaH.Usercontrol.Admin;
using NhaH.Usercontrol.Admin.QuanLyMonAn;
using NhaH.Usercontrol.Admin.TrangChu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NhaH
{
    public partial class DashBoard_Admin : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public DashBoard_Admin()
        {
            InitializeComponent();
            Order_Admin order_Admin = new Order_Admin();
            order_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(order_Admin);
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement5_Click_1(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            QLNhanVien qLNhanVien = new QLNhanVien();
            qLNhanVien.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();

            // Thêm UserControl vào fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Add(qLNhanVien);
        }

        private void DashBoad_Load(object sender, EventArgs e)
        {

        }

        private void DashBoad_Resize(object sender, EventArgs e)
        {
           
        }

        private void accordionControlElement12_Click(object sender, EventArgs e)
        {
            LichNhanVien_Admin lichNhanVien_Admin = new LichNhanVien_Admin();
            lichNhanVien_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(lichNhanVien_Admin);
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            LuongNhanVien_Admin luongNhanVien_Admin = new LuongNhanVien_Admin();
            luongNhanVien_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(luongNhanVien_Admin);
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            MonAn_Admin monAn_Admin = new MonAn_Admin();
            monAn_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(monAn_Admin);
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            Order_Admin order_Admin = new Order_Admin();
            order_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(order_Admin);
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            BanAn_Admin banAn_Admin = new BanAn_Admin();
            banAn_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(banAn_Admin);
        }
    }
}
