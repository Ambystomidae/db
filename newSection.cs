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
    public partial class newSection : Form
    {
        public static int selectedProjectId;
        public int id;
        public newSection()
        {
            InitializeComponent();
            id = selectedProjectId;        
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "insert into projectsection values(default,"+ SQL.EnterParam(id,textBox1.Text,dateTimePicker1.Text,dateTimePicker2.Text,dateTimePicker3.Text,textBox2.Text) +");";
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.myCommand.ExecuteNonQuery();

            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); };
            this.Close();
        }
    }
}
