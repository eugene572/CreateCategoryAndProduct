using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtID.Text==""||txtPwd.Text=="")
            {
                MessageBox.Show("아이디 혹은 비밀번호를 모두 입력해주세요.");
            }
            else { 
                if(txtID.Text=="admin"&& txtPwd.Text=="admin")
                {
                    MessageBox.Show("로그인을 성공하였습니다.");
                    main m = new main();
                    m.Show();
                    txtID.Text = txtPwd.Text = "";
                    
                }
                else
                {
                    MessageBox.Show("로그인을 실패하였습니다.");
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
