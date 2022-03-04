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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        String Conn = @"Data Source=ADMIN;Initial Catalog=QLTTV;Integrated Security=True";
        SqlConnection sqlcon;
        SqlDataAdapter Da;
        DataTable Dt;
        SqlCommand Cmd;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_matkhaucu_dmk.Text!=""&txt_matkhaumoi_dmk.Text!=""&txt_taikhoan_dmk.Text!=""&txt_xacnhanmatkhau_dmk.Text!="")
            {
                sqlcon.Open();
                Cmd = sqlcon.CreateCommand();
                Cmd.CommandText = "select count(*) from account where TaiKhoan=@taikhoan and MatKhau=@matkhau";
                Cmd.Parameters.AddWithValue("@taikhoan", txt_taikhoan_dmk.Text);
                Cmd.Parameters.AddWithValue("@matkhau", txt_matkhaucu_dmk.Text);
                Cmd.ExecuteNonQuery();
                Da = new SqlDataAdapter(Cmd);
                Dt = new DataTable();
                Da.Fill(Dt);
                if (Dt.Rows[0][0].ToString() != "1")
                {
                    sqlcon.Close();
                    MessageBox.Show("Tài Khoản Mật Khẩu bạn nhập chưa chính xác!!!");
                }
                else
                {
                    if(txt_matkhaumoi_dmk.Text==txt_xacnhanmatkhau_dmk.Text )
                    {
                        if(txt_matkhaumoi_dmk.Text!=txt_matkhaucu_dmk.Text)
                        {
                            SqlCommand cmd1 = sqlcon.CreateCommand();
                            cmd1.CommandText = "update account set MatKhau=@matkhau where TaiKhoan=@taikhoan";
                            cmd1.Parameters.AddWithValue("@taikhoan", txt_taikhoan_dmk.Text);
                            cmd1.Parameters.AddWithValue("@matkhau", txt_matkhaumoi_dmk.Text);
                            cmd1.ExecuteNonQuery();
                            if(cmd1.ExecuteNonQuery()>0)
                            {
                                MessageBox.Show("succes");
                                sqlcon.Close();

                            }
                            else
                            {
                                MessageBox.Show("fail");
                                sqlcon.Close();

                            }
                        }   
                        else
                        {
                            MessageBox.Show("Mật Khẩu mới và mật khẩu cũ phải khác nhau!!!");
                            sqlcon.Close();

                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật Khẩu Mới không giống nhau!");
                        sqlcon.Close();

                    }
                }    
            }
            else
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin");
                sqlcon.Close();

            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(Conn);
        }
    }
}
