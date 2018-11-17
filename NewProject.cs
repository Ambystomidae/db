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
    public partial class NewProject : Form
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
 
            SQL.myCommand.CommandText = "insert into Project values(default," +
                SQL.EnterParam(textBox1.Text, dateTimePicker1.Text, dateTimePicker2.Text, dateTimePicker3.Text)
                + ",'В разработке');";
            //MessageBox.Show(SQL.myCommand.CommandText);
            try
            {
                SQL.myCommand.ExecuteNonQuery();
                this.Close();
            }
            catch (MySqlException ex) { MessageBox.Show(ex.ToString()); };
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
