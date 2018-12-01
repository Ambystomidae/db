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
                "Data Source=127.0.0.1;" +
                "User Id=LoginTest;" +
                "Password=1234;" +
                "charset = UTF8;" +
                "port=3306";
            
            SQL.myConnection = new MySqlConnection(SQL.Connect);

            SQL.myCommand = new MySqlCommand(SQL.CommandText, SQL.myConnection);
            try
            {
                SQL.myConnection.Open();



                SQL.myCommand.CommandText = "select admin from employee where login = " 
                    + SQL.StringToQueryFormat(textBox1.Text) + " and password = " 
                    + SQL.StringToQueryFormat(textBox2.Text) + ";";

                int UserStatus = (int)SQL.myCommand.ExecuteScalar();
                SQL.myConnection.Close();

                Form f;
                if (UserStatus == 1)
                {
                    SQL.Connect = "Database=id46381849_liga;" +
                "Data Source=127.0.0.1;" +
                "User Id=Admin;Password=1234;" +
                "charset = UTF8;port=3306";
                    SQL.myConnection = new MySqlConnection(SQL.Connect);
                    SQL.myCommand = new MySqlCommand(SQL.CommandText, SQL.myConnection);
                    SQL.myConnection.Open();
                    f = new administrator();
                }
                else
                {
                    SQL.Connect = "Database=id46381849_liga;" +
                "Data Source=127.0.0.1;" +
                "User Id=User;Password=1234;" +
                "charset = UTF8;port=3306";
                    SQL.myConnection = new MySqlConnection(SQL.Connect);
                    SQL.myCommand = new MySqlCommand(SQL.CommandText, SQL.myConnection);
                    SQL.myConnection.Open();
                    f = new UserWindow();
                }
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
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Неправильный логин или пароль");

            }
        }

        
    }
}
