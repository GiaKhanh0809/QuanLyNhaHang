using DevExpress.XtraBars;
using NhaH.Usercontrol.Admin.TrangChu;
using NhaH.Usercontrol.NhanVien;
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
    public partial class DashBoard_User : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public DashBoard_User()
        {
            InitializeComponent();
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            Order_Admin order_Admin = new Order_Admin();
            order_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(order_Admin);
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            BanAn_Admin banAn_Admin = new BanAn_Admin();
            banAn_Admin.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(banAn_Admin);
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            ThongTinNhanVien_User thongTinNhanVien = new ThongTinNhanVien_User();
            thongTinNhanVien.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(thongTinNhanVien);
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            XemLichNV_User xemLichNV_User = new XemLichNV_User();
            xemLichNV_User.Dock = DockStyle.Fill;

            // Xóa tất cả các điều khiển hiện tại trong fluentDesignFormContainer1
            fluentDesignFormContainer1.Controls.Clear();
            fluentDesignFormContainer1.Controls.Add(xemLichNV_User);
        }
    }
}
