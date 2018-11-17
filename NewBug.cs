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
    public partial class NewBug : Form
    {
        public static int SectionId;
        int id;
        int bugnum;
        public NewBug()
        {
            id = SectionId;
            InitializeComponent();

            SQL.myCommand.CommandText = "select count(*) from buglist where projectsection_id = " + id.ToString() + ";";
            try
            {
                bugnum= Int32.Parse(SQL.myCommand.ExecuteScalar().ToString())+1;  
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "insert into buglist values(" + SQL.EnterParam(id, bugnum, 1, textBox2.Text, textBox3.Text, textBox4.Text, comboBox1.Text, textBox1.Text) + ");";
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.myCommand.ExecuteNonQuery();
                this.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
