using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaH.Usercontrol.Admin.TrangChu.Form_BanAn;

namespace NhaH.Usercontrol.Admin.TrangChu
{
    public partial class BanAn_Admin : UserControl
    {
        public BanAn_Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tachban tachban = new Tachban();
            tachban.ShowDialog();
            
        }
    }
}
