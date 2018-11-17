using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace MyWebStudio
{
    public partial class EmployeeInfo : Form
    {
        public static int selectedEmloyeeId;
        public int id;
        public EmployeeInfo()
        {
            InitializeComponent();
            id = selectedEmloyeeId;
            SQL.myCommand.CommandText = "select name,second_name,rating from employee where id = " + id.ToString() + ";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    label1.Text = SQL.MyDataReader.GetString(0);
                    label2.Text = SQL.MyDataReader.GetString(1);
                    label3.Text = SQL.MyDataReader.GetInt32(2).ToString();
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            SQL.myCommand.CommandText = "select name,value from employeeinfo where employee_id = " + id.ToString() + ";";
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    dataGridView1.Rows.Add(SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(0));
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }

        private void EmployeeInfo_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
