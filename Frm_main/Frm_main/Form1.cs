using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Frm_main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String Conn = @"Data Source=ADMIN;Initial Catalog=QLTTV;Integrated Security=True";
        SqlConnection sqlcon;
        SqlDataAdapter Da;
        DataTable Dt;
        SqlCommand Cmd;


        void refesh_dgv(DataGridView dgv, string tenbang)
        {
            sqlcon.Open();
            Cmd = sqlcon.CreateCommand();
            Cmd.CommandText = "select * from " + tenbang;

            Da = new SqlDataAdapter(Cmd);
            Dt = new DataTable();
            Da.Fill(Dt);
            dgv.DataSource = Dt;
            sqlcon.Close();
        }
        void refesh_dgv_HD()
        {
            sqlcon.Open();
            
            Cmd = sqlcon.CreateCommand();
            Cmd.CommandText = "select hd.MAHD as N'Mã Hoá Đơn',cthd.MASP as N'Mã Sản Phẩm',cthd.SOLUONG as N'Số lượng',Cthd.THANHTIEN as N'Thành Tiền',hd.MAKH as N'Mã Khách Hàng',hd.MANV as N'Mã Nhân Viên',hd.ngayxuathd as N'Ngày Xuất'  from SANPHAM sp, HOADON Hd, CHITIETHOADON Cthd, KHACHHANG Kh, NHANVIEN Nv where hd.MAHD = cthd.MAHD and hd.MANV = nv.MANV And hd.MAKH = kh.MAKH And sp.MASP = Cthd.MASP";
            Da = new SqlDataAdapter(Cmd);
            Dt = new DataTable();
            Da.Fill(Dt);
            dgv_HoaDon.DataSource = Dt;
            sqlcon.Close();
        }
        void antabpage()
        {
            tabControl1.TabPages.Remove(tab_thethanhvien);
            tabControl1.TabPages.Remove(tab_hoadon);
            tabControl1.TabPages.Remove(tab_diemthuong);
            tabControl1.TabPages.Remove(tab_SanPham);
            tabControl1.TabPages.Remove(tab_nhanvien);
            tabControl1.TabPages.Remove(tab_KhachHang);
            tabControl1.Hide();
        }
        void kiemtratabpage(TabPage tb, TabControl tb_ctr)
        {

            if (tabControl1.TabPages.Contains(tb) == true)
            {
                tb_ctr.SelectedTab = tb;

            }
            else
            {
                tb_ctr.Show();
                tb_ctr.TabPages.Add(tb);
                tb_ctr.SelectedTab = tb;

            }
        }
        private void dgv_sanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLTTVDataSet.THETHANHVIEN' table. You can move, or remove it, as needed.
            this.tHETHANHVIENTableAdapter.Fill(this.qLTTVDataSet.THETHANHVIEN);
            // TODO: This line of code loads data into the 'qLTTVDataSet.NHANVIEN' table. You can move, or remove it, as needed.
            this.nHANVIENTableAdapter.Fill(this.qLTTVDataSet.NHANVIEN);
            // TODO: This line of code loads data into the 'qLTTVDataSet.SANPHAM' table. You can move, or remove it, as needed.
            this.sANPHAMTableAdapter.Fill(this.qLTTVDataSet.SANPHAM);
            // TODO: This line of code loads data into the 'qLTTVDataSet.KHACHHANG' table. You can move, or remove it, as needed.
            this.kHACHHANGTableAdapter.Fill(this.qLTTVDataSet.KHACHHANG);
            sqlcon = new SqlConnection(Conn);
            //WindowState = FormWindowState.Maximized;
            antabpage();
        }

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_SanPham, tabControl1);

        }

        private void thựcĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_hoadon, tabControl1);
            refesh_dgv_HD();
        }

        private void ThethanhvienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_thethanhvien, tabControl1);

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_nhanvien, tabControl1);

        }

        private void điểmThưởngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_diemthuong, tabControl1);

        }

        private void thanPhiềnGópÝToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Bạn có thật sự muốn thoát không?", "Thông Báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();   
            }
        }

        private void btn_Tracuu_Tracuuthe_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_them_TheThanhVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaTTV_TTV.Text != "" & maKHComboBox_TTV.Text != "" & loaiTheComboBox.Text != "")
                {
                    sqlcon.Open();
                    Cmd = sqlcon.CreateCommand();
                    SqlCommand cmd2 = sqlcon.CreateCommand();
                    cmd2.CommandText = "select count(*) from TheThanhVien where MaKH=@KH ";
                    cmd2.Parameters.AddWithValue("@KH", maKHComboBox_TTV.Text);
                    Da = new SqlDataAdapter(cmd2);
                    cmd2.ExecuteNonQuery();
                    Dt = new DataTable();
                    Da.Fill(Dt);

                    if (Dt.Rows[0][0].ToString() =="1")
                    {
                        MessageBox.Show("Mã Khách Hàng Đã Được sử dụng cho Thẻ Thành Viên Khác!!!");
                    }
                    else
                    {
                        Cmd.CommandText = "Insert into TheThanhVien Values(@MaTTV,@MaKH,@LoaiThe,@NgayCap,@NgayHetHan,@DoanhSo,@Diem)";
                        Cmd.Parameters.AddWithValue("@MaTTV", txt_MaTTV_TTV.Text);
                        Cmd.Parameters.AddWithValue("@MaKH", maKHComboBox_TTV.Text);
                        Cmd.Parameters.AddWithValue("@LoaiThe", loaiTheComboBox.Text);
                        Cmd.Parameters.AddWithValue("@NgayCap", DTP_NgayCap_TTV.Value);
                        Cmd.Parameters.AddWithValue("@NgayHetHan", DTP_NgayHetHan_TTV.Value);
                        
                        if (loaiTheComboBox.Text == "Bạc")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "1000000");
                            Cmd.Parameters.AddWithValue("@Diem", "10");

                        }
                        else if (loaiTheComboBox.Text == "Vàng")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "2000000");
                            Cmd.Parameters.AddWithValue("@Diem", "20");


                        }
                        else if (loaiTheComboBox.Text == "Bạch Kim")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "3000000");
                            Cmd.Parameters.AddWithValue("@Diem", "30");


                        }
                        else
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "4000000");
                            Cmd.Parameters.AddWithValue("@Diem", "40");


                        }
                        Cmd.ExecuteNonQuery(); MessageBox.Show("Thêm Thành công!!!");
                    }
                    
                   

                    sqlcon.Close();
                    refesh_dgv(dgv_thethanhvien, "TheThanhVien");
                }
                else MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaSP_SP.Text != "" & txt_TenSP_SP.Text != "" & txt_GiaBan_SP.Text != "" & txt_DiemTichLuy_SP.Text != "" & txt_TheLoai_SP.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from SanPham where MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() != "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "insert into SanPham Values (@MaSP,@TenSP,@GiaBan,@Diem,@TheLoai,@NamSX)";
                        Cmd.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);
                        Cmd.Parameters.AddWithValue("@TenSP", txt_TenSP_SP.Text);
                        Cmd.Parameters.AddWithValue("@GiaBan", txt_GiaBan_SP.Text);
                        Cmd.Parameters.AddWithValue("@Diem", txt_DiemTichLuy_SP.Text);
                        Cmd.Parameters.AddWithValue("@TheLoai", txt_TheLoai_SP.Text);
                        Cmd.Parameters.AddWithValue("@NamSX", DTP_NamSX_SP.Value);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thêm Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_sanpham, "SanPham");
                    }else
                    {
                        MessageBox.Show("Data inserted");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
private void btn_Sua_SP_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaSP_SP.Text != "" & txt_TenSP_SP.Text != "" & txt_GiaBan_SP.Text != "" & txt_DiemTichLuy_SP.Text != "" & txt_TheLoai_SP.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from SanPham where MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "update SanPham set TenSP=@TenSP,GiaBan=@GiaBan,DiemTichLuy_SP=@Diem,TheLoai_SP=@TheLoai,NamSX_SP=@NamSX where MaSP=@MaSP";
                        Cmd.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);
                        Cmd.Parameters.AddWithValue("@TenSP", txt_TenSP_SP.Text);
                        Cmd.Parameters.AddWithValue("@GiaBan", txt_GiaBan_SP.Text);
                        Cmd.Parameters.AddWithValue("@Diem", txt_DiemTichLuy_SP.Text);
                        Cmd.Parameters.AddWithValue("@TheLoai", txt_TheLoai_SP.Text);
                        Cmd.Parameters.AddWithValue("@NamSX", DTP_NamSX_SP.Value);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sửa Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_sanpham, "SanPham");
                    }
                    else
                    {
                        MessageBox.Show("Data not insert!");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Xoa_SP_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaSP_SP.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from SanPham where MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "delete from SanPham where MaSP=@MaSP";
                        Cmd.Parameters.AddWithValue("@MaSP", txt_MaSP_SP.Text);

                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoá Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Xoá Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_sanpham, "SanPham");
                    }
                    else
                    {
                        MessageBox.Show("data not insert");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaHD_HD.Text != "" & CB_MaSP_HD.Text != "" & txt_SoLuong_HD.Text != "" & CB_MaNV_HD.Text != "" & CB_MaKH_HD.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from ChiTietHoaDon where MaHD=@MaHD and MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                    Cmd1.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if(Dt.Rows[0][0].ToString() != "1")
                    {
                    
                       
                            Cmd = sqlcon.CreateCommand();
                            Cmd.CommandText = "exec addhoadon @MaHD,@MaSP,@SoLuong,@MaNV,@MaKH,@NgayXuat";
                            Cmd.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                            Cmd.Parameters.AddWithValue("@NgayXuat", dtp_NgayXuat_HoaDon.Value);
                            Cmd.Parameters.AddWithValue("@MaNV", CB_MaNV_HD.Text);
                            Cmd.Parameters.AddWithValue("@MaKH", CB_MaKH_HD.Text);
                            Cmd.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                              Cmd.Parameters.AddWithValue("@SoLuong", txt_SoLuong_HD.Text);

                        MessageBox.Show("Insert Succes!!");
                        Cmd.ExecuteNonQuery();
                        sqlcon.Close();
                            refesh_dgv_HD();
                    }
                    else
                    {
                        MessageBox.Show("Đã Tồn Tại Dữ Liệu Này"); sqlcon.Close();
                    }


                }
                else
                {
                    MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin"); sqlcon.Close();
                }    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Sua_HD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaHD_HD.Text != "" & CB_MaSP_HD.Text != "" & txt_SoLuong_HD.Text != "" & dtp_NgayXuat_HoaDon.Text != "" & CB_MaNV_HD.Text != "" & CB_MaKH_HD.Text != "")
                {
                    sqlcon.Open();

                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from ChiTietHoaDon where MaHD=@MaHD and MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                    Cmd1.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString()== "1")
                    {

                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "exec updatehoadon @MaHD,@MaSP,@SoLuong,@MaNV,@MaKH,@NgayXuat";
                        Cmd.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                        Cmd.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                        Cmd.Parameters.AddWithValue("@SoLuong", txt_SoLuong_HD.Text);
                        Cmd.Parameters.AddWithValue("@NgayXuat", dtp_NgayXuat_HoaDon.Value);
                        Cmd.Parameters.AddWithValue("@MaNV", CB_MaNV_HD.Text);
                        Cmd.Parameters.AddWithValue("@MaKH", CB_MaKH_HD.Text);
                        MessageBox.Show("Change Succes!!!!");
                        Cmd.ExecuteNonQuery();
                        sqlcon.Close();
                        refesh_dgv_HD();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Inserted");
                        sqlcon.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                //MessageBox.Show("Đã Tồn Tại Dữ Liệu! Vui Lòng Chọn Chức Năng Khác!!!");
            }
        }

        private void btn_Sua_TTV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaTTV_TTV.Text != "" & maKHComboBox_TTV.Text != "" & loaiTheComboBox.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from TheThanhVien Where MaTTV=@MaTTV and MaKH=@MaKH";
                    Cmd1.Parameters.AddWithValue("@MaTTV", txt_MaTTV_TTV.Text);
                    Cmd1.Parameters.AddWithValue("@MaKH", maKHComboBox_TTV.Text);

                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "update TheThanhVien set MaKH=@MaKH,LoaiThe=@LoaiThe,NgayCap=@NgayCap,NgayHetHan=@NgayHetHan,DoanhSoTichLuy_TTV=@DoanhSo,DiemTichLuy_TTV=@Diem where  MaTTV=@MaTTV";
                        Cmd.Parameters.AddWithValue("@MaTTV", SqlDbType.Char).Value = txt_MaTTV_TTV.Text;
                        Cmd.Parameters.AddWithValue("@MaKH", SqlDbType.Char).Value = maKHComboBox_TTV.Text;
                        Cmd.Parameters.AddWithValue("@LoaiThe", SqlDbType.NVarChar).Value = loaiTheComboBox.Text;
                        Cmd.Parameters.AddWithValue("@NgayCap", SqlDbType.DateTime).Value = DTP_NgayCap_TTV.Value;
                        Cmd.Parameters.AddWithValue("@NgayHetHan", SqlDbType.DateTime).Value = DTP_NgayHetHan_TTV.Value;
                        if (loaiTheComboBox.Text == "Bạc")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "1000000");
                            Cmd.Parameters.AddWithValue("@Diem", "10");
                        }
                        else if (loaiTheComboBox.Text == "Vàng")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "2000000");
                            Cmd.Parameters.AddWithValue("@Diem", "20");
                        }
                        else if (loaiTheComboBox.Text == "Bạch Kim")
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "3000000");
                            Cmd.Parameters.AddWithValue("@Diem", "30");
                        }
                        else
                        {
                            Cmd.Parameters.AddWithValue("@DoanhSo", "4000000");
                            Cmd.Parameters.AddWithValue("@Diem", "40");

                        }
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sửa Thành Công");
                            sqlcon.Close();

                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                            sqlcon.Close();

                        }
                        refesh_dgv(dgv_thethanhvien, "TheThanhVien");
                    }
                    else
                    {
                        MessageBox.Show("data not insert !");
                        sqlcon.Close();
                    }
                }
                else MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Xoa_TTV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaTTV_TTV.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from TheThanhVien Where MaTTV=@MaTTV";
                    Cmd1.Parameters.AddWithValue("@MaTTV", txt_MaTTV_TTV.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "Delete from  TheThanhVien where MaTTV=@MaTTV";
                        Cmd.Parameters.AddWithValue("@MaTTV", SqlDbType.Char).Value = txt_MaTTV_TTV.Text;
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoá Thành Công");
                            sqlcon.Close();

                        }
                        else
                        {
                            MessageBox.Show("Xoá Thất Bại");
                            sqlcon.Close();

                        }
                        refesh_dgv(dgv_thethanhvien, "TheThanhVien");
                    }
                    else
                    {
                        MessageBox.Show("data not insert");
                        sqlcon.Close();
                    }
                }
                else MessageBox.Show("Bạn Chưa Nhập Mã Thẻ Thành Viên");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_gui_GopY_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_thoat_GopY_Click(object sender, EventArgs e)
        {

        }

        private void btn_Them_NV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNV_NV.Text != "" & txt_HoTen_NV.Text != "" & CB_GioiTinh_NV.Text != "" & txt_CMND_NV.Text != "" & txt_DiaChi_NV.Text != "" & txt_SDT_NV.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from NhanVien where MaNV=@MaNV";
                    Cmd1.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() != "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "insert into NhanVien Values (@MaNV,@HoTen,@GioiTinh,@DiaChi,@NgaySinh,@CMND,@SDT,@NgayVao)";
                        Cmd.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                        Cmd.Parameters.AddWithValue("@HoTen", txt_HoTen_NV.Text);
                        Cmd.Parameters.AddWithValue("@GioiTinh", CB_GioiTinh_NV.Text);
                        Cmd.Parameters.AddWithValue("@NgaySinh", DTP_NgaySinh_NV.Value);
                        Cmd.Parameters.AddWithValue("@CMND", txt_CMND_NV.Text);
                        Cmd.Parameters.AddWithValue("@SDT", txt_SDT_NV.Text);
                        Cmd.Parameters.AddWithValue("@DiaChi", txt_DiaChi_NV.Text);
                        Cmd.Parameters.AddWithValue("@NgayVao", DTP_NgayVao_NV.Value);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thêm Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_nhanvien, "NhanVien");
                    }
                    else
                    {
                        MessageBox.Show("Data inserted");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Sua_NV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNV_NV.Text != "" & txt_HoTen_NV.Text != "" & CB_GioiTinh_NV.Text != "" & txt_CMND_NV.Text != "" & txt_DiaChi_NV.Text != "" & txt_SDT_NV.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from NhanVien where MaNV=@MaNV";
                    Cmd1.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "update NhanVien set HoTenNV=@HoTen,GioiTinh_NV=@GioiTinh,DiaChi_NV=@DiaChi,NgaySinh_NV=@NgaySinh,CMND_NV=@CMND,SDT_NV=@SDT,NgayVao_NV=@NgayVao where MaNV=@MaNV";
                        Cmd.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                        Cmd.Parameters.AddWithValue("@HoTen", txt_HoTen_NV.Text);
                        Cmd.Parameters.AddWithValue("@GioiTinh", CB_GioiTinh_NV.Text);
                        Cmd.Parameters.AddWithValue("@NgaySinh", DTP_NgaySinh_NV.Value);
                        Cmd.Parameters.AddWithValue("@CMND", txt_CMND_NV.Text);
                        Cmd.Parameters.AddWithValue("@SDT", txt_SDT_NV.Text);
                        Cmd.Parameters.AddWithValue("@DiaChi", txt_DiaChi_NV.Text);
                        Cmd.Parameters.AddWithValue("@NgayVao", DTP_NgayVao_NV.Value);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sửa Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_nhanvien, "NhanVien");
                    }
                    else
                    {
                        MessageBox.Show("Data not insert");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Xoa_NV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaNV_NV.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from NhanVien where MaNV=@MaNV";
                    Cmd1.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "delete from NhanVien where MaNV=@MaNV";
                        Cmd.Parameters.AddWithValue("@MaNV", txt_MaNV_NV.Text);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoá Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Xoá Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_nhanvien, "NhanVien");
                    }
                    else
                    {
                        MessageBox.Show("data not insert");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không Tồn Tại Dữ Liệu Để Xoá!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Xoa_KH_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaKH_KH.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from khachhang where MaKH=@MaKH";
                    Cmd1.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "delete from KhachHang where MaKH=@MaKH ";
                        Cmd.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);

                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Xoá Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Xoá Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_KhachHang, "KhachHang");
                    }
                    else
                    {
                        MessageBox.Show("data not insert");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Không Có Dữ Liệu Này Để Xoá");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Them_KH_Click(object sender, EventArgs e)
        {

            try
            {
                if (txt_MaKH_KH.Text != "" & txt_HoTen_KH.Text != "" & CB_GioiTinh_KH.Text != "" & txt_CMND_KH.Text != "" & txt_SDT_KH.Text != "" & txt_DiaChi_KH.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from khachhang where MaKH=@MaKH";
                    Cmd1.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() != "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "insert into KhachHang Values (@MaKH,@HoTen,@GioiTinh,@NamSinh,@CMND,@SDT,@DiaChi)";
                        Cmd.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);
                        Cmd.Parameters.AddWithValue("@HoTen", txt_HoTen_KH.Text);
                        Cmd.Parameters.AddWithValue("@GioiTinh", CB_GioiTinh_KH.Text);
                        Cmd.Parameters.AddWithValue("@NamSinh", DTP_NgaySinh_KH.Value);
                        Cmd.Parameters.AddWithValue("@CMND", txt_CMND_KH.Text);
                        Cmd.Parameters.AddWithValue("@SDT", txt_SDT_KH.Text);
                        Cmd.Parameters.AddWithValue("@DiaChi", txt_DiaChi_KH.Text);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Thêm Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_KhachHang, "KhachHang");
                    }else
                    {
                        MessageBox.Show("Data inserted");
                        sqlcon.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_Sua_KH_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaKH_KH.Text != "" & txt_HoTen_KH.Text != "" & CB_GioiTinh_KH.Text != "" & txt_CMND_KH.Text != "" & txt_SDT_KH.Text != "" & txt_DiaChi_KH.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from khachhang where MaKH=@MaKH";
                    Cmd1.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "update KhachHang set HoTenKH=@HoTen,GioiTinhKH=@GioiTinh,NgaySinhKH=@NamSinh,CMND_KH=@CMND,SDT_KH=@SDT,DiaChi_KH=@DiaChi where MaKH=@MaKH ";
                        Cmd.Parameters.AddWithValue("@MaKH", txt_MaKH_KH.Text);
                        Cmd.Parameters.AddWithValue("@HoTen", txt_HoTen_KH.Text);
                        Cmd.Parameters.AddWithValue("@GioiTinh", CB_GioiTinh_KH.Text);
                        Cmd.Parameters.AddWithValue("@NamSinh", DTP_NgaySinh_KH.Value);
                        Cmd.Parameters.AddWithValue("@CMND", txt_CMND_KH.Text);
                        Cmd.Parameters.AddWithValue("@SDT", txt_SDT_KH.Text);
                        Cmd.Parameters.AddWithValue("@DiaChi", txt_DiaChi_KH.Text);
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sửa Thành Công");
                            sqlcon.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                            sqlcon.Close();
                        }
                        refesh_dgv(dgv_KhachHang, "KhachHang");
                    }
                    else
                    {
                        sqlcon.Close();
                        MessageBox.Show("Data not insert");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn Nhập Chưa đủ thông tin!!!");
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgv_Tracuu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
                
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_login frm = new frm_login();
            frm.ShowDialog();
            this.Close();
        }

        private void dgv_thethanhvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_thethanhvien.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaTTV_TTV.Text = row.Cells[0].Value.ToString();
                maKHComboBox_TTV.Text = row.Cells[1].Value.ToString();
                loaiTheComboBox.Text = row.Cells[2].Value.ToString();
                DateTime n1 = DateTime.Parse(row.Cells[3].Value.ToString());
                DateTime n2 = DateTime.Parse(row.Cells[4].Value.ToString());
          


                DTP_NgayCap_TTV.Value = n1;
                DTP_NgayHetHan_TTV.Value = n2;
            }
        }

        private void dgv_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_HoaDon.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaHD_HD.Text = row.Cells[0].Value.ToString();
                CB_MaSP_HD.Text = row.Cells[1].Value.ToString();
                txt_SoLuong_HD.Text = row.Cells[2].Value.ToString();
                txt_ThanhTien_HoaDon.Text = row.Cells[3].Value.ToString();
                DateTime n2 = DateTime.Parse(row.Cells[6].Value.ToString());
                CB_MaNV_HD.Text = row.Cells[5].Value.ToString();
                CB_MaKH_HD.Text = row.Cells[4].Value.ToString();
                dtp_NgayXuat_HoaDon.Value = n2;
            }
        }

        private void dgv_KhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_KhachHang.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaKH_KH.Text = row.Cells[0].Value.ToString();
                txt_HoTen_KH.Text = row.Cells[1].Value.ToString();
                CB_GioiTinh_KH.Text = row.Cells[2].Value.ToString();
                DateTime n1 = DateTime.Parse(row.Cells[3].Value.ToString());
                txt_CMND_KH.Text = row.Cells[4].Value.ToString();
                txt_SDT_KH.Text = row.Cells[5].Value.ToString();
                txt_DiaChi_KH.Text = row.Cells[6].Value.ToString();




                DTP_NgaySinh_KH.Value = n1;
            }
        }

        private void dgv_nhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgv_sanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_sanpham.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaSP_SP.Text = row.Cells[0].Value.ToString();
                txt_TenSP_SP.Text = row.Cells[1].Value.ToString();
                txt_GiaBan_SP.Text = row.Cells[2].Value.ToString();
                txt_DiemTichLuy_SP.Text = row.Cells[3].Value.ToString();
                txt_TheLoai_SP.Text = row.Cells[4].Value.ToString();
                DateTime n1 = DateTime.Parse(row.Cells[7].Value.ToString());


                DTP_NamSX_SP.Value = n1;
            }
        }

        private void dgv_diemthuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = this.dgv_diemthuong.Rows[e.RowIndex];
                cb_MaTTV_DT.Text = row.Cells[0].Value.ToString();
                txt_DiemTL_DT.Text = row.Cells[6].Value.ToString();
               
            }
        }

        private void btn_Xoa_HD_Click(object sender, EventArgs e)
        {

        }

        private void btn_Xoa_HD_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (txt_MaHD_HD.Text != ""& CB_MaSP_HD.Text!="")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from ChiTietHoaDon where MaHD=@MaHD and MaSP=@MaSP";
                    Cmd1.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                    Cmd1.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();

                        Cmd.CommandText = "delete from ChiTietHoaDon where MaHD=@MaHD and MaSP= @MaSP";
                        Cmd.Parameters.AddWithValue("@MaHD", txt_MaHD_HD.Text);
                        Cmd.Parameters.AddWithValue("@MaSP", CB_MaSP_HD.Text);
                        Cmd.ExecuteNonQuery();
                        MessageBox.Show("Delete Succes!!!");
                        sqlcon.Close();
                        refesh_dgv_HD();
                    }
                    else
                    {
                        MessageBox.Show("Data not exists!!!");
                        sqlcon.Close();
                    }
                }
                else MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (cb_MaTTV_DT.Text != "" & txt_DiemTL_DT.Text != "")
                {
                    sqlcon.Open();
                    SqlCommand Cmd1 = sqlcon.CreateCommand();
                    Cmd1.CommandText = "select count(*) from TheThanhVien Where MaTTV=@MaTTV";
                    Cmd1.Parameters.AddWithValue("@MaTTV", cb_MaTTV_DT.Text);
                    Da = new SqlDataAdapter(Cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    Cmd1.ExecuteNonQuery();
                    if (Dt.Rows[0][0].ToString() == "1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "update TheThanhVien set DiemTichLuy_TTV=@Diem where  MaTTV=@MaTTV";
                        Cmd.Parameters.AddWithValue("@MaTTV", SqlDbType.Char).Value = cb_MaTTV_DT.Text;
                        Cmd.Parameters.AddWithValue("@Diem", SqlDbType.Char).Value = txt_DiemTL_DT.Text;
                        Cmd.ExecuteNonQuery();
                        if (Cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Sửa Thành Công");
                            sqlcon.Close();

                        }
                        else
                        {
                            MessageBox.Show("Sửa Thất Bại");
                            sqlcon.Close();

                        }
                        refesh_dgv(dgv_diemthuong, "TheThanhVien");
                    }
                    else
                    {
                        MessageBox.Show("data not insert !");
                        sqlcon.Close();
                    }
                }
                else MessageBox.Show("Bạn Chưa Nhập Đủ Thông Tin");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dgv_sanpham_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_sanpham_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_sanpham.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaSP_SP.Text = row.Cells[0].Value.ToString();
                txt_TenSP_SP.Text = row.Cells[1].Value.ToString();
                txt_GiaBan_SP.Text = row.Cells[2].Value.ToString();
                txt_DiemTichLuy_SP.Text = row.Cells[3].Value.ToString();
                txt_TheLoai_SP.Text = row.Cells[4].Value.ToString();
                DateTime n1 = DateTime.Parse(row.Cells[5].Value.ToString());


                DTP_NamSX_SP.Value = n1;
            }
        }

        private void dgv_nhanvien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //gets a collection that contains all the rows
                DataGridViewRow row = this.dgv_nhanvien.Rows[e.RowIndex];
                //populate the textbox from specific value of the coordinates of column and row.
                txt_MaNV_NV.Text = row.Cells[0].Value.ToString();
                txt_HoTen_NV.Text = row.Cells[1].Value.ToString();
                CB_GioiTinh_NV.Text = row.Cells[2].Value.ToString();
                txt_DiaChi_NV.Text = row.Cells[3].Value.ToString();
                DateTime n1 = DateTime.Parse(row.Cells[4].Value.ToString());
                txt_CMND_NV.Text = row.Cells[5].Value.ToString();
                txt_SDT_NV.Text = row.Cells[6].Value.ToString();
                DateTime n2 = DateTime.Parse(row.Cells[7].Value.ToString());

                DTP_NgaySinh_NV.Value = n1;
                DTP_NgayVao_NV.Value = n2;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            kiemtratabpage(tab_KhachHang, tabControl1);

        }

        private void tab_diemthuong_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_thethanhvien);

        }

        private void btn_Thoat_HD_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_hoadon);

        }

        private void button13_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_diemthuong);

        }

        private void btn_Thoat_SP_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_SanPham);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_nhanvien);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tab_KhachHang);

        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.ShowDialog();
        }
    }
}
    

