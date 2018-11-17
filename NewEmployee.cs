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
    public partial class NewEmployee : Form
    {
        public NewEmployee()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "insert into employee values (default," + SQL.EnterParam(textBox2.Text, textBox1.Text, 100, dateTimePicker1.Text) + ",null);";
            try
            {
                //MessageBox.Show(SQL.myCommand.CommandText);
                SQL.myCommand.ExecuteNonQuery();
                this.Close();
            } catch (MySqlException ex) { MessageBox.Show(ex.ToString()); };
        }

        private void NewEmployee_Load(object sender, EventArgs e)
        {

        }
    }
}
