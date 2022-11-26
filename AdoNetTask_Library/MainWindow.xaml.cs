using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdoNetTask_Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowAll()
        {
            listView.Items.Clear();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = App.ConnectionString;
                SqlDataReader reader = null;

                conn.Open();

                string query = "SELECT * FROM Authors";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        listView.Items.Add($"{reader[0]} - {reader[1]} - {reader[2]}");
                    }
                }
            }
        }
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            
            using(var conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
                conn.Open();
                string query = @"INSERT INTO Authors(Id,Firstname,Lastname)
                VALUES(@id,@firstname,@lastname)";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var idParam = new SqlParameter();
                    idParam.ParameterName = "@id";
                    idParam.SqlDbType = SqlDbType.Int;
                    idParam.Value = int.Parse(IdTB.Text);

                    var FirstnameParam = new SqlParameter();
                    FirstnameParam.ParameterName = "@firstname";
                    FirstnameParam.Value = FirstnameTB.Text;
                    FirstnameParam.SqlDbType = SqlDbType.NVarChar;

                    var LastnameParam = new SqlParameter();
                    LastnameParam.ParameterName = "@lastname";
                    LastnameParam.Value = LastnameTB.Text;
                    LastnameParam.SqlDbType = SqlDbType.NVarChar;

                    cmd.Parameters.Add(idParam);
                    cmd.Parameters.Add(FirstnameParam);
                    cmd.Parameters.Add(LastnameParam);

                    var result = cmd.ExecuteNonQuery();
                    ShowAll();
                }
            }
        }

        private void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            listView.Items.Clear();
        }
    }
}
