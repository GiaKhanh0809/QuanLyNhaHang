using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NhaH.DTO;
using NhaH.DAO;
using System.IO;
using System.Data.SqlClient;

namespace NhaH.Usercontrol.Admin.TrangChu
{
    public partial class Order_Admin : UserControl
    {
        DataConnection db = new DataConnection();
        public int idmonan { get; set; }
        public Order_Admin()
        {
            InitializeComponent();
            LoadMon(1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Order_Admin_Load(object sender, EventArgs e)
        {
            LoadBan();
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            CalculateAndDisplayTotal();
        }
        public void LoadMon(int id)
        {
            MonAnMethod monAn = new MonAnMethod();
            List<MonAn> listmonan = monAn.LoadMon(id);

            flpMonAn.WrapContents = true;
         
            flpMonAn.Controls.Clear();
            foreach (MonAn item in listmonan)
            {
                Button btn = new Button();
                btn.Width = 100;
                btn.Height = 120;


                if (item.ImageMon != null)
                {
                    string fullPath = Path.Combine(Application.StartupPath, item.ImageMon);
                    if (File.Exists(fullPath))
                    {
                        Image img = Image.FromFile(fullPath);
                        int newWidth = 80;
                        int newHeight = 80;
                        Image resizedImg = new Bitmap(img, newWidth, newHeight);

                        btn.Image = resizedImg;
                        //btn.Image = img;
                    }
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                }

                btn.Text = item.TenMonAn + Environment.NewLine + item.GiaMon;
                btn.Click += Btn_Click;
                btn.Tag = item;
                flpMonAn.Controls.Add(btn);
            }
        }
        public void LoadMonNuoc(int id)
        {
            MonAnMethod monAn = new MonAnMethod();
            List<MonAn> listmonan = monAn.LoadMon(id);

            flpMonAn.WrapContents = false;

            flpMonAn.Controls.Clear();
            foreach (MonAn item in listmonan)
            {
                Button btn = new Button();
                btn.Width = 100;
                btn.Height = 120;


                if (item.ImageMon != null)
                {
                    string fullPath = Path.Combine(Application.StartupPath, item.ImageMon);
                    if (File.Exists(fullPath))
                    {
                        Image img = Image.FromFile(fullPath);
                        int newWidth = 80;
                        int newHeight = 80;
                        Image resizedImg = new Bitmap(img, newWidth, newHeight);

                        btn.Image = resizedImg;
                    }
                    btn.ImageAlign = ContentAlignment.TopCenter;
                    btn.TextImageRelation = TextImageRelation.ImageAboveText;
                }

                btn.Text = item.TenMonAn + Environment.NewLine + item.GiaMon;
                btn.Click += Btn_Click;
                btn.Tag = item;
                flpMonAn.Controls.Add(btn);
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            idmonan = ((sender as Button).Tag as MonAn).IDmon;
            //lsvBill.Tag = (sender as Button).Tag;
            //ShowBill(tableID);
        }

        public void LoadBan()
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Use a SqlCommand to retrieve data from the TableStatus table
                    using (SqlCommand cmd = new SqlCommand("SELECT table_number, nameTable FROM TableStatus WHERE tableStatus = N'Trống'", connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        // Set the ComboBox properties
                        comboBox1.DisplayMember = "nameTable"; // Display this column in the ComboBox
                        comboBox1.ValueMember = "table_number"; // Use this column as the value for each item
                        comboBox1.DataSource = table; // Set the data source to the DataTable
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int id = 1;
            LoadMon(id);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = 2;
            LoadMonNuoc(id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = 3;
            LoadMonNuoc(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int mamon = idmonan;
            try
            {
                MonAn monAn = LoadMonByID(mamon);

                if (monAn != null)
                {

                    List<GioHang> gioHangList = new List<GioHang>();

                    int soLuong = 1;

                    bool found = false;
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (item.Text == monAn.IDmon.ToString())
                        {
                            // If the item already exists, update its quantity
                            int currentQuantity = int.Parse(item.SubItems[2].Text);
                            currentQuantity++;
                            item.SubItems[2].Text = currentQuantity.ToString();

                            // Update total price
                            double giaMon = double.Parse(item.SubItems[3].Text);
                            double thanhTien = currentQuantity * giaMon;
                            item.SubItems[4].Text = thanhTien.ToString();

                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {

                        // Create a GioHang object and add it to the list
                        GioHang gioHangItem = new GioHang
                        {
                            IDmon = monAn.IDmon,
                            TenMonAn = monAn.TenMonAn,
                            SoLuong = soLuong,
                            GiaMon = monAn.GiaMon,
                            ThanhTien = soLuong * monAn.GiaMon
                        };

                        gioHangList.Add(gioHangItem);

                        // Assuming you have a ListView named listViewGioHang
                        foreach (GioHang gioHang in gioHangList)
                        {
                            //
                            // Add the GioHang item to the ListView
                            ListViewItem listViewItem = new ListViewItem(gioHang.IDmon.ToString());
                            listViewItem.SubItems.Add(gioHang.TenMonAn);
                            listViewItem.SubItems.Add(gioHang.SoLuong.ToString());
                            listViewItem.SubItems.Add(gioHang.GiaMon.ToString());
                            listViewItem.SubItems.Add(gioHang.ThanhTien.ToString());

                            listView1.Items.Add(listViewItem);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                throw;
            }
            CalculateAndDisplayTotal();
        }
        public MonAn LoadMonByID(int mamon)
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Use a SqlCommand to retrieve data from the Dishes table
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Dishes WHERE dish_id = @DishId", connection))
                    {
                        // Use a SqlParameter to prevent SQL injection
                        cmd.Parameters.AddWithValue("@DishId", mamon);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        MonAn dish = null;
                        if (table.Rows.Count > 0)
                        {
                            dish = new MonAn
                            {
                                IDmon = mamon,
                                TenMonAn = table.Rows[0]["dish_name"].ToString(),
                                IDCategory = Convert.ToInt32(table.Rows[0]["category_id"]),
                                ImageMon = table.Rows[0]["Anh_Monan"].ToString(),
                                GiaMon = Convert.ToInt32(table.Rows[0]["price"])
                            };

                        }
                        return dish;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Retrieve the selected item
                ListViewItem selecteditem = listView1.SelectedItems[0];

                // Assuming you have a List<GioHang> named gioHangList
                string mamon = selecteditem.SubItems[0].Text;

                //// Find and remove the corresponding item from gioHangList
                //List<GioHang> gioHangList = new List<GioHang>();
                //GioHang gioHangItem = gioHangList.Find(item => item.IDmon == tenMonAn);
                //if (gioHangItem != null)
                //{
                //    gioHangList.Remove(gioHangItem);
                //}

                //// Remove the selected item from the ListView
                listView1.Items.Remove(selecteditem);
            }
            CalculateAndDisplayTotal();
        }
        private void CalculateAndDisplayTotal()
        {
            // Assuming you have a TextBox named textBox1 for displaying the total amount
            double totalAmount = 0.0;

            foreach (ListViewItem item in listView1.Items)
            {
                // Assuming column index 3 corresponds to the "ThanhTien" column
                if (double.TryParse(item.SubItems[4].Text, out double thanhTien))
                {
                    totalAmount += thanhTien;
                }
            }

            textBox1.Text = totalAmount.ToString("$#,0");
        }
        private int CheckIfDataExists()
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Sử dụng SqlCommand để thực hiện truy vấn
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 order_id FROM Orders ORDER BY order_id DESC", connection))
                    {
                        // ExecuteScalar để lấy giá trị đầu tiên của kết quả
                        object result = cmd.ExecuteScalar();

                        // Kiểm tra xem có giá trị hay không
                        if (result != null && result != DBNull.Value)
                        {
                            // Convert giá trị sang kiểu int
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            // Không có dữ liệu, trả về 0
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Trạng thái lỗi
            }
        }
        private int CheckIfDataChiTIET()
        {
            try
            {
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Sử dụng SqlCommand để thực hiện truy vấn
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 order_detail_id FROM OrderDetails ORDER BY order_detail_id DESC", connection))
                    {
                        // ExecuteScalar để lấy giá trị đầu tiên của kết quả
                        object result = cmd.ExecuteScalar();

                        // Kiểm tra xem có giá trị hay không
                        if (result != null && result != DBNull.Value)
                        {
                            // Convert giá trị sang kiểu int
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            // Không có dữ liệu, trả về 0
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return -1; // Trạng thái lỗi
            }
        }
        private int Random()
        {
            int i = CheckIfDataExists();
            if (i==0)
            {
                i = 1;
                return i;
            }
            else
            {
                i++;
                return i;
            }

        }
        private int RandomCHITIET()
        {
            int i = CheckIfDataChiTIET();
            if (i == 0)
            {
                i = 1;
                return i;
            }
            else
            {
                i++;
                return i;
            }

        }
        private int TinhTongTien()
        {
            int totalAmount = 0;

            // Lặp qua từng ListViewItem để tính tổng số tiền
            foreach (ListViewItem item in listView1.Items)
            {
                int soLuong = int.Parse(item.SubItems[2].Text); // Giả sử cột thứ hai là số lượng
                int giaMon = int.Parse(item.SubItems[3].Text); // Giả sử cột thứ ba là giá món

                // Tổng số tiền cho mỗi món = Số lượng * Giá món
                totalAmount += soLuong * giaMon;
            }

            return totalAmount;
        }
        private void ThemDonHang(int maban , int manhanvien , int orderID)
        {
            try
            {
               

                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Thêm thông tin đơn hàng vào bảng Orders
                    string insertOrderQuery = "INSERT INTO Orders (order_id, table_number, order_date, employee_id, total_amount, status) VALUES (@OrderID, @TableNumber, GETDATE(), @EmployeeID, @TotalAmount, @Status)";

                    using (SqlCommand insertOrderCommand = new SqlCommand(insertOrderQuery, connection))
                    {
                        // Thay thế các tham số bằng giá trị thực tế
                        insertOrderCommand.Parameters.AddWithValue("@OrderID", orderID);
                        insertOrderCommand.Parameters.AddWithValue("@TableNumber", maban);
                        insertOrderCommand.Parameters.AddWithValue("@EmployeeID", manhanvien);

                        // Tính tổng số tiền dựa trên dữ liệu trong ListView
                        int totalAmount = TinhTongTien();
                        insertOrderCommand.Parameters.AddWithValue("@TotalAmount", totalAmount);

                        // Tình trạng đơn hàng (bạn có thể thiết lập giá trị mặc định hoặc xử lý tùy thuộc vào yêu cầu của bạn)
                        insertOrderCommand.Parameters.AddWithValue("@Status", "Đang xử lý");

                        // Thực hiện truy vấn
                        insertOrderCommand.ExecuteNonQuery();
                    }

                    // Thêm thông tin chi tiết đơn hàng vào bảng OrderDetails (nếu cần)
                    foreach (ListViewItem item in listView1.Items)
                    {
                        int idchitiet = RandomCHITIET();
                        int idmonan = Convert.ToInt32(item.SubItems[0].Text); // Giả sử cột đầu tiên là tên món ăn
                        int soLuong = int.Parse(item.SubItems[2].Text);
                        int gia = int.Parse(item.SubItems[3].Text); // Giả sử cột thứ hai là số lượng



                        string insertOrderDetailsQuery = "INSERT INTO [dbo].[OrderDetails] ([order_detail_id] ,[order_id] ,[dish_id] ,[quantity] ,[priceDish])" +
                    "VALUES (@orderchitiet,@OrderID, @idMonAn, @SoLuong,@gia)";
                        using (SqlCommand insertOrderDetailsCommand = new SqlCommand(insertOrderDetailsQuery, connection))
                        {
                            // Thay thế các tham số bằng giá trị thực tế
                            insertOrderDetailsCommand.Parameters.AddWithValue("@orderchitiet", idchitiet);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@OrderID", orderID);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@idMonAn", idmonan);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@SoLuong", soLuong);
                            insertOrderDetailsCommand.Parameters.AddWithValue("@gia", gia);

                            // Thực hiện truy vấn
                            insertOrderDetailsCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Đơn hàng đã được đặt thành công!");
                    listView1.Items.Clear();
                    CalculateAndDisplayTotal();
                    UpdateStatus(maban);
                    LoadBan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int maban = int.Parse(comboBox1.SelectedValue.ToString());
            int manhanvien = Session.UserType;
            int orderID = Random();
            ThemDonHang(maban, manhanvien, orderID);
        }
        public void UpdateStatus(int maban)
        {
            try
            {
                // Assuming you have a SqlConnection named "connection" for your database connection
                using (SqlConnection connection = db.sqlstring())
                {
                    connection.Open();

                    // Assuming you have a TableStatus table with a column named "table_number" and "tableStatus"
                    string updateStatusQuery = "UPDATE TableStatus SET tableStatus = @NewStatus WHERE table_number = @MaBan";

                    using (SqlCommand updateStatusCommand = new SqlCommand(updateStatusQuery, connection))
                    {
                        // Assuming you have a new status value to set
                        string newStatus = "Có khách"; // Replace with your actual new status value

                        // Replace parameters with actual values
                        updateStatusCommand.Parameters.AddWithValue("@NewStatus", newStatus);
                        updateStatusCommand.Parameters.AddWithValue("@MaBan", maban);

                        // Execute the update command
                        updateStatusCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
