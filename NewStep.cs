using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWebStudio
{
    public partial class NewStep : Form
    {
        public static int selectedSectionId;
        public static int selectedProjectId;
        
        public int id,project_id;
        public NewStep()
        {
            InitializeComponent();
            id = selectedSectionId;
            project_id = selectedProjectId;
            
        }

        private void NewStep_Load(object sender, EventArgs e)
        {
            UpdateListOfEmployeeInProject();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "insert into projectstep values(default," + SQL.EnterParam(id, textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Text, dateTimePicker2.Text, dateTimePicker3.Text, textBox4.Text, textBox5.Text, dataGridView4.Rows[dataGridView4.SelectedCells[0].RowIndex].Cells[0].Value.ToString()) + ");";
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.myCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); };
            this.Close();
        }

        private void UpdateListOfEmployeeInProject()
        {
            dataGridView4.Rows.Clear();
            SQL.myCommand.CommandText = "SELECT employeeinproject.employee_id,employee.second_name,employee.name,employeeinproject.role FROM employeeinproject inner join employee on employeeinproject.employee_id=employee.id where employeeinproject.project_id =  " + selectedProjectId.ToString() + ";";
           // MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    dataGridView4.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2), SQL.MyDataReader.GetString(3));
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
