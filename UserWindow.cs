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
using System.Diagnostics;

namespace MyWebStudio
{
    public partial class UserWindow : Form
    {
        static public int SelectedIndexOfProjectSection;
        static public int SelectedIndexOfProjectInfo;
        static public int SelectedIndexOfProject;
        static public int SelectedIndexOfEmployee;//как его получить?
        private void UpdateListOfProject(int EmployeeID)
        {
            dataGridView1.Rows.Clear();
            SQL.myCommand.CommandText = " SELECT * FROM project inner join employeeinproject on project.id = employeeinproject.Project_id where employeeinproject.employee_id = " + SelectedIndexOfEmployee.ToString() + ";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    dataGridView1.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2), SQL.MyDataReader.GetString(3), SQL.MyDataReader.GetString(4), SQL.MyDataReader.GetString(5));
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException e) { MessageBox.Show(e.ToString()); };
        }
        private void UpdateProjectInfo(int ProjectID)
        {
            dataGridView2.Rows.Clear();
            SQL.myCommand.CommandText = "select id,name,value from projectinfo where project_id = " + ProjectID.ToString() + ";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();

                while (SQL.MyDataReader.Read())
                {
                    dataGridView2.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2));
                }
                SQL.MyDataReader.Close();

            }
            catch (MySqlException e) { MessageBox.Show(e.Message); };
        }
        private void UpdateProjectStatus()
        {
            SQL.myCommand.CommandText = "Select start_date,end_date,release_date,status from project where id = " + SelectedIndexOfProject.ToString();
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();

                while (SQL.MyDataReader.Read())
                {
                    // dataGridView2.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2));
                    dateTimePicker1.Value = SQL.MyDataReader.GetDateTime(0);
                    dateTimePicker2.Value = SQL.MyDataReader.GetDateTime(1);
                    dateTimePicker3.Value = SQL.MyDataReader.GetDateTime(2);
                    textBox1.Text = SQL.MyDataReader.GetString(3);
                }
                SQL.MyDataReader.Close();

            }
            catch { };

        }
        private void UpdateListOfProjectSection()
        {
            SQL.myCommand.CommandText = "Select id,name from projectsection where project_id = " + SelectedIndexOfProject;

            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                dataGridView5.Rows.Clear(); //очищаем список сотрудников справа

                while (SQL.MyDataReader.Read())
                {
                    dataGridView5.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1));
                }
                SQL.MyDataReader.Close();

            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }
        private void UpdateProjectStep()
        {
            dataGridView6.Rows.Clear();
            SQL.myCommand.CommandText =
                "Select " +
                "projectstep.ID," +
                "projectstep.name," +
                "projectstep.dev_time," +
                "projectstep.fact_time," +
                "projectstep.end_date," +
                "projectstep.fact_date," +
                "projectstep.test_date," +
                "projectstep.status," +
                "concat(employee.name,' ',employee.second_name)," +
                "projectstep.comment" +
                " from projectstep inner join employee on employee.id = projectstep.employee_id where projectstep.ProjectSection_id = " + SelectedIndexOfProjectSection.ToString() + ";";
            
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    /*int empId = SQL.MyDataReader.GetInt32(8);
                    SQL.myCommand.CommandText = "select  CONCAT(name,' ',second_name) from employee where id = " + empId.ToString() + ";";
                    string name = SQL.myCommand.ExecuteScalar().ToString();*/
                    dataGridView6.Rows.Add(
                        SQL.MyDataReader.GetInt32(0),
                        SQL.MyDataReader.GetString(1),
                        SQL.MyDataReader.GetInt32(2),
                        SQL.MyDataReader.GetInt32(3),
                        SQL.MyDataReader.GetMySqlDateTime(4),
                        SQL.MyDataReader.GetMySqlDateTime(5),
                        SQL.MyDataReader.GetMySqlDateTime(6),
                        SQL.MyDataReader.GetString(7),
                        SQL.MyDataReader.GetString(8),
                        SQL.MyDataReader.GetString(9)
                        );
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }
        public UserWindow()
        {
            InitializeComponent();
            UpdateListOfProject(SelectedIndexOfEmployee);
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedIndexOfProject = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()); // номер текущего выбранного индекса проекта
                UpdateProjectInfo(SelectedIndexOfProject);
                UpdateListOfProjectSection();
                UpdateProjectStatus();
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedIndexOfProjectSection = Int32.Parse(dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString());
                UpdateProjectStep();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
