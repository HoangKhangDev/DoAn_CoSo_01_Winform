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
    public partial class frm_login : Form
    {
        public frm_login()
        {
            InitializeComponent();
        }
        String Conn = @"Data Source=ADMIN;Initial Catalog=QLTTV;Integrated Security=True";
        SqlConnection sqlcon;
        SqlDataAdapter Da;
        DataTable Dt;
        SqlCommand Cmd;
        private void button1_Click(object sender, EventArgs e)
        {
            if(txt_id_dn.Text!=""&txt_pass_dn.Text!="")
            {
                sqlcon.Open();
                Cmd = sqlcon.CreateCommand();
                Cmd.CommandText = "select count(*) from account where TaiKhoan=@taikhoan and MatKhau=@matkhau";
                Cmd.Parameters.AddWithValue("@taikhoan", txt_id_dn.Text);
                Cmd.Parameters.AddWithValue("@matkhau", txt_pass_dn.Text);
                Cmd.ExecuteNonQuery();
                Da = new SqlDataAdapter(Cmd);
                Dt = new DataTable();
                Da.Fill(Dt);
                if (Dt.Rows[0][0].ToString() == "1")
                {

                    this.Hide();
                    MessageBox.Show("Login succes");
                    Form1 frm = new Form1();
                    frm.ShowDialog();
                    this.Close();


                }
                else 
                {
                    MessageBox.Show("Login fail, ID or Password not correct");
                    sqlcon.Close();
                }
            }  
            else
            {
                MessageBox.Show("Vui lòng điền đủ thông tin");
                sqlcon.Close();
            }


            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form_KH frm = new Form_KH();
            frm.ShowDialog();
            this.Close();
          
        }

        private void frm_login_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            sqlcon = new SqlConnection(Conn);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát không?", "Thông Báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
