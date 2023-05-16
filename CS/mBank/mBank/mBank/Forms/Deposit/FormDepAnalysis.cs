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
    public partial class FormDepAnalysis : Form
    {

        Database database = new Database();
        int[] arrayId = new int[StaticInfBank.count_Dep];
        //int[] arrayId = new int[StaticInfBank.count_TypeDep];

        private async Task LoadHistDepInBox() //запрос к бд для записи в выпадающий список
        {
            int idDep = 1;
            string name = "";
            string untStr = "";
            int i = 0;
            SqlDataReader sqlReader = null;
            using (SqlCommand getHistCommand = new SqlCommand("pHistDepType", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                string login = StaticInfBank.login;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    idDep = sqlReader.GetInt32(sqlReader.GetOrdinal("id_deposit"));
                    name = sqlReader.GetString(sqlReader.GetOrdinal("name"));
                    untStr = name + " №" + Convert.ToString(idDep);
                    arrayId[i] = idDep;
                    comboBox1.Items.Add(untStr);
                    i++;
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        public void CountTransactDep()
        {
            int count = 0;
            using (SqlCommand getCommand = new SqlCommand("pCountTransactDep", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                getCommand.Parameters.AddWithValue("@idDep", arrayId[comboBox1.SelectedIndex]);
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        count = dr.GetInt32(dr.GetOrdinal("Количество операций"));
                    }
                    StaticInfBank.count_OperDep = count;
                }
            }
        }

        private async Task LoadAnalysisInf(string nameProc, double[] arraySum, string[] arrayDate) //запрос к бд для считывания суммы и даты 
        {
            double Sum;
            DateTime date;
            int i = 0;
            SqlDataReader sqlReader = null;
            using (SqlCommand getHistCommand = new SqlCommand(nameProc, database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                getHistCommand.Parameters.AddWithValue("@idDep", arrayId[comboBox1.SelectedIndex]);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    arraySum[i] = sqlReader.GetDouble(sqlReader.GetOrdinal("Сумма"));
                    //arrayDate[i] = sqlReader.GetDateTime(sqlReader.GetOrdinal("Дата"));
                    date = sqlReader.GetDateTime(sqlReader.GetOrdinal("Дата"));
                    arrayDate[i] = date.ToShortDateString();
                    i++;
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private void AddPoint(double[] arraySum, string[] arrayDate)
        {
            chart1.ChartAreas[0].AxisY.Minimum = arraySum[0] - 5;
            chart1.ChartAreas[0].AxisY.Maximum = arraySum[StaticInfBank.count_OperDep-1] + 5;
            for (int i = 0; i < StaticInfBank.count_OperDep; i++)
            {
                this.chart1.Series[0].Points.AddXY(arrayDate[i], arraySum[i]);
                //this.chart1.Series[1].Points.AddXY(arrayDate[i], arraySum[0]);
            }
        }

        private async void LoadAndInsertData() 
        {
            string name = "";
            if (comboBox2.SelectedIndex == 0)
            {
                name = "pAnalysisSum";
            }
            else
            {
                name = "pAnalysisProc";
            }

            double[] arraySum = new double[StaticInfBank.count_OperDep];
            string[] arrayDate = new string[StaticInfBank.count_OperDep];

            await LoadAnalysisInf(name, arraySum, arrayDate);

            AddPoint(arraySum, arrayDate);
        }

        void OnKeyPress(object sender, KeyPressEventArgs e) //запрет записи
        {
            e.Handled = true;
        }



        public FormDepAnalysis()
        {
            InitializeComponent();
        }

        public async void FormDepAnalysis_Load(object sender, EventArgs e)
        {
            database.openConnection();

            await LoadHistDepInBox();

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox1.KeyPress += OnKeyPress;
            comboBox2.KeyPress += OnKeyPress;

            CountTransactDep();
            LoadAndInsertData();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadAndInsertData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadAndInsertData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            //this.chart1.Series[1].Points.Clear();
            CountTransactDep();
            LoadAndInsertData();
        }

        private void FormDepAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
