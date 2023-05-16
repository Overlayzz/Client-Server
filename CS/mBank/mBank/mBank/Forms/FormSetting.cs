using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mBank.Forms
{

    public partial class FormSetting : Form
    {
        Database database = new Database();
        private void CheckSymbLet(KeyPressEventArgs e) //провека на ввод только букв
        {
            string Symbol = e.KeyChar.ToString();

            if (Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]").Success)
            {
                return;
            }

            //Управляющие клавиши <Backspace>, <Enter> и т.д.
            if (Char.IsControl(e.KeyChar))
            {
                return;
            }

            e.Handled = true;
        }

        private void CheckSymbNum(KeyPressEventArgs e) //провека на ввод только цифр
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                return;
            }

            //Управляющие клавиши <Backspace>, <Enter> и т.д.
            if (Char.IsControl(e.KeyChar))
            {
                return;
            }

            //Остальное запрещено
            e.Handled = true;
        }

        private void CheckSymb(KeyPressEventArgs e) //проверка на ввод букв, цифр, точки и знака @
        {
            string Symbol = e.KeyChar.ToString();

            if (Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]|[0-9]|[.]|[@]").Success)
            {
                return;
            }

            //Управляющие клавиши <Backspace>, <Enter> и т.д.
            if (Char.IsControl(e.KeyChar))
            {
                return;
            }

            e.Handled = true;
        }

        private async void UpdateLoginPas() //обновление логина и пароля (вызов функций)
        {
            if (textBox5.Text != "" && textBox5.Text.Contains('@') && (textBox5.Text.Length > 7))
            {
                await UpdateLoginBD();
            }
            if (textBox6.Text != "" && (textBox6.Text.Length > 5))
            {
                await UpdatePasswordBD();
            }
        }

        private async Task UpdateLoginBD() //запрос для обновления логина
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pUpdateLogin", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@loginOld", StaticInfBank.login);
                getHistCommand.Parameters.AddWithValue("@loginNew", textBox5.Text);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task UpdatePasswordBD() //запрос для обновления пароля
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pUpdatePassword", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                getHistCommand.Parameters.AddWithValue("@password", textBox6.Text);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task UpdatePassportBD() //запрос для обновления паспортных данных
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pUpdatePassport", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                getHistCommand.Parameters.AddWithValue("@passport", textBox4.Text);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async Task UpdateFIOBD(string fio) //запрос для обновления паспортных данных
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pUpdateFIO", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                getHistCommand.Parameters.AddWithValue("@FIO", fio);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async Task PrintFIO() //запрос для обновления паспортных данных
        {
            string fio;
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pPrintFIO", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    fio = sqlReader.GetString(sqlReader.GetOrdinal("FIO"));
                    label2.Text = fio;
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        public FormSetting()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckSymbLet(e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckSymbLet(e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckSymbLet(e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckSymbNum(e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckSymb(e);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label15.Visible = false;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            label16.Visible = false;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label17.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if ((textBox5.Text.Contains('@') && (textBox5.Text.Length > 7)) || (textBox6.Text.Length > 5))
            {
                UpdateLoginPas();
            }
            else if (textBox6.Text == "" && textBox5.Text == "")
            {
                label15.Visible = false;
                label16.Visible = false;
            }
            else
            {
                label15.Visible = true;
                label16.Visible = true;
            }

            if (textBox4.Text.Length == 10)
            {
                //вызов обновления паспорта
                await UpdatePassportBD();
            }
            else if (textBox4.Text == "")
            {
                label17.Visible = false;
            }
            else
            {
                label17.Visible = true;
            }

            if ((textBox1.Text != "") && (textBox2.Text != "") && (textBox3.Text != ""))
            {
                //вызов обновления ФИО
                string FIO = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
                await UpdateFIOBD(FIO);
                await PrintFIO();
            }
            else if ((textBox1.Text == "") && (textBox2.Text == "") && (textBox3.Text == ""))
            {
                label14.Visible = false;
            }
            else
            {
                label14.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label14.Visible = false;
        }

        private async void FormSetting_Load(object sender, EventArgs e)
        {
            database.openConnection();
            await PrintFIO();
        }

        private void FormSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
