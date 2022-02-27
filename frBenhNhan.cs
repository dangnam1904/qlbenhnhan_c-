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
    public partial class frBenhNhan : Form
    {
        DataTable BenhNhan;
        public frBenhNhan()
        {
            InitializeComponent();
        }


        public static SqlConnection Con;  //Khai báo đối tượng kết nối        

        public static void Connect()
        {
            Con = new SqlConnection();   //Khởi tạo đối tượng
            Con.ConnectionString = @"Data Source=DESKTOP-RT3QMVS;Initial Catalog=quanlybenhnhan;Integrated Security=True";
            Con.Open();                  //Mở kết nối
            //Kiểm tra kết nối

        }
        private void frBenhNhan_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            display_maHoSo();
            display_LoaiBN();
            display_TenBN();
        }

        private void LoadDataGridView()
        {
            String sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn left join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN";
           BenhNhan= Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            Gridview_BN.DataSource = BenhNhan; //Nguồn dữ liệu            
            Gridview_BN.Columns[0].HeaderText = "Mã hồ sơ";
            Gridview_BN.Columns[1].HeaderText = " Họ tên";
            Gridview_BN.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Gridview_BN.Columns[2].HeaderText = "Ngày sinh";
            Gridview_BN.Columns[3].HeaderText = "Giới tính ";
            Gridview_BN.Columns[4].HeaderText = "Mã loại ";
            Gridview_BN.Columns[5].HeaderText = "Tên Loại";
            Gridview_BN.Columns[5].Width = 300;
           
          
            Gridview_BN.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            Gridview_BN.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
        }

        private void display_maHoSo()
        {
            string sql = " select* from BenhNhan";
            Functions.FillCombo(sql, cb_maHS, "MaHoSo", "MaHoSo");
            cb_maHS.SelectedIndex = -1;
        }
       
        private void display_LoaiBN()
        {
            string sql = " select* from LoaiBN";
            Functions.FillCombo(sql, cb_maLoai, "MaLoaiBN", "MaLoaiBN");
            cb_maLoai.SelectedIndex = -1;

        }
        private void display_TenBN()
        {
            string sql = " select* from BenhNhan";
            Functions.FillCombo(sql, cb_TenBn, "MaHoSo", "TenBN");
            cb_TenBn.SelectedIndex = -1;
        }

        private void Gridview_BN_Click(object sender, EventArgs e)
        {
            if (btn_add.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maHS.Focus();
                return;
            }
            if (BenhNhan.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cb_maHS.Text = Gridview_BN.CurrentRow.Cells["MaHoSo"].Value.ToString();
            cb_TenBn.Text = Gridview_BN.CurrentRow.Cells["TenBN"].Value.ToString();
          
            date_ngaysinh.Value = (DateTime)Gridview_BN.CurrentRow.Cells["NgaySinh"].Value;
            cb_maLoai.Text = Gridview_BN.CurrentRow.Cells["MaLoaiBN"].Value.ToString();
          
            string sex = Gridview_BN.CurrentRow.Cells["Gioitinh"].Value.ToString();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Resetvalues();
            cb_maHS.Enabled = true;
            btn_add.Enabled = false;
           
        }

        private void Resetvalues()
        {
            cb_maHS.Text = "";
            cb_maLoai.Text = "";
            cb_TenBn.Text = "";
          
            
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string sql; //Lưu lệnh sql
            if (cb_maHS.Text.Trim().Length == 0) //Nếu chưa nhập mã chất liệu
            {
                MessageBox.Show("Bạn phải nhập mã hồ sơ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_maHS.Focus();
                return;
            }
    
           
            if (cb_TenBn.Text.Trim().Length == 0) //Nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn phải nhập tên bệnh nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_TenBn.Focus();
                return;
            }

            sql = "Select MaHoSo From BenhNhan where MaHoSo=N'" + cb_maHS.Text.Trim() + "'";
            if (Functions.CheckKey(sql) == false)
            {
                if (checkbox_nam.Checked == true)
                {
                    string gioitinh = "Nam";

                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" +date_ngaysinh.Value + "',N'" + gioitinh + "','" + cb_maLoai.Text.Trim() + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
                else if (checkbox_nu.Checked == true)
                {
                    string gioitinh = "Nữ";
                    String sql_addbenhnhan = "insert into BenhNhan values(N'" + cb_maHS.Text.Trim() + "',N'" + cb_TenBn.Text.Trim() + "','" + date_ngaysinh.Value + "',N'" + gioitinh + "', '" + cb_maLoai.Text.Trim() + "')";
                    Functions.RunSql(sql_addbenhnhan);
                }
            }
            else
            {

                MessageBox.Show("Mã hồ sơ đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
               cb_maHS.Focus();
                return;
            }
            LoadDataGridView(); //Nạp lại DataGridView
            btn_add.Enabled = true;

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (BenhNhan.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cb_maHS.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá "  +cb_TenBn.Text+" không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string check_Fk_BN_CV = "select MaHoSo from BN_CV where mahoso=N'"+cb_maHS.Text.Trim()+"'";
                string check_Fk_BN_NTru = "select MaHoSo from BN_NoiTru where mahoso=N'" + cb_maHS.Text.Trim() + "'";
                string check_Fk_BN_NGTru = "select MaHoSo from BN_Ngoaitru where mahoso=N'" + cb_maHS.Text.Trim() + "'";
               if(Functions.CheckKey(check_Fk_BN_CV) || Functions.CheckKey(check_Fk_BN_NGTru) || Functions.CheckKey(check_Fk_BN_NTru))
                {
                    MessageBox.Show("Bệnh nhân này đang điều trị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    string sql = "DELETE BenhNhan WHERE MaHoSo='" + cb_maHS.Text.Trim() + " '";
                    Functions.RunSql(sql);
                }
                
               
            }
            LoadDataGridView();
            Resetvalues();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            cb_maHS.Enabled = false;
            btn_add.Enabled=false;
            btn_save.Enabled=false;
           

            if (BenhNhan.Rows.Count == 0)
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
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" + date_ngaysinh.Value + "', GioiTinh='" + gioitinh + "', MaLoaiBN='"+cb_maLoai.Text.Trim()+"' Where MaHoSo='" + cb_maHS.Text.Trim() + "' ";
                Functions.RunSql(sql_1);
            }
            if (checkbox_nu.Checked == true)
            {
                gioitinh = "Nữ";
                string sql_1 = "Update BenhNhan set MaHoSo='" + cb_maHS.Text.Trim() + "', TenBN=N'" + cb_TenBn.Text.Trim() + "',NgaySinh='" + date_ngaysinh.Value + "', GioiTinh='" + gioitinh + "',  MaLoaiBN='" + cb_maLoai.Text.Trim() + "' Where MaHoSo='" + cb_maHS.Text.Trim() + "'  ";
                Functions.RunSql(sql_1);
            }
            if(checkbox_nam.Checked==true && checkbox_nu.Checked == true)
            {
                MessageBox.Show("Bạn chỉ được chọn một giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            LoadDataGridView(); //Nạp lại DataGridView
        }

        private void bt_find_name_Click(object sender, EventArgs e)
        {
            if ((txt_find_by_name.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN where TenBN like N'%" + txt_find_by_name.Text.Trim() + "%'";

            BenhNhan= Functions.GetDataTable(sql);
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

        private void btn_sort_Click(object sender, EventArgs e)
        {
            string sql_sort = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn inner join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN order by TenBN Asc";
           Functions.RunSql(sql_sort);
           LoadDataGridView();
           // string sql_sort_1 = " select TenBN from BenhNhan";
           //List<string> list = new List<string>();
           // var name = "";
           // Connect();
           // SqlCommand cmd = new SqlCommand(sql_sort,Con);
           // SqlDataReader reader;
           // reader = cmd.ExecuteReader();
           // if (reader.HasRows)
           // {
           //     while (reader.Read())
           //         name = reader.GetString("TenBN");
           //     list.Add(name);
              
           // }
           // else
           // {
           //     reader.Close();
           // }
           // for( int i = 0; i < list.Count; i++)
           // {
           //     list[i].ToString();
           // }
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
            str = "select TenBN from BenhNhan where MaHoSo='"+cb_maHS.Text+"' ";
            cb_TenBn.Text = Functions.GetFieldValues(str);
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
             str = "Select NgaySinh from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime(date_ngaysinh.Text);
            str = "select BenhNhan.MaLoaiBN from BenhNhan join LoaiBN on BenhNhan.MaLoaiBN=LoaiBN.MaLoaiBN where MaHoSo='"+cb_maHS.Text+"'"; 
            cb_maLoai.Text=Functions.GetFieldValues(str);
        }

        private void cb_TenBn_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            str = "select MaHoSo from BenhNhan where TenBN=N'" + cb_TenBn.Text + "' ";
            cb_maHS.Text = Functions.GetFieldValues(str);
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
            str = "Select NgaySinh from BenhNhan where MaHoSo = N'" + cb_maHS.Text + "'";
            date_ngaysinh.Text = Functions.GetFieldValues(str);
            date_ngaysinh.Value = Convert.ToDateTime(date_ngaysinh.Text);
            str = "select BenhNhan.MaLoaiBN from BenhNhan join LoaiBN on BenhNhan.MaLoaiBN=LoaiBN.MaLoaiBN where MaHoSo='" + cb_maHS.Text + "'";
            cb_maLoai.Text = Functions.GetFieldValues(str);
        }

        private void cb_maLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            String sql = " select bn.MaHoSo, TenBN,NgaySinh,GioiTinh,bn.MaLoaiBN, TenLoai from BenhNhan bn left join LoaiBN LBn on bn.MaLoaiBN=LBn.MaLoaiBN where bn.MaLoaiBN='"+cb_maLoai.Text+"'";
            BenhNhan = Functions.GetDataTable(sql); //Đọc dữ liệu từ bảng
            Gridview_BN.DataSource = BenhNhan; //Nguồn dữ liệu            
            Gridview_BN.Columns[0].HeaderText = "Mã hồ sơ";
            Gridview_BN.Columns[1].HeaderText = " Họ tên";
            Gridview_BN.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            Gridview_BN.Columns[2].HeaderText = "Ngày sinh";
            Gridview_BN.Columns[3].HeaderText = "Giới tính ";
            Gridview_BN.Columns[4].HeaderText = "Mã loại ";
            Gridview_BN.Columns[5].HeaderText = "TenLoai";
        }
    }
}
