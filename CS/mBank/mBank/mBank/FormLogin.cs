using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace mBank
{
    public partial class FormLogin : Form
    {
        Database database = new Database();
        int pw;
        bool hided;


        private Boolean CheckLogin()
        {
            int stringColumn = 0;
            using (SqlCommand getCommand = new SqlCommand("pCheckLogin", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                getCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        stringColumn = dr.GetInt32(dr.GetOrdinal("Количество записей"));
                    }
                }
            }
            if (stringColumn == 1)
            {
                return true;
            }
            return false;
        }

        private Boolean CheckLoginAndPas()
        {
            int stringColumn = 0;
            using (SqlCommand getCommand = new SqlCommand("pCheckLoginAndPas", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                getCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                getCommand.Parameters.AddWithValue("@password", StaticInfBank.password);
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        stringColumn = dr.GetInt32(dr.GetOrdinal("Количество записей"));
                    }
                }
            }
            if (stringColumn == 1)
            {
                return true;
            }
            return false;
        }





        public FormLogin()
        {
            InitializeComponent();
            pw = panel5.Width;
            hided = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Введите логин";
                return;
            }
            textBox1.ForeColor = Color.White;
            panel5.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                //textBox2.Text = "Введите пароль";
                return;
            }
            textBox2.ForeColor = Color.White;
            textBox2.PasswordChar = '*'; // convert text to *
            panel7.Visible = false;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.FromArgb(11, 129, 201);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Введите логин")
            {
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "Введите пароль" || textBox2.Text == "")
            {
                panel7.Visible = true;
                textBox2.Focus();
                return;
            }
            //textBox1.Text = "ivanovPavl@mail.ru"; // автозаполнение
            //textBox2.Text = "123456789";

            

            StaticInfBank.login = textBox1.Text;
            StaticInfBank.password = textBox2.Text;
            if (CheckLogin())
            {
                if (!CheckLoginAndPas())
                {
                    panel7.Visible = true;
                    textBox2.Focus();
                    return;
                }
            }
            else
            {
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            Hide();
            FormMainMenu form2 = new FormMainMenu();
            Forms.Deposit.FormDepHistory formDepHistory = new Forms.Deposit.FormDepHistory();
            form2.ShowDialog();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            database.openConnection();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
