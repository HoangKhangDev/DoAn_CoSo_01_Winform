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
    public partial class Form_KH : Form
    {
        public Form_KH()
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
     

        private void dgv_Tracuu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_Tracuu_Tracuuthe_Click(object sender, EventArgs e)
        {

        }

        private void groupBox17_Enter(object sender, EventArgs e)
        {
                    }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát không?", "Thông Báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btn_thoat_GopY_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát không?", "Thông Báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_makh_tct.Text != "" & txt_tenkh_tct.Text != "")
                {

                    sqlcon.Open();
                    SqlCommand cmd1 = sqlcon.CreateCommand();
                    cmd1.CommandText = "select count(*) from KhachHang where MaKH=@MaKH and HoTenKH=@TenKH";
                    cmd1.Parameters.AddWithValue("@MaKH", txt_makh_tct.Text);
                    cmd1.Parameters.AddWithValue("@TenKH", txt_tenkh_tct.Text);
                    Da = new SqlDataAdapter(cmd1);
                    Dt = new DataTable();
                    Da.Fill(Dt);
                    cmd1.ExecuteNonQuery();
                    if(Dt.Rows[0][0].ToString()=="1")
                    {
                        Cmd = sqlcon.CreateCommand();
                        Cmd.CommandText = "select * from KhachHang where MaKH=@MaKH and HoTenKH=@TenKH";
                        Cmd.Parameters.AddWithValue("@MaKH", txt_makh_tct.Text);
                        Cmd.Parameters.AddWithValue("@TenKH", txt_tenkh_tct.Text);
                        Da = new SqlDataAdapter(Cmd);
                        Dt = new DataTable();
                        Da.Fill(Dt);
                        Cmd.ExecuteNonQuery();
                        dgv_tct.DataSource = Dt;
                            sqlcon.Close();
                            MessageBox.Show("Succes");
                    
                    }
                    else
                    {
                        MessageBox.Show("Thông Tin bạn nhập chưa chính xác");
                                                    sqlcon.Close();

                    }
                    
                }
                else
                {
                    MessageBox.Show("Nhập Chưa đủ Thông Tin", "ERROR");
                    sqlcon.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form_KH_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLTTVDataSet.KHACHHANG' table. You can move, or remove it, as needed.
            this.kHACHHANGTableAdapter.Fill(this.qLTTVDataSet.KHACHHANG);
            sqlcon = new SqlConnection(Conn);
        }

        private void btn_gui_GopY_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_tengopy.Text != "" & txt_lienhegopy.Text != "" & txt_noidunggopy.Text != "" & txt_vandegopy.Text != "")
                {
                    sqlcon = new SqlConnection(Conn);
                    SqlCommand cmd = sqlcon.CreateCommand();
                    cmd.CommandText = "insert into GopY(Ten_GopY,LienHe_GopY,VanDe_GopY,NoiDung_GopY)  VALUES (@tengopy,@lienhegopy,@vandegopy,@noidunggopy)";
                    sqlcon.Open();
                    cmd.Parameters.AddWithValue("@tengopy", txt_tengopy.Text);
                    cmd.Parameters.AddWithValue("@lienhegopy", txt_lienhegopy.Text);
                    cmd.Parameters.AddWithValue("@vandegopy", txt_vandegopy.Text);
                    cmd.Parameters.AddWithValue("@noidunggopy", txt_noidunggopy.Text);
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();
                    MessageBox.Show("Record Inserted Successfully");

                }
                else
                {
                    MessageBox.Show("Vui lòng Nhập Hết Thông tin");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form_KH_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
