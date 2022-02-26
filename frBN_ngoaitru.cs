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
    public partial class frBN_ngoaitru : Form
    {
        DataTable bn_ngoaitru;
        public frBN_ngoaitru()
        {
            InitializeComponent();
        }

        private void frBN_ngoaitru_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            display_maHoSo();
            display_maKhoa();
            display_maNgoaitru();
            display_TenBN();
            display_TenbacSi();
            display_MaBacsi();
            display_mathuoc();
            display_Tenthuoc();


        }

        private void display_mathuoc()
        {
            string sql = " select *from ToaThuoc";
            Functions.FillCombo(sql, cb_matoathuoc, "MaToaThuoc", "MaToaThuoc");
            cb_matoathuoc.SelectedIndex = -1;
        }

        private void display_Tenthuoc()
        {
            string sql = " select *from ToaThuoc";
            Functions.FillCombo(sql, cbo_tenthuoc, "MaToaThuoc", "TenThuoc");
            cbo_tenthuoc.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_ngtru.Ma_NgoaiTru, bn_ngtru.NgayKham, bn_ngtru.SoBHYT, bs.MaBacSi,bs.TenBacSi, bs.MaKhoa,  bn_ngtru.MaToaThuoc,tt.TenThuoc" +
                " from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo inner join BacSi bs on bs.MaBacSi=bn_ngtru.MaBacSi inner join ToaThuoc tt on tt.MaToaThuoc= bn_ngtru.MaToaThuoc";
           bn_ngoaitru = Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            Gridview_BN_Ngoaitru.DataSource = bn_ngoaitru; //Nguồn dữ liệu            
            Gridview_BN_Ngoaitru.Columns[0].HeaderText = "Mã hồ sơ";
            Gridview_BN_Ngoaitru.Columns[1].HeaderText = " Họ tên";
            Gridview_BN_Ngoaitru.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";

            Gridview_BN_Ngoaitru.Columns[2].HeaderText = "Ngày sinh";
            Gridview_BN_Ngoaitru.Columns[3].HeaderText = "Giới tính ";
            Gridview_BN_Ngoaitru.Columns[4].HeaderText = "Mã ngoại trú ";
            Gridview_BN_Ngoaitru.Columns[5].HeaderText = "Ngày khám";
            Gridview_BN_Ngoaitru.Columns[6].HeaderText = "Số BHYT";
            Gridview_BN_Ngoaitru.Columns[7].HeaderText = "Mã bác sĩ";
            Gridview_BN_Ngoaitru.Columns[8].HeaderText = "Bác sĩ khám";
            Gridview_BN_Ngoaitru.Columns[9].HeaderText = "Mã khoa";
            Gridview_BN_Ngoaitru.Columns[10].HeaderText = "Mã toa thuốc";
            Gridview_BN_Ngoaitru.Columns[11].HeaderText = "Tên thuốc";
            Gridview_BN_Ngoaitru.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            Gridview_BN_Ngoaitru.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp

        }

        /// hiển thị dữ liệu trong combobox
        /// 

        private void display_TenbacSi()
        {
            string sql = "select * from  BacSi";
            Functions.FillCombo(sql, cb_TenBacSi, "MaBacSi", "TenBacSi");
            cb_TenBacSi.SelectedIndex = -1;
        }
        private void display_MaBacsi()
        {
            string sql = "select * from  BacSi";
            Functions.FillCombo(sql, cb_mabacsi, "MaBacSi", "MaBacSi");
            cb_mabacsi.SelectedIndex = -1;
        }
        private void display_maHoSo()
        {
            string sql = " select* from BenhNhan";
            Functions.FillCombo(sql, cb_maHS, "MaHoSo", "MaHoSo");
            cb_maHS.SelectedIndex = -1;
        }
        private void display_maKhoa()
        {
            string sql = " select* from Khoa";
            Functions.FillCombo(sql, cb_maKhoa, "MaKhoa", "MaKhoa");
            cb_maKhoa.SelectedIndex = -1;
        }
        private void display_maNgoaitru()
        {
            string sql = " select* from BN_NgoaiTru";
            Functions.FillCombo(sql, cb_maNgtru, "Ma_NgoaiTru", "Ma_NgoaiTru");
            cb_maNgtru.SelectedIndex = -1;

        }
        private void display_TenBN()
        {
            string sql = " select* from BenhNhan";
            Functions.FillCombo(sql, cb_TenBn, "MaHoSo", "TenBN");
            cb_TenBn.SelectedIndex = -1;
        }

        private void Resetvalues()
        {
            cb_maHS.Focus();
            cb_maHS.Text = "";
            cb_maKhoa.Text = "";
            cb_maNgtru.Text = "";
            cb_TenBn.Text = "";
            cb_TenBacSi.Text = "";
            txt_BHYT.Text = "";
            cb_matoathuoc.Text = "";
            cbo_tenthuoc.Text = "";

        }

        private void Gridview_BN_Ngoaitru_Click(object sender, EventArgs e)
        {
            if (bn_ngoaitru.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cb_maHS.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["MaHoSo"].Value.ToString();
            cb_TenBn.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["TenBN"].Value.ToString();
            date_ngaysinh.Value = (DateTime)Gridview_BN_Ngoaitru.CurrentRow.Cells["NgaySinh"].Value;
           
            cb_maNgtru.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["Ma_NgoaiTru"].Value.ToString();
            date_ngaykham.Value = (DateTime) Gridview_BN_Ngoaitru.CurrentRow.Cells["NgayKham"].Value;
           
            txt_BHYT.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["SoBHYT"].Value.ToString();
            cb_matoathuoc.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["MaToaThuoc"].Value.ToString();
            cbo_tenthuoc.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["TenThuoc"].Value.ToString();
            cb_maKhoa.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["MaKhoa"].Value.ToString();
            cb_TenBacSi.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["TenBacSi"].Value.ToString();
            cb_mabacsi.Text = Gridview_BN_Ngoaitru.CurrentRow.Cells["MaBacSi"].Value.ToString();
            string sex = Gridview_BN_Ngoaitru.CurrentRow.Cells["Gioitinh"].Value.ToString();
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
            cb_maNgtru.Enabled = true;
            cb_mabacsi.Enabled = true;
            btn_add.Enabled = false;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            btn_add.Enabled = false;
            btn_edit.Enabled = false;
            string sql; //Lưu lệnh sql
            if (cb_maHS.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã hồ sơ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maHS.Focus();
                return;
            }
            if (cb_maNgtru.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã ngoại trú", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maNgtru.Focus();
                return;
            }
            if (cb_maKhoa.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã khoa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maKhoa.Focus();
                return;
            }
            if (cb_TenBn.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_TenBn.Focus();
                return;
            }
            string Sql_check_BHYT = "select SoBHYT from BN_NgoaiTru where Ma_NgoaiTru =N'"+cb_maNgtru.Text.Trim()+"'";
            if (Functions.CheckKey(Sql_check_BHYT))
            {
                MessageBox.Show("Số bảo hiểm y tế đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               txt_BHYT.Focus();
                return;
            }
            sql = "Select Ma_NgoaiTru From BN_NgoaiTru where Ma_NgoaiTru=N'" + cb_maNgtru.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã ngoại đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_maNgtru.Focus();
                return;
            }
            string check_key_thuoc = " select MaToaThuoc from ToaThuoc where MaToaThuoc='"+cb_matoathuoc.Text.Trim()+"'";
            if (Functions.CheckKey(check_key_thuoc)==false)
            {
                MessageBox.Show("Mã không tồn tại trong kho thuốc, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cb_matoathuoc.Focus();
                return;
            }
            sql = "Select MaHoSo From BenhNhan where MaHoSo=N'" + cb_maHS.Text.Trim() + "'";
            if (Functions.CheckKey(sql) == false)
            {
                String maLoaiBN = "BN_NGoaiT";
                if (checkbox_nam.Checked == true)
                {
                    string gioitinh = "Nam";
                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" +date_ngaysinh.Value + "',N'" + gioitinh + "', '" + maLoaiBN + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
                else if (checkbox_nu.Checked == true)
                {
                    string gioitinh = "Nữ";
                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" + date_ngaysinh.Value + "',N'" + gioitinh + "','" + maLoaiBN + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
            }
            else
            {
                sql = "update benhnhan set mahoso='" + cb_maHS.Text.Trim() + "', TenBN= N'" + cb_TenBn.Text.Trim() + "',NgaySinh ='" + date_ngaysinh.Value + "', MaLoaiBN='BN_NGoaiT' where mahoso='" + cb_maHS.Text.Trim() + "'";
                Functions.RunSql(sql);
                String check_maBs = "select MaBacSi from BacSi where MaBacSi='" + cb_mabacsi.Text.Trim() + "' ";
                if (Functions.CheckKey(check_maBs) == false)
                {
                    MessageBox.Show("Mã bác sĩ không tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cb_mabacsi.Focus();
                    return;
                }
                string maLoaiBN = "BN_NGoaiT";
                string add_BN_Ntru = "insert into  BN_NgoaiTru values('" + cb_maNgtru.Text.Trim() + "','" +date_ngaykham.Value + "','" + txt_BHYT.Text.Trim() + "','" + cb_matoathuoc.Text.Trim() + "','" +cb_maHS.Text.Trim() + "','" + maLoaiBN + "','" + cb_mabacsi.Text.Trim() + "')";
                Functions.RunSql(add_BN_Ntru);
            }
            LoadDataGridView(); //Nạp lại DataGridView
            btn_add.Enabled=true;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (bn_ngoaitru.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_maNgtru.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bệnh nhân "+cb_TenBn.Text+ " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "DELETE BN_NgoaiTru WHERE Ma_NgoaiTru=N'" + cb_maNgtru.Text.Trim() + " '";
                Functions.RunSql(sql);
                LoadDataGridView();
                Resetvalues();
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            cb_maNgtru.Enabled = false;
            btn_save.Enabled = false;
                      

            if (bn_ngoaitru.Rows.Count == 0)
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
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" + date_ngaysinh.Value + "', GioiTinh=N'" + gioitinh + "' Where MaHoSo='" + cb_maHS.Text.Trim() + "' ";
                Functions.RunSql(sql_1);
            }
            if (checkbox_nu.Checked == true)
            {
                gioitinh = "Nữ";
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" + date_ngaysinh.Value + "', GioiTinh=N'" + gioitinh + "' Where MaHoSo='" + cb_maHS.Text.Trim() + "'  ";
                Functions.RunSql(sql_1);
            }
            string Sql_check_BHYT = "select SoBHYT from BN_NgoaiTru where Ma_NgoaiTru =N'" + cb_maNgtru.Text.Trim() + "'";
            if (Functions.CheckKey(Sql_check_BHYT))
            {
                MessageBox.Show("Số bảo hiểm y tế đã tồn tại, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_BHYT.Focus();
                return;
            }
            string edit_Bsi = "update BacSi set TenBacSi=N'" + cb_TenBacSi.Text.Trim() + "' where MaBacSi ='" + cb_mabacsi.Text.Trim() + "'";
            Functions.RunSql(edit_Bsi);
            String edit_BN_NoiTru = "update BN_NgoaiTru set NgayKham='"+date_ngaykham.Value+"', SoBHYT='"+txt_BHYT.Text.Trim()+"', MaToaThuoc='"+cb_matoathuoc.Text.Trim()+"' ";
            Functions.RunSql(edit_BN_NoiTru);
            LoadDataGridView(); //Nạp lại DataGridView
        }

        private void bt_find_name_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_ngtru.Ma_NgoaiTru, bn_ngtru.NgayKham, bn_ngtru.SoBHYT, bs.MaBacSi,bs.TenBacSi, bs.MaKhoa,  bn_ngtru.MaToaThuoc,tt.TenThuoc" +
                " from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo inner join BacSi bs on bs.MaBacSi=bn_ngtru.MaBacSi inner join ToaThuoc tt on tt.MaToaThuoc= bn_ngtru.MaToaThuoc where TenBN like N'%" + txt_find_by_name.Text.Trim() + "%'";

           bn_ngoaitru = Functions.GetDataTable(sql);
            if (bn_ngoaitru.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + bn_ngoaitru.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN_Ngoaitru.DataSource = bn_ngoaitru;
        }

        private void btn_find_maHso_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_ngtru.Ma_NgoaiTru, bn_ngtru.NgayKham, bn_ngtru.SoBHYT, bs.MaBacSi,bs.TenBacSi, bs.MaKhoa,  bn_ngtru.MaToaThuoc,tt.TenThuoc" +
                " from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo inner join BacSi bs on bs.MaBacSi=bn_ngtru.MaBacSi inner join ToaThuoc tt on tt.MaToaThuoc= bn_ngtru.MaToaThuoc where bn.MaHoSo like N'%" + txt_find_by_ma.Text.Trim() + "%'";

            bn_ngoaitru = Functions.GetDataTable(sql);
            if (bn_ngoaitru.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + bn_ngoaitru.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Gridview_BN_Ngoaitru.DataSource = bn_ngoaitru;
        }

        private void sort_Click(object sender, EventArgs e)
        {
           String sql = "select bn.MaHoSo, bn.TenBN, bn.NgaySinh, bn.GioiTinh, bn_ngtru.Ma_NgoaiTru, bn_ngtru.NgayKham, bn_ngtru.SoBHYT, bs.MaBacSi,bs.TenBacSi, bs.MaKhoa,  bn_ngtru.MaToaThuoc,tt.TenThuoc" +
              " from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo inner join BacSi bs on bs.MaBacSi=bn_ngtru.MaBacSi inner join ToaThuoc tt on tt.MaToaThuoc= bn_ngtru.MaToaThuoc  order by TenBN Asc";
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

        // Thuốc change
        private void cb_matoathuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select TenThuoc from ToaThuoc where MaToaThuoc= '"+cb_matoathuoc.Text+"'";
            cbo_tenthuoc.Text=Functions.GetFieldValues(sql);
        }
        private void cbo_tenthuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select MaToaThuoc from ToaThuoc where TenThuoc=N'" + cbo_tenthuoc.Text + "' ";
            cb_matoathuoc.Text = Functions.GetFieldValues(str);
        }

        //end thuốc change

        //Bệnh nhân Change

        private void cb_maHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            sql = "select TenBN from BenhNhan where MaHoSo= '" + cb_maHS.SelectedValue + "'";
            cb_TenBn.Text = Functions.GetFieldValues(sql);
            sql = "select SoBHYT from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where bn.MaHoSo= '" + cb_maHS.SelectedValue + "'";
            txt_BHYT.Text = Functions.GetFieldValues(sql);
            sql = "select Ma_NgoaiTru from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where bn.MaHoSo= '" + cb_maHS.SelectedValue + "'";
            cb_maNgtru.Text = Functions.GetFieldValues(sql);
            string st3 = "select GioiTinh from BenhNhan where MaHoSo ='" + cb_maHS.Text + "'";
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
          string   str = "Select NgaySinh from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime(date_ngaysinh.Text);
        }

        private void cb_TenBn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql;
            sql = "select MaHoSo from BenhNhan  where TenBN= N'" + cb_TenBn.Text + "'";
          cb_maHS.Text = Functions.GetFieldValues(sql);
            sql = "select SoBHYT from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where bn.TenBN= N'" + cb_TenBn.Text + "'";
            txt_BHYT.Text = Functions.GetFieldValues(sql);
            sql = "select Ma_NgoaiTru from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where  bn.TenBN= N'" + cb_TenBn.Text + "'";
            cb_maNgtru.Text = Functions.GetFieldValues(sql);
            string st3 = "select GioiTinh from BenhNhan where MaHoSo ='" + cb_maHS.Text + "'";
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
            string str = "Select NgaySinh from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime(date_ngaysinh.Text);
        }

        private void cb_maNgtru_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select bn.MaHoSo from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where Ma_NgoaiTru ='"+cb_maNgtru.Text+"' ";
            cb_maHS.Text = Functions.GetFieldValues(str);
            str = "select bn.TenBN from BenhNhan bn inner join BN_NgoaiTru bn_ngtru on bn_ngtru.MaHoSo=bn.MaHoSo where Ma_NgoaiTru ='" + cb_maNgtru.Text + "' ";
            cb_TenBn.Text = Functions.GetFieldValues(str);

        }

        //end bênh nhân change

        //Bac si change

        private void cb_TenBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "SELECT MaBacSi FROM BacSi where TenBacSi=N'" + cb_TenBacSi.Text + "'";
            cb_mabacsi.Text = Functions.GetFieldValues(str);
            string str1 = "SELECT BacSi.MaKhoa FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where BacSi.MaBacSi='" + cb_mabacsi.SelectedValue + "'";
            cb_maKhoa.Text = Functions.GetFieldValues(str1);
        }

        private void cb_mabacsi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "SELECT TenBacSi FROM BacSi where MaBacSi='" + cb_mabacsi.SelectedValue + "'";
            cb_TenBacSi.Text = Functions.GetFieldValues(str);
            string str1 = "SELECT BacSi.MaKhoa FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where BacSi.MaBacSi='" + cb_mabacsi.SelectedValue + "'";
            cb_maKhoa.Text = Functions.GetFieldValues(str1);
        }

        private void cb_maKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "SELECT MaBacSi FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where Khoa.MaKhoa=N'" + cb_maKhoa.Text + "'";
            cb_mabacsi.Text = Functions.GetFieldValues(str);
            str = "SELECT TenBacSi FROM BacSi join Khoa on Khoa.MaKhoa= BacSi.MaKhoa where Khoa.MaKhoa=N'" + cb_maKhoa.Text + "'";
            cb_TenBacSi.Text = Functions.GetFieldValues(str);
         

        }

        //end bac sĩ change

    }
}
