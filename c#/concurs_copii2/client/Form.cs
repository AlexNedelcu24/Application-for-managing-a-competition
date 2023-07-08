using concurs_copii2.domain;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace concurs_copii2
{
    public partial class Form : System.Windows.Forms.Form
    {
        static service.Service service;

        public Form()
        {
            InitializeComponent();
        }

        public Form(service.Service ser)
        {
            service = ser;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(password_text.Text))
            {
               label3.Text = "Wrong username or password!";
            }
            else
            {
                label3.Text = "Succes!";
                try
                {
                    User user = service.FindLog(textBox1.Text, password_text.Text);
                    label3.Text = "Succes!";
                    Form2 form = new Form2(service);
                    this.Hide();
                    form.Show();
                    //this.Close();
                }
                catch (ArgumentException)
                {
                    label3.Text = "Wrong username or password!";
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_text_TextChanged(object sender, EventArgs e)
        {

        }

  
    }
}