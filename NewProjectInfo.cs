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
    public partial class NewProjectInfo : Form
    {
        public NewProjectInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQL.myCommand.CommandText = "insert into projectinfo values (default," + SQL.EnterParam(administrator.SelectedIndexOfProject,textBox1.Text, textBox2.Text) + ");";
            try
            {
                SQL.myCommand.ExecuteNonQuery();
                this.Close();
            }
            catch (Exception ex ){ MessageBox.Show(ex.Message); }
        }
    }
}
