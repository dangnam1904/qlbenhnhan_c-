using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBenhNhan
{
    public partial class fr_khoa : Form
    {
        DataTable khoa;
        public fr_khoa()
        {
            InitializeComponent();
        }

        private void fr_khoa_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            display_makhoa();
            display_tenkhoa();
        }

        private void LoadDataGridView()
        {
            String load = "select *from khoa";
           khoa = Functions.GetDataTable(load); //Đọc dữ liệu từ bảng
            Gridview_Khoa.DataSource = khoa; //Nguồn dữ liệu            
            Gridview_Khoa.Columns[0].HeaderText = "Mã Khoa";
            Gridview_Khoa.Columns[1].HeaderText = "Tên Khoa";
            Gridview_Khoa.Columns[2].HeaderText = "Mô tả ";
            Gridview_Khoa.Columns[3].HeaderText = "Ghi chú ";
            Gridview_Khoa.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            Gridview_Khoa.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp

        }

        private void Gridview_Khoa_Click(object sender, EventArgs e)
        {
            cb_makhoa.Text = Gridview_Khoa.CurrentRow.Cells["MaKhoa"].Value.ToString();
            cbo_tenkhoa.Text = Gridview_Khoa.CurrentRow.Cells["TenKhoa"].Value.ToString();
            rtxt_mota.Text = Gridview_Khoa.CurrentRow.Cells["MoTa"].Value.ToString();
            rtxt_ghichu.Text = Gridview_Khoa.CurrentRow.Cells["GhiChu"].Value.ToString();
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            ResetValues();
            cb_makhoa.Enabled = true;
            btn_add.Enabled = false;
        }

        private void ResetValues()
        {
            cb_makhoa.Text = "";
           cbo_tenkhoa.Text = "";
            rtxt_ghichu.Text = "";
            rtxt_mota.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (cb_makhoa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_makhoa.Focus();
                return;
            }

            if (cbo_tenkhoa.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               cbo_tenkhoa.Focus();
                return;
            }

            sql = "Select makhoa From khoa where makhoa=N'" + cb_makhoa.Text.Trim() + "'";

            if (Functions.CheckKey(sql) == false)
            {
                String sql_add = "insert into Khoa values('" + cb_makhoa.Text.Trim() + "',N'" +cbo_tenkhoa.Text.Trim() + "',N'" + rtxt_mota.Text.Trim() + "',N'" + rtxt_ghichu.Text.Trim() + "')";
                Functions.RunSql(sql_add);
            }
            else
            {

                MessageBox.Show("Mã khoa đã tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_makhoa.Focus();
                return;
            }
            btn_add.Enabled = true;
            LoadDataGridView(); //Nạp lại DataGridView
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
           
            if (khoa.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_makhoa.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá khoa " +cbo_tenkhoa.Text+ " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string check_fk = "select makhoa from bacsi where makhoa=N'"+cb_makhoa.Text.Trim()+"'";
                string check_fk_BNNN = "select makhoa from BN_Noitru where makhoa=N'" + cb_makhoa.Text.Trim() + "'";
                if( Functions.CheckKey(check_fk) || Functions.CheckKey(check_fk_BNNN))
                {
                    MessageBox.Show("Khoa này chứa người không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string sql = "DELETE KHOA WHERE MAKHOA  ='" + cb_makhoa.Text.Trim() + " '";
                    Functions.RunSql(sql);
                }
           
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            cb_makhoa.Enabled = false;
            btn_save.Enabled = false;


            if (khoa.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_makhoa.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sql_1 = "Update KHOA set tenkhoa=N'" +cbo_tenkhoa.Text.Trim() + "',MoTa=N'" + rtxt_mota.Text.Trim() + "',GhiChu=N'" + rtxt_ghichu.Text.Trim() + "' where MaKhoa='" + cb_makhoa.Text.Trim() + "' ";

            Functions.RunSql(sql_1);

            LoadDataGridView(); //Nạp lại DataGridView
        }

        private void bt_find_name_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "Select * from Khoa  where Tenkhoa like N'%" + txt_find_by_name.Text.Trim() + "%'";

            khoa = Functions.GetDataTable(sql);
            if (khoa.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + khoa.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_Khoa.DataSource = khoa;
        }

        private void btn_find_maHso_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_ma.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "Select * from Khoa  where makhoa like N'%" + txt_find_by_ma.Text.Trim() + "%'";

            khoa = Functions.GetDataTable(sql);
            if (khoa.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + khoa.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_Khoa.DataSource = khoa;
        }

        private void btn_sort_Click(object sender, EventArgs e)
        {

            string sort = "Select * from khoa  order by tenkhoa Asc'";
            Functions.RunSql(sort);
            LoadDataGridView();
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            btn_add.Enabled = true;
            btn_edit.Enabled = true;
            btn_save.Enabled = true;
            btn_delete.Enabled = true;
        }

        private void cb_maThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select TenThuoc from ToaThuoc where MaToaThuoc= '" + cb_makhoa.Text + "'";
           cbo_tenkhoa.Text = Functions.GetFieldValues(sql);
        }

        private void cb_makhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql= "select TenKhoa from Khoa where Makhoa=N'"+cb_makhoa.Text+"'";
            cbo_tenkhoa.Text = Functions.GetFieldValues(sql);
        }

        private void cbo_tenkhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select Makhoa from Khoa where tenkhoa=N'" + cbo_tenkhoa.Text + "'";
            cbo_tenkhoa.Text = Functions.GetFieldValues(sql);
        }
        private void display_makhoa()
        {

            string sql = " select* from Khoa";
            Functions.FillCombo(sql, cb_makhoa, "MaKhoa", "MaKhoa");
            cb_makhoa.SelectedIndex = -1;
        }
        private void display_tenkhoa()
        {
            string sql = " select* from Khoa";
            Functions.FillCombo(sql, cbo_tenkhoa, "MaKhoa", "TenKhoa");
            cbo_tenkhoa.SelectedIndex = -1;
        }
    }
}
