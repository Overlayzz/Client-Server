using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace mBank.Forms.Deposit
{
    public partial class FormDepOper : Form
    {
        Database database = new Database();
        string[] arrayName = new string[StaticInfBank.count_TypeDep];
        int[] arrayId = new int[StaticInfBank.count_Dep];
        bool[] arrayInfIns = new bool[StaticInfBank.count_Dep]; //информация о возможности добаления в депозит денег
        bool[] arrayInfCut = new bool[StaticInfBank.count_Dep]; //информация о возможности снятия с депозита денег


        private async Task LoadHistDepInBox(string[] arrayName) //запрос к бд для записи в выпадающий список с информацией о вкладах
        {
            string name = "";
            double proc = 0;
            int term = 0;
            int termPay = 0;
            bool addPay = false;
            bool cutPay = false;
            int minSum = 0;
            string untStr = "";
            SqlDataReader sqlReader = null;
            int i = 0;
            using (SqlCommand getHistCommand = new SqlCommand("pInfEachDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    name = sqlReader.GetString(sqlReader.GetOrdinal("Название"));
                    proc = sqlReader.GetDouble(sqlReader.GetOrdinal("Процент"));
                    term = sqlReader.GetInt32(sqlReader.GetOrdinal("Срок"));
                    termPay = sqlReader.GetInt32(sqlReader.GetOrdinal("Выплаты"));
                    addPay = sqlReader.GetBoolean(sqlReader.GetOrdinal("Возможность добавлять"));
                    cutPay = sqlReader.GetBoolean(sqlReader.GetOrdinal("Возможность снимать"));
                    minSum = sqlReader.GetInt32(sqlReader.GetOrdinal("Минимальная сумма"));

                    if (minSum <= Convert.ToInt32(textBox1.Text))
                    {
                        untStr = "Название: " + name + ", %:" + Convert.ToString(proc) + ", Срок(мес):" + Convert.ToString(term) +
                            ", Выплаты(через дней):" + Convert.ToString(termPay) + ", Добавление $:" + Convert.ToString(addPay) + ", Снятие $:" + Convert.ToString(cutPay);
                        comboBox1.Items.Add(untStr);
                        arrayName[i] = Convert.ToString(sqlReader["Название"]);
                        i++;
                    }
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task InsertDataInDep(string[] arrayName) //запрос для создания записи о новом вкладе
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pInsertDataDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("nameDep", arrayName[comboBox1.SelectedIndex]);
                getHistCommand.Parameters.AddWithValue("summ_Deposit", Convert.ToInt32(textBox1.Text));
                getHistCommand.Parameters.AddWithValue("loginCl", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task InsertDepTransact() //запрос на добавление/снятие средств 
        {
            if (comboBox3.Text == "Снять деньги")
            {
                textBox2.Text = "-" + textBox2.Text;
            }

            SqlDataReader sqlReader = null;
            using (SqlCommand getHistCommand = new SqlCommand("pInsertMoneyInDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                //getHistCommand.Parameters.AddWithValue("@table", dataTypeDep);
                getHistCommand.Parameters.AddWithValue("name_TypeDeposit", comboBox3.Text);
                getHistCommand.Parameters.AddWithValue("summ_Deposit", Convert.ToInt32(textBox2.Text));
                getHistCommand.Parameters.AddWithValue("id_Deposit", arrayId[comboBox2.SelectedIndex]);
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task LoadCheckInfDepInBox() //запрос к бд для записи в выпадающий список
        {
            int idDep = 1;
            string name = "";
            string untStr = "";
            SqlDataReader sqlReader = null;
            int i = 0;
            using (SqlCommand getHistCommand = new SqlCommand("pCheckInfTypeDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                string login = StaticInfBank.login;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    idDep = sqlReader.GetInt32(sqlReader.GetOrdinal("Номер депозита"));
                    name = sqlReader.GetString(sqlReader.GetOrdinal("Название"));
                    untStr = name + " №" + Convert.ToString(idDep);
                    arrayId[i] = Convert.ToInt32(sqlReader["Номер депозита"]);
                    arrayInfIns[i] = Convert.ToBoolean(sqlReader["Добавление"]);
                    arrayInfCut[i] = Convert.ToBoolean(sqlReader["Снятие"]);

                    comboBox2.Items.Add(untStr);
                    i++;
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }


        private async Task CloseDep() //запрос для закрытия вклада
        {
            SqlDataReader sqlReader = null;

            using (SqlCommand getHistCommand = new SqlCommand("pCloseDeposit", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@id_deposit", Convert.ToInt32(arrayId[comboBox2.SelectedIndex]));
                sqlReader = await getHistCommand.ExecuteReaderAsync();

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        void OnKeyPress(object sender, KeyPressEventArgs e) //запрет записи
        {
            e.Handled = true;
        }


        /*_____События объектов формы______*/

        public FormDepOper()
        {
            InitializeComponent();
        }

        public void FormDepOper_Load(object sender, EventArgs e)
        {
            database.openConnection();

            comboBox1.Enabled = false;
            button3.Enabled = false;
            comboBox3.Enabled = false;
            textBox2.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            comboBox1.KeyPress += OnKeyPress;
            comboBox2.KeyPress += OnKeyPress;
            comboBox3.KeyPress += OnKeyPress;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if(Convert.ToInt32( textBox1.Text) >= 10000)
                {
                    FormDepAnalysis formDepAnalysis = new FormDepAnalysis();
                    MsgBox msgbox = new MsgBox();
                    msgbox.lblTextMsg.Text = "Подтверждаете открытие нового вклада?";
                    msgbox.ShowDialog(); //вызов диалогового окна
                    if (msgbox.clickBtnOK == true)
                    {
                        await InsertDataInDep(arrayName);
                        //formDepAnalysis.CountTransactDep();
                    }
                }
                else
                {
                    label3.Enabled = true;
                    label3.ForeColor = Color.Red;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Text = "";
            panel2.Visible = false;
            panel3.Visible = true;
            await LoadCheckInfDepInBox();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {
            label3.ForeColor = Color.LightGray;
            FormDepOper formDepOper = new FormDepOper();
            comboBox1.Items.Clear();

            if (textBox1.Text != "")
            {
                comboBox1.Enabled = true;
                if(Convert.ToInt32(textBox1.Text) >= 10000)
                {
                    await LoadHistDepInBox(arrayName);
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            comboBox3.Enabled = true;
            button5.Enabled = true;
            if ((arrayInfIns[comboBox2.SelectedIndex] == true) && ((arrayInfCut[comboBox2.SelectedIndex] == true)))
            {
                comboBox3.Items.Add("Положить деньги");
                comboBox3.Items.Add("Снять деньги");
            }
            else if (arrayInfIns[comboBox2.SelectedIndex] == true)
            {
                comboBox3.Items.Add("Положить деньги");
            }
            else if (arrayInfCut[comboBox2.SelectedIndex] == true)
            {
                comboBox3.Items.Add("Снять деньги");
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button4.Enabled = true;
        }

        private void FormDepOper_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private async void button4_Click(object sender, EventArgs e)
        {
            FormDepAnalysis formDepAnalysis = new FormDepAnalysis();
            MsgBox msgbox = new MsgBox();
            msgbox.ShowDialog(); //вызов диалогового окна
            if (msgbox.clickBtnOK == true)
            {
                await InsertDepTransact();
                //formDepAnalysis.CountTransactDep();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            FormDepAnalysis formDepAnalysis = new FormDepAnalysis();
            MsgBox msgbox = new MsgBox();
            msgbox.lblTextMsg.Text = "Уверены, что хотите закрыть вклад?";
            msgbox.ShowDialog(); //вызов диалогового окна
            if (msgbox.clickBtnOK == true)
            {
                await CloseDep();
                //formDepAnalysis.CountTransactDep();
            }
        }
    }
}
