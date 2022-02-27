using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyBenhNhan.ThongKe
{
    public partial class frTk_BN : Form
    {
        DataTable BenhNhan;
        public frTk_BN()
        {
            InitializeComponent();
        }

        private void frTk_BN_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            String sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN";
            BenhNhan = Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            Gridview_BN.DataSource = BenhNhan; //Nguồn dữ liệu            
            Gridview_BN.Columns[0].HeaderText = "Mã hồ sơ";
            Gridview_BN.Columns[1].HeaderText = " Họ tên";
            Gridview_BN.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Gridview_BN.Columns[2].HeaderText = "Ngày sinh";
            Gridview_BN.Columns[3].HeaderText = "Giới tính ";
            Gridview_BN.Columns[4].HeaderText = "Mã loại ";
            Gridview_BN.Columns[5].HeaderText = "TenLoai";
            Gridview_BN.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            Gridview_BN.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void bt_find_name_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN where TenBN like N'%" + txt_find_by_name.Text.Trim() + "%'";

            BenhNhan = Functions.GetDataTable(sql);
            if (BenhNhan.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + BenhNhan.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN.DataSource = BenhNhan;
        }

        private void btn_find_maHso_Click(object sender, EventArgs e)
        {

            if ((txt_find_by_ma.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN where MaHoSo like N'%" + txt_find_by_ma.Text.Trim() + "%'";

            BenhNhan = Functions.GetDataTable(sql);
            if (BenhNhan.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + BenhNhan.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN.DataSource = BenhNhan;
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = " select count(MaHoSo) from BenhNhan";
            txtSum.Visible = true;
            txtSum.Text = Functions.GetFieldValues(sql);

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                // Khởi động chương trình Excel
                COMExcel.Application exApp = new COMExcel.Application();
                COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
                COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
                COMExcel.Range exRange;
                string sql;
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            // Make the object visible.
            excelApp.Visible = true;

            // Create a new, empty workbook and add it to the collection returned
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template.
            // Because no argument is sent in this example, Add creates a new workbook.
            excelApp.Workbooks.Add();

            // This example uses a single workSheet.
            Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;
            DataTable dulieu; 
                exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
                exSheet = exBook.Worksheets[1];
                // Định dạng chung
                exRange = exSheet.Cells[1, 1];
                exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
               
                exRange.Range["C2:E2"].Value = "Danh sách bệnh nhân";
                // Biểu diễn thông tin chung của hóa đơn bán
             sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN";

            dulieu = Functions.GetDataTable(sql);
           exRange.Range["B6:G34"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hồ sơ";
            exRange.Range["C6:C6"].Value = "Họ tên";
            exRange.Range["C6:C6"].ColumnWidth = 20;
            exRange.Range["D6:D6"].Value = "Ngày sinh";
            exRange.Range["D6:D6"].ColumnWidth = 20;
            exRange.Range["E6:E6"].Value = "Giới tính";
            exRange.Range["F6:F6"].Value = "Mã loại";
            exRange.Range["G6:G6"].Value = "Tên Loại";
            exRange.Range["G6:G6"].ColumnWidth = 20;
           
            //int r = 1;
            //foreach (DataRow row in dulieu.Rows)
            //{
              
            //    //workSheet.Cells[row, "A"] = "ạdf";
            //    //workSheet.Cells[row, "B"] = "fgignf";
            //    //workSheet.Cells[row, "C"] = row["NgaySinh"];
            //    //workSheet.Cells[row, "D"] = row["GioiTinh"];
            //    //workSheet.Cells[row, "E"] = row[" MaLoaiBN"];
            //    //workSheet.Cells[row, "E"] = row[" TenLoai"];
            //   // exRange.Range["C8:C8"].Value = row["MaHoSo"];
            //}
            exRange.Range["B7:B7"].Value = dulieu.Rows[0][0].ToString();
            exRange.Range["C7:C7"].Value = dulieu.Rows[0][1].ToString();
            string a= dulieu.Rows[0][2].ToString();
            string sub_a = a.Substring(0, 10);
            exRange.Range["D7:D7"].Value = sub_a;
            exRange.Range["E7:E7"].Value = dulieu.Rows[0][3].ToString();
       
            exRange.Range["F7:F7"].Value = dulieu.Rows[0][4].ToString();
            exRange.Range["G7:G7"].Value = dulieu.Rows[0][5].ToString();
            exApp.Visible = true;
            
        }
    }
}
