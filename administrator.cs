using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
namespace MyWebStudio
{
    public partial class administrator : Form
    {
        static public int SelectedIndexOfProjectSection;
        static public int SelectedIndexOfProjectInfo;
        static public int SelectedIndexOfProject;
        public administrator()
        { 
            InitializeComponent();   
            UpdateListOfProject();
            UpdateListOfEmployee();
            UpdateListOfEmployeeInProject();
        } 
        private void button1_Click(object sender, EventArgs e)
        {
            Form f = new NewProject();
            f.ShowDialog();
            UpdateListOfProject();
        }
        private void UpdateListOfProject()
        {   
            dataGridView1.Rows.Clear();         
            SQL.myCommand.CommandText = "select id,name,start_date,end_date,release_date,status from project;";
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
            SQL.myCommand.CommandText = "select id,name,value from projectinfo where project_id = "+ProjectID.ToString()+";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();

                while (SQL.MyDataReader.Read())
                {
                    dataGridView2.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2));
                }
                SQL.MyDataReader.Close();
              
            }
            catch { };
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
        private void administrator_Load(object sender, EventArgs e){        }
        private void UpdateListOfEmployee()
        {
            SQL.myCommand.CommandText = "Select id,second_name,name from employee;";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                dataGridView3.Rows.Clear(); //очищаем список сотрудников справа
                while (SQL.MyDataReader.Read())
                {
                    dataGridView3.Rows.Add(SQL.MyDataReader.GetInt32(0),SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2));
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex){ MessageBox.Show(ex.ToString()); }
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
        private void button2_Click(object sender, EventArgs e)
        {
            var f = new NewProjectInfo();
            f.ShowDialog();
            UpdateProjectInfo(SelectedIndexOfProject);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Удаление проекта", MessageBoxButtons.YesNo).ToString() == "No") return;
            SQL.myCommand.CommandText = "delete from project where id =" + SelectedIndexOfProject.ToString() + ";";
            try
            {
                SQL.myCommand.ExecuteNonQuery();
                UpdateListOfProject();
                dataGridView2.Rows.Clear(); //очищаем список ресурсов, а то мусор с выбранного удаленного объекта остается
            }
            catch { }   
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedIndexOfProject = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()); // номер текущего выбранного индекса проекта
                UpdateProjectInfo(SelectedIndexOfProject);
                UpdateListOfEmployeeInProject();
                UpdateListOfProjectSection();
                panel2.Visible = true; // 
                /*label1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                dateTimePicker3.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());*/
                UpdateProjectStatus();
            } 
        }
        private void button4_Click(object sender, EventArgs e)
        {   
            if (MessageBox.Show("Вы уверены?","Удаление информации о проекте",MessageBoxButtons.YesNo).ToString()=="No") return;            
            SQL.myCommand.CommandText = "delete from projectinfo where id =" + SelectedIndexOfProjectInfo.ToString() + ";";
            try
            {
                SQL.myCommand.ExecuteNonQuery();
                UpdateProjectInfo(SelectedIndexOfProject);
            }
            catch { }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedIndexOfProjectInfo = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
        private void dataGridView2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try { Process.Start(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString()); } catch { }; 
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var f = new NewEmployee();
            f.ShowDialog();
            UpdateListOfEmployee();
        }
        private void dataGridView3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            //dataGridView2.DoDragDrop(dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),DragDropEffects.Copy);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow elem in dataGridView4.Rows)
            {
                
                if (Int32.Parse(elem.Cells[0].Value.ToString()) == Int32.Parse(dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString()))
                {
                    return;
                }
            }
            dataGridView4.Rows.Add(dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[1].Value.ToString());
            SQL.myCommand.CommandText = "insert into employeeinproject values("+ SQL.EnterParam(Int32.Parse(dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString()), SelectedIndexOfProject, "Работник") +");";
           // MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex){ MessageBox.Show(ex.ToString()); }
        }
        private void UpdateListOfEmployeeInProject()
        {
            dataGridView4.Rows.Clear();
            SQL.myCommand.CommandText = "SELECT employeeinproject.employee_id,employee.second_name,employee.name,employeeinproject.role FROM employeeinproject inner join employee on employeeinproject.employee_id=employee.id where employeeinproject.project_id =  " + SelectedIndexOfProject + ";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                while (SQL.MyDataReader.Read())
                {
                    dataGridView4.Rows.Add(SQL.MyDataReader.GetInt32(0), SQL.MyDataReader.GetString(1), SQL.MyDataReader.GetString(2), SQL.MyDataReader.GetString(3));
                 
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex){ MessageBox.Show(ex.ToString()); }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            
            EmployeeInfo.selectedEmloyeeId = Int32.Parse(dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString());
            EmployeeInfo f = new EmployeeInfo();
            f.Show();
        }
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedIndexOfProjectSection = Int32.Parse(dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString());
                UpdateProjectStep();
            }
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
                " from projectstep inner join employee on employee.id = projectstep.employee_id where projectstep.ProjectSection_id = " + SelectedIndexOfProjectSection.ToString() +";";
            //MessageBox.Show(SQL.myCommand.CommandText);
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
        private void dataGridView6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.String)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        private void dataGridView6_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                dataGridView6.Rows.Add(e.Data.GetData(typeof(System.String)));
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            newSection.selectedProjectId = SelectedIndexOfProject;
            var f = new newSection();
            f.ShowDialog();
            UpdateListOfProjectSection();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells[0].RowIndex < 0) return;
            SQL.myCommand.CommandText = "delete from projectsection where id = " + dataGridView5.Rows[dataGridView5.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + ";";
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            UpdateListOfProjectSection();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            NewStep.selectedSectionId = SelectedIndexOfProjectSection;
            NewStep.selectedProjectId = SelectedIndexOfProject;
            var f = new NewStep();
            f.ShowDialog();
            UpdateProjectStep();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "delete from projectstep where id = " + dataGridView6.Rows[dataGridView6.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + ";";
            
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            UpdateListOfProjectSection();
            UpdateProjectStep();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "delete from employee where id = " + dataGridView3.Rows[dataGridView3.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + ";";
            //MessageBox.Show(SQL.myCommand.CommandText); return;
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            UpdateListOfProjectSection();
            UpdateProjectStep();
            UpdateListOfEmployee();
             
        }

        private void button5_Click(object sender, EventArgs e)
        {
            

            SQL.myCommand.CommandText = "UPDATE `project` SET `start_date` = "+SQL.StringToQueryFormat(dateTimePicker1.Text)+ ", `end_date` = " + SQL.StringToQueryFormat(dateTimePicker2.Text) + ", `release_date` = " + SQL.StringToQueryFormat(dateTimePicker3.Text) + ", `status` = " + SQL.StringToQueryFormat(textBox1.Text) + " WHERE(`id` = "+ SelectedIndexOfProject+");";
           // MessageBox.Show(SQL.myCommand.CommandText); return;
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            UpdateProjectStatus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BugList.sectionID = SelectedIndexOfProjectSection;
            var f = new BugList();
            f.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            NewBug.SectionId = SelectedIndexOfProjectSection;
            var f = new NewBug();
            f.ShowDialog();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.Text);
            SQL.myCommand.CommandText = "UPDATE `employeeinproject` SET `role` = "+SQL.StringToQueryFormat(comboBox1.Text) +" WHERE (`employee_id` = "+SQL.StringToQueryFormat(dataGridView4.Rows[dataGridView4.SelectedCells[0].RowIndex].Cells[0].Value.ToString()) +") and (`Project_id` = "+SelectedIndexOfProject+");";
            try
            {
                SQL.myCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
            UpdateListOfEmployeeInProject();
        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}