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

namespace NhaH.Usercontrol.Admin.TrangChu
{
    public partial class DoanhThu : UserControl
    {
        DataConnection db = new DataConnection();
        public DoanhThu()
        {
            InitializeComponent();
        }
        private void LoadDefaultReport()
        {
            DoanhThuRpt rpt = new DoanhThuRpt();
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = false;
            crystalReportViewer1.Refresh();
        }
        private void LoadReportByDateRange(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("GetRevenueByDateRange", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    // Use SqlDataAdapter to fill a DataTable with the result set
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Pass the DataTable to the report
                    DoanhThuRpt rpt = new DoanhThuRpt();
                    rpt.SetDataSource(dataTable);

                    // Set the report source and refresh the CrystalReportViewer
                    crystalReportViewer1.ReportSource = rpt;
                    crystalReportViewer1.Refresh();
                }
            }
        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            LoadDefaultReport();
        }
    }
}
