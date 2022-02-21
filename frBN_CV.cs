using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBenhNhan
{

    public partial class frBN_CV : Form
    {

        DataTable bn_cv;
        public frBN_CV()
        {
            InitializeComponent();
        }

        private void frBN_CV_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            display_maHoSo();
            display_maKhoa();
            display_maCV();
            display_TenBN();
            display_mabacsi();
            display_Tenbacsi();
        }

        private void display_mabacsi()
        {
            string sql = "select * from  BacSi";
            Functions.FillCombo(sql, cb_mabacsi, "MaBacSi", "MaBacSi");
            cb_mabacsi.SelectedIndex = -1;
        }

        private void display_Tenbacsi()
        {
            string sql = "select * from  BacSi";
            Functions.FillCombo(sql, cb_TenBacSi, "MaBacSi", "TenBacSi");
            cb_TenBacSi.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            String sql = " select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_cv.Ma_CV, bn_cv.ChuanDoanBenh,bn_cv.NgayChuyen,NoiChuyen, bn_cv.MaKhoa, bs.MaBacSi, bs.TenBacSi from BenhNhan bn inner join BN_CV bn_cv on bn.MaHoSo=bn_cv.MaHoSo inner join BacSi bs on bs.MaBacSi= bn_cv.MaBacSi";
            bn_cv = Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            Gridview_BN_CV.DataSource = bn_cv; //Nguồn dữ liệu            
            Gridview_BN_CV.Columns[0].HeaderText = "Mã hồ sơ";
            Gridview_BN_CV.Columns[1].HeaderText = " Họ tên";
           // Gridview_BN_CV.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Gridview_BN_CV.Columns[2].HeaderText = "Ngày sinh";
            Gridview_BN_CV.Columns[3].HeaderText = "Giới tính ";
            Gridview_BN_CV.Columns[4].HeaderText = "Mã chuyển viện ";
            Gridview_BN_CV.Columns[5].HeaderText = "Chuẩn đoán bệnh";
            Gridview_BN_CV.Columns[6].HeaderText = "Ngày chuyển";
            Gridview_BN_CV.Columns[7].HeaderText = "Nơi chuyển";
            Gridview_BN_CV.Columns[8].HeaderText = "Mã khoa";
            Gridview_BN_CV.Columns[9].HeaderText = "Mã bác sĩ";
            Gridview_BN_CV.Columns[10].HeaderText = "Tên bác sĩ";
            Gridview_BN_CV.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            Gridview_BN_CV.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp

        }

        /// hiển thị dữ liệu trong combobox
        private void display_maHoSo()
        {
            string sql = " select * from BenhNhan";
            Functions.FillCombo(sql, cb_maHS, "MaHoSo", "MaHoSo");
            cb_maHS.SelectedIndex = -1;
        }
        private void display_maKhoa()
        {
            string sql = " select * from Khoa";
            Functions.FillCombo(sql, cb_maKhoa, "MaKhoa", "MaKhoa");
            cb_maKhoa.SelectedIndex = -1;
        }
        private void display_maCV()
        {
            string sql = " select * from BN_CV";
            Functions.FillCombo(sql, cb_MaCV, "Ma_CV", "Ma_CV");
            cb_MaCV.SelectedIndex = -1;

        }
        private void display_TenBN()
        {
            string sql = " select * from BenhNhan";
            Functions.FillCombo(sql, cb_TenBn, "MaHoSo", "TenBN");
            cb_TenBn.SelectedIndex = -1;
        }

        private void Gridview_BN_CV_Click(object sender, EventArgs e)
        {
            if (bn_cv.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cb_maHS.Text = Gridview_BN_CV.CurrentRow.Cells["MaHoSo"].Value.ToString();
            cb_TenBn.Text = Gridview_BN_CV.CurrentRow.Cells["TenBN"].Value.ToString();
           date_ngaysinh.Value =  (DateTime) Gridview_BN_CV.CurrentRow.Cells["NgaySinh"].Value;

            cb_MaCV.Text = Gridview_BN_CV.CurrentRow.Cells["Ma_CV"].Value.ToString();
            txt_cdoanbenh.Text = Gridview_BN_CV.CurrentRow.Cells["ChuanDoanBenh"].Value.ToString();
            txt_noichuyen.Text = Gridview_BN_CV.CurrentRow.Cells["NoiChuyen"].Value.ToString();
            cb_maKhoa.Text = Gridview_BN_CV.CurrentRow.Cells["MaKhoa"].Value.ToString();
          
            cb_TenBacSi.Text = Gridview_BN_CV.CurrentRow.Cells["TenBacSi"].Value.ToString();
            cb_mabacsi.Text = Gridview_BN_CV.CurrentRow.Cells["MaBacSi"].Value.ToString();
           date_ngaychuyen.Value =(DateTime) Gridview_BN_CV.CurrentRow.Cells["NgayChuyen"].Value;

            string sex = Gridview_BN_CV.CurrentRow.Cells["Gioitinh"].Value.ToString();
            if (sex == "Nam")
            {
                checkbox_nam.Checked = true;
                checkbox_nu.Checked = false;
            }
            else
            {
                checkbox_nu.Checked = true;
                checkbox_nam.Checked = false;
            }

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Resetvalues();
            cb_maHS.Enabled = true;
            cb_MaCV.Enabled = true;
            cb_mabacsi.Enabled = true;
            btn_add.Enabled = false;
        }
        private void Resetvalues()
        {
            cb_maHS.Focus();
            cb_maHS.Text = "";
            cb_maKhoa.Text = "";
            cb_MaCV.Text = "";
            cb_TenBn.Text = "";
            cb_mabacsi.Text = "";
            txt_noichuyen.Text = "";
            cb_TenBacSi.Text = "";
            txt_cdoanbenh.Text = "";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            btn_edit.Enabled = false;
            string sql; //Lưu lệnh sql
            if (cb_maHS.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hồ sơ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maHS.Focus();
                return;
            }
            if (cb_MaCV.Text.Trim().Length == 0) 

            {
                MessageBox.Show("Bạn phải nhập mã chuyện viện", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_MaCV.Focus();
                return;
            }
            if (cb_maKhoa.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập mã khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maKhoa.Focus();
                return;
            }
            if (cb_TenBn.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập tên bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_TenBn.Focus();
                return;
            }
            sql = "Select Ma_CV From BN_CV where Ma_CV=N'" + cb_MaCV.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã chuyển viện đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_MaCV.Focus();
                return;
            }

            sql = "Select MaHoSo From BenhNhan where MaHoSo=N'" + cb_maHS.Text.Trim() + "'";
            if (Functions.CheckKey(sql) == false)
            {
                String maLoaiBN = "BN_CV";
                if (checkbox_nam.Checked == true)
                {
                    string gioitinh = "Nam";
                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" + date_ngaysinh.Value + "',N'" + gioitinh + "', '" + maLoaiBN + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
                else if (checkbox_nu.Checked == true)
                {
                    string gioitinh = "Nữ";
                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" + date_ngaysinh.Value + "',N'" + gioitinh + "', '" + maLoaiBN + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
            }
            else
            {
                sql = "update benhnhan set mahoso='"+cb_maHS.Text.Trim()+"', TenBN= N'"+cb_TenBn.Text.Trim()+"',NgaySinh ='"+date_ngaysinh.Value+ "', MaLoaiBN='BN_CV' where mahoso='"+cb_maHS.Text.Trim()+"'";
                Functions.RunSql(sql);
                String check_maBs = "select MaBacSi from BacSi where MaBacSi='" + cb_mabacsi.Text.Trim() + "' ";
                if (Functions.CheckKey(check_maBs) == false)
                {
                    //MessageBox.Show("Mã bác sĩ không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //cb_mabacsi.Focus();
                    //return;
                    sql = "insert into BacSi (MaBacSi,TenBacSi,MaKhoa) values('"+cb_mabacsi.Text.Trim()+"', N'"+cb_TenBacSi.Text.Trim()+"','"+cb_maKhoa.Text.Trim()+"')";
                }
                string maLoaiBN = "BN_CV";
                string add_BN_Cv = "insert into  BN_CV values('" + cb_MaCV.Text.Trim() + "',N'" + txt_cdoanbenh.Text.Trim() + "','" +date_ngaychuyen.Value + "',N'"+txt_noichuyen.Text.Trim()+"', '" + cb_maKhoa.Text.Trim() + "','" + cb_maHS.Text.Trim() + "','" + maLoaiBN + "','" + cb_mabacsi.Text.Trim() + "')";
                Functions.RunSql(add_BN_Cv);
            }
            LoadDataGridView(); //Nạp lại DataGridView
            btn_add.Enabled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            if (bn_cv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_MaCV.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá " +cb_TenBn.Text+ " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE BN_CV WHERE Ma_CV='" + cb_MaCV.Text.Trim() + " '";
                Functions.RunSql(sql);
                LoadDataGridView();
                Resetvalues();
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
          
            cb_MaCV.Enabled = false;
           

            if (bn_cv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_maHS.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string gioitinh;

            if (checkbox_nam.Checked == true)
            {
                gioitinh = "Nam";
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" +date_ngaysinh.Value + "', GioiTinh=N'" + gioitinh + "' Where MaHoSo='" + cb_maHS.Text.Trim() + "' ";
                Functions.RunSql(sql_1);
            }
            if (checkbox_nu.Checked == true)
            {
                gioitinh = "Nữ";
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" + date_ngaysinh.Value + "', GioiTinh=N'" + gioitinh + "' Where MaHoSo='" + cb_maHS.Text.Trim() + "'  ";
                Functions.RunSql(sql_1);
            }

            string edit_Bsi = "update BacSi set TenBacSi=N'" + cb_TenBacSi.Text.Trim() + "' where MaBacSi ='" + cb_mabacsi.Text.Trim() + "' ";
            Functions.RunSql(edit_Bsi);
            String edit_BN_CV = "update BN_CV set ChuanDoanBenh=N'"+txt_cdoanbenh.Text.Trim()+"', NgayChuyen='"+date_ngaychuyen.Value+"', NoiChuyen=N'"+txt_noichuyen.Text.Trim()+"', MaKhoa= '"+cb_maKhoa.Text.Trim()+"', MaBacSi='"+cb_mabacsi.Text.Trim()+"' where Ma_CV= '"+cb_MaCV.Text.Trim()+"' ";
            Functions.RunSql(edit_BN_CV);
            LoadDataGridView(); //Nạp lại DataGridView
            btn_add.Enabled = true;
        }

        private void bt_find_name_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_cv.Ma_CV, bn_cv.ChuanDoanBenh,bn_cv.NgayChuyen, bn_cv.NoiChuyen, bn_cv.MaKhoa, bs.MaBacSi, bs.TenBacSi from BenhNhan bn inner join BN_CV bn_cv on bn.MaHoSo=bn_cv.MaHoSo inner join BacSi bs on bs.MaBacSi= bn_cv.MaBacSi where TenBN like N'%" + txt_find_by_name.Text.Trim() + "%'";

            bn_cv = Functions.GetDataTable(sql);
            if (bn_cv.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + bn_cv.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN_CV.DataSource = bn_cv;
        }

        private void btn_find_maHso_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_ma.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_cv.Ma_CV, bn_cv.ChuanDoanBenh,bn_cv.NgayChuyen, bn_cv.NoiChuyen, bn_cv.MaKhoa, bs.MaBacSi, bs.TenBacSi from BenhNhan bn inner join BN_CV bn_cv on bn.MaHoSo=bn_cv.MaHoSo inner join BacSi bs on bs.MaBacSi= bn_cv.MaBacSi where bn.MaHoSo like N'%" + txt_find_by_ma.Text.Trim() + "%'";

            bn_cv = Functions.GetDataTable(sql);
            if (bn_cv.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + bn_cv.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN_CV.DataSource = bn_cv;
        }

        private void btn_sort_Click(object sender, EventArgs e)
        {
            String sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_cv.Ma_CV, bn_cv.ChuanDoanBenh,bn_cv.NgayChuyen, bn_cv.NoiChuyen, bn_cv.MaKhoa, bs.MaBacSi, bs.TenBacSi from BenhNhan bn inner join BN_CV bn_cv on bn.MaHoSo=bn_cv.MaHoSo inner join BacSi bs on bs.MaBacSi= bn_cv.MaBacSi order by TenBN Asc";
            Functions.RunSql(sql);
            LoadDataGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            btn_add.Enabled = true;
            btn_delete.Enabled = true;
            btn_save.Enabled = true;
            btn_edit.Enabled = true;
        }

        private void cb_maHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "Select TenBN from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            cb_TenBn.Text = Functions.GetFieldValues(str);
            string str1 = "Select Ma_CV from BenhNhan inner join BN_CV on BenhNhan.MaHoSo = BN_CV.MaHoSo where BenhNhan.MaHoSo = N'" + cb_maHS.Text + "'";
            cb_MaCV.Text = Functions.GetFieldValues(str1);
            string st3 = "select GioiTinh from BenhNhan where MaHoSo ='" + cb_maHS.Text + "'";
            string gioitinh = Functions.GetFieldValues(st3);
            if(gioitinh =="Nam")
            {
                checkbox_nam.Checked = true;
                checkbox_nu.Checked = false;
            }
            else
            {
                checkbox_nam.Checked = false;
                checkbox_nu.Checked = true;
            }
            str = "Select NgaySinh from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime( date_ngaysinh.Text);
        }

        private void cb_TenBn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "Select MaHoSo from BenhNhan where TenBN = N'" + cb_TenBn.Text + "'";
            cb_maHS.Text = Functions.GetFieldValues(str);
            string str1 = "Select Ma_CV from BenhNhan inner join BN_CV on BenhNhan.MaHoSo = BN_CV.MaHoSo where BenhNhan.TenBN = N'" + cb_TenBn.Text + "'";
            cb_MaCV.Text = Functions.GetFieldValues(str1);
            string st3 = "select GioiTinh from BenhNhan where TenBN=N'" + cb_TenBn.Text + "'";
            string gioitinh = Functions.GetFieldValues(st3);
            if (gioitinh == "Nam")
            {
                checkbox_nam.Checked = true;
                checkbox_nu.Checked = false;
            }
            else
            {
                checkbox_nam.Checked = false;
                checkbox_nu.Checked = true;
            }
            str = "Select NgaySinh from BenhNhan where TenBN = N'" + cb_TenBn.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime(date_ngaysinh.Text);
        }
        private void cb_maKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "SELECT TenBacSi FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where Khoa.MaKhoa='" + cb_maKhoa.Text + "'";
            cb_TenBacSi.Text = Functions.GetFieldValues(str);
            string str1 = "SELECT MaBacSi FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where Khoa.MaKhoa='" + cb_maKhoa.Text + "'";
            cb_mabacsi.Text = Functions.GetFieldValues(str1);

        }
        private void cb_mabacsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "SELECT TenBacSi FROM BacSi where MaBacSi='" + cb_mabacsi.Text + "'";
            cb_TenBacSi.Text = Functions.GetFieldValues(str);
            string str1 = "SELECT MaKhoa FROM BacSi where MaBacSi='" + cb_mabacsi.Text + "'";
            cb_maKhoa.Text = Functions.GetFieldValues(str1);
        }

        private void cb_TenBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select BacSi.MaKhoa from BacSi join Khoa on BacSi.MaKhoa= Khoa.MaKhoa where TenBacSi=N'" + cb_TenBacSi.Text + "' ";
            cb_maKhoa.Text = Functions.GetFieldValues(str);
            String str1 = "select MaBacSi from BacSi where TenBacSi=N'" + cb_TenBacSi.Text + "' ";
            cb_mabacsi.Text = Functions.GetFieldValues(str1);
        }

        private void cb_MaCV_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
     
    }
}
