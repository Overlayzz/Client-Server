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

namespace mBank.Forms.Deposit
{
    public partial class FormDepHistory : Form
    {
        int[] arrayId = new int[StaticInfBank.count_Dep];
        Database database = new Database();

        private async Task LoadHistDepInBox(int[] arrayId) //запрос к бд для записи в выпадающий список
        {
            int idDep = 1;
            string name = "";
            string untStr = "";
            SqlDataReader sqlReader = null;
            int i = 0;
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
                    arrayId[i] = Convert.ToInt32(sqlReader["id_deposit"]);
                    comboBox1.Items.Add(untStr);
                    i++;
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }
        private async Task LoadHistDepInList() //запрос к бд для записи в таблицу
        {
            SqlDataReader sqlReader = null;
            using (SqlCommand getHistCommand = new SqlCommand("pHistDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                string login = StaticInfBank.login;
                getHistCommand.Parameters.AddWithValue("@login", StaticInfBank.login);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["name"]),
                        Convert.ToString(sqlReader["id_deposit"]),
                        Convert.ToString(sqlReader["Вид операции"]),
                        Convert.ToString(sqlReader["Сумма"]),
                        Convert.ToString(sqlReader["Дата"])
                    });

                    listView1.Items.Add(item);
                }

                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }

            }
        }

        private async Task LoadAndFiltr() //запрос к бд для фильтрации таблицы по выбранному элементу выпадающего списка
        {
            SqlDataReader sqlReader = null;
            using (SqlCommand getHistCommand = new SqlCommand("pFiltrHistDep", database.sqlConnection))
            {
                getHistCommand.CommandType = CommandType.StoredProcedure;
                int id = arrayId[comboBox1.SelectedIndex - 1];
                getHistCommand.Parameters.AddWithValue("@indDep", id);
                sqlReader = await getHistCommand.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                        Convert.ToString(sqlReader["name"]),
                        Convert.ToString(sqlReader["id_deposit"]),
                        Convert.ToString(sqlReader["Вид операции"]),
                        Convert.ToString(sqlReader["Сумма"]),
                        Convert.ToString(sqlReader["Дата"])
                    });

                    listView1.Items.Add(item);
                }

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


        public FormDepHistory()
        {
            InitializeComponent();
        }

        private async void FormDepHistory_Load(object sender, EventArgs e)
        {
            database.openConnection();

            await LoadHistDepInBox(arrayId);
            comboBox1.SelectedIndex = 0;
            comboBox1.KeyPress += OnKeyPress;
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.Clear();
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Название");
            listView1.Columns.Add("Номер депозита");
            listView1.Columns.Add("Вид операции");
            listView1.Columns.Add("Сумма");
            listView1.Columns.Add("Дата");
            if (comboBox1.SelectedIndex == 0)
            {
                await LoadHistDepInList();
            }
            else
            {
                await LoadAndFiltr();
            }
            //comboBox1.Items[comboBox1.SelectedIndex];


            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        private void FormDepHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.closeConnection();
        }
    }
}
