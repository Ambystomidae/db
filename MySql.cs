using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace MyWebStudio
/*Данное пространство имен и классы были созданы для того, чтобы было можно использовать элементы работы с скл во всех формах*/
{
    public static class TempData
    {
        public static Int32 selectedId { get; set; }
    }
    public class ProjectLabel
    {
        public Label label;
        public int id;
        public string date1, date2, date3;
    }
    public static class SQL
    {
        public static int status { get; set; }
        public static string CommandText { get; set; }
        public static string Connect { get; set; }
        public static MySqlConnection myConnection { get; set; }
        public static MySqlCommand myCommand { get; set; }
        public static MySqlDataReader MyDataReader { get; set; }
        public static string StringToQueryFormat(string input)
        {
            return "'" + input + "'";
        }
        public static string EnterParam(params object[] arr)
        {
            string output = "";
            foreach (var param in arr)
            {
                if (param is Int32 || param is float || param is decimal|| param is double)
                {
                    output += param.ToString() + ",";
                }else if (param is string)
                {
                    output += StringToQueryFormat(param.ToString()) + ",";
                }
                else
                {
                    output += param.ToString() + ",";
                }
            }
            output = output.Remove(output.Length - 1);
            return output;
        }
    }
}
