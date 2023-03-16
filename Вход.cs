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
using System.IO;
using New_work_1;

namespace Trade
{
    public partial class formLogin : Form
    {
        static string connectionString = "Data Source=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=DESKTOP-Q5KENKS\\SQLEXPRESS;Initial Catalog=Trade1;Data Source=DESKTOP-Q5KENKS\\SQLEXPRESS";
        static SqlConnection connection = new SqlConnection(connectionString);
        int count = 0;
        int v = 2;
        int time = 3000;
        int captcha = 0;

        public formLogin()
        {
            InitializeComponent();
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void InputButton_Click(object sender, EventArgs e) //С помощью данной кнопки производится вход в программу 
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "Login";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserLogin", LoginText.Text);
            command.Parameters.AddWithValue("@UserPassword", PasswordText.Text);
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                
                this.Hide();
                Товары newForm = new Товары();
                newForm.Show();
                
            }
            else
            {
                MessageBox.Show("Вы не смогли войти!");
                TimerPassword.Interval = 1000;
                count++;
                if (count == v)
                {
                     InputButton.Enabled = false;
                     TimerPassword.Enabled = true;
                     MessageBox.Show("Введите каптчу! ");

                    pictureBox2.Visible = true;
                    textBox1.Visible = true;

                }
            }

            connection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //С помощью изменения проверки пароль скрывается 
        {
            if (PassChar.Checked)
            {
                PasswordText.UseSystemPasswordChar = true;
            }
           else
            {
                PasswordText.UseSystemPasswordChar = false;
            }

        }

        private void TimerPassword_Tick(object sender, EventArgs e)
        {
            InputButton.Enabled = true;
 
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)//С помощью captcha производится повторная попытка входа
        {
            if (captcha == 123)
            {
                InputButton.Enabled = true;
            }
            else
            {
                InputButton.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void LoginText_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PasswordText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
