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
    public partial class BugList : Form
    {
        public static int sectionID;
        int id;
        public BugList()
        {
            InitializeComponent();
            id = sectionID;
            UpdateBugList();
        }

        private void BugList_Load(object sender, EventArgs e)
        {

        }

        private void UpdateBugList()
        {
            SQL.myCommand.CommandText = "SELECT projectsection.name,buglist.bug_num,buglist.description,buglist.screenshot_url,buglist.iteration,buglist.status,buglist.manager_comment,buglist.worker_comment FROM buglist inner join projectsection on buglist.ProjectSection_id = projectsection.id  where projectsection.id =" + id.ToString() +";";
            try
            {
                SQL.MyDataReader = SQL.myCommand.ExecuteReader();
                dataGridView1.Rows.Clear();
                while (SQL.MyDataReader.Read())
                {
                    dataGridView1.Rows.Add(
                        SQL.MyDataReader.GetString(0),
                        SQL.MyDataReader.GetInt32(1),
                        SQL.MyDataReader.GetString(2),
                        SQL.MyDataReader.GetString(3), 
                        SQL.MyDataReader.GetInt32(4),
                        SQL.MyDataReader.GetString(5),
                        SQL.MyDataReader.GetString(6),
                        SQL.MyDataReader.GetString(7)
                        );
                  
                }
                SQL.MyDataReader.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
