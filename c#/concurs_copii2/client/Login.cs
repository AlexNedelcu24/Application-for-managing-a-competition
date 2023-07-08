using System;
using System.Windows.Forms;

namespace client
{
    public partial class Login : Form
    {
        private ClientCtrl ctrl;
        public Login(ClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }

        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            String user = textBoxUsername.Text;
            String pass = textBoxPassword.Text;
            
            if (user.Length == 0 || pass.Length == 0)
            {
                MessageBox.Show("Please complete all fields !");
                return;
            }
            
            try
            {
                ctrl.login(user, pass);
                //labelWrong.Text = "Wrong username or password!";
                Window win = new Window(ctrl);
                win.Text = "Window for " + user;
                win.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Login Error " + ex.Message/*+ex.StackTrace*/, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}