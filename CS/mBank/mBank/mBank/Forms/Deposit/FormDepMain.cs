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
    public partial class FormDepMain : Form
    {
        Database database = new Database();

        private int CountDep()
        {
            int count = 0;
            using (SqlCommand getCommand = new SqlCommand("pCountDep", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                getCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        count = dr.GetInt32(dr.GetOrdinal("Количество депозитов"));
                    }
                    StaticInfBank.count_Dep = count;
                }
            }
            return count;
        }

        private void CountTypeDep()
        {
            int count = 0;
            using (SqlCommand getCommand = new SqlCommand("pCountTypeDep", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        count = dr.GetInt32(dr.GetOrdinal("Количество типов"));
                    }
                    StaticInfBank.count_TypeDep = count;
                }
            }
        }

        private double SumDep()
        {
            double sum = 0;
            using (SqlCommand getCommand = new SqlCommand("pSumDep", database.sqlConnection))
            {
                getCommand.CommandType = CommandType.StoredProcedure;
                getCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                using (SqlDataReader dr = getCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        sum = dr.GetDouble(dr.GetOrdinal("Сумма"));
                    }
                }
            }
            return sum;
        }

        private async Task SumEachDep(int[] arraySum, string[] arrayName)
        {
            SqlDataReader sqlReader = null;
            int i = 0;
            using (SqlCommand getHistCommand = new SqlCommand("pSumEachDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                string login = StaticInfBank.login;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["Номер депозита"]),
                        Convert.ToString(sqlReader["Название"]),
                        Convert.ToString(sqlReader["Сумма"]),
                    });
                    arraySum[i] = Convert.ToInt32(sqlReader["Сумма"]);
                    arrayName[i] = Convert.ToString(sqlReader["Номер депозита"]) + " №" + Convert.ToString(sqlReader["Название"]);
                    i++;
                    listView1.Items.Add(item);
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private void AddPoint(int[] arraySum, string[] arrayName)
        {
            Color[] arrColor = { Color.LawnGreen, Color.MediumOrchid, Color.Crimson, Color.DarkOrange, Color.DarkTurquoise, Color.Gold, Color.MediumBlue, Color.DeepPink, Color.Olive, Color.SlateGray, Color.GreenYellow, Color.Tomato, Color.Maroon };
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < arraySum.Length; i++)
            {
                chart1.Series[0].Points.AddY(arraySum[i]);
                //chart1.Series[0].Points[i].LegendText = arrayName[i];
                chart1.Series[0].Points[i].Color = arrColor[i];
                chart1.Series[0].Points[i].LabelToolTip = arrayName[i];
                //chart1.Legends[0].ForeColor = arrColor[i];
                chart1.Series[0].Points[i].LabelBackColor = arrColor[i];
            }
        }



        public FormDepMain()
        {
            InitializeComponent();
        }

        private async void FormDepMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            database.openConnection();
            CountTypeDep();
            label4.Text = Convert.ToString(CountDep());
            label2.Text = Convert.ToString(SumDep());

            int[] arraySum = new int[StaticInfBank.count_Dep];
            string[] arrayName = new string[StaticInfBank.count_Dep];

            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Номер депозита");
            listView1.Columns.Add("Название");
            listView1.Columns.Add("Сумма");

            await SumEachDep(arraySum, arrayName);
            AddPoint(arraySum, arrayName);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void FormDepMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
