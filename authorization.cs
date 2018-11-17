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
    public partial class authorization : Form
    {
        public authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.Connect = "Database=id46381849_liga;" +
                "Data Source=mysql.id46381849.myjino.ru;" +
                "User Id="+textBox1.Text+";" +
                "Password="+textBox2.Text+";" +
                "charset = UTF8;" +
                //"SslMode=none;" +
                "port=3306";
            
            SQL.myConnection = new MySqlConnection(SQL.Connect);

            SQL.myCommand = new MySqlCommand(SQL.CommandText, SQL.myConnection);
            try
            {
                SQL.myConnection.Open();
                var f = new administrator();
                this.Visible = false;
                f.ShowDialog();
                SQL.myConnection.Close();
                this.Close();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);


                return;
            }
        }

        
    }
}
