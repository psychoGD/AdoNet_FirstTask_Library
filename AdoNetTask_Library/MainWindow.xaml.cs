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
        //SqlConnection conn;
        //string cs = "";
        //DataTable table;
        //SqlDataReader reader;
        //public MainWindow()
        //{
        //    InitializeComponent();
        //    conn = new SqlConnection();
        //    cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;


        //    using (conn = new SqlConnection())
        //    {
        //        da = new SqlDataAdapter();
        //        conn.ConnectionString = cs;
        //        conn.Open();
        //        set = new DataSet();

        //        SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);

        //        //command.Parameters.Add(new SqlParameter
        //        //{
        //        //    DbType = DbType.Int32,
        //        //    ParameterName = "@id",
        //        //    Value = 1
        //        //});

        //        da.SelectCommand = command;

        //        da.Fill(set, "firstAuthor");
        //        myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
        //    }
        //}



        //DataSet set;
        //SqlDataAdapter da;
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    #region DataTable

        //    //using (var conn=new SqlConnection())
        //    //{
        //    //    conn.ConnectionString = cs;
        //    //    conn.Open();

        //    //    SqlCommand command = new SqlCommand();
        //    //    command.CommandText = "SELECT * FROM Authors";
        //    //    command.Connection = conn;

        //    //    table = new DataTable();

        //    //    bool hasColumnAdded = false;
        //    //    using (reader=command.ExecuteReader())
        //    //    {
        //    //        while (reader.Read())
        //    //        {
        //    //            if (!hasColumnAdded)
        //    //            {
        //    //                for (int i = 0; i < reader.FieldCount; i++)
        //    //                {
        //    //                    table.Columns.Add(reader.GetName(i));
        //    //                }
        //    //                hasColumnAdded = true;
        //    //            }
        //    //            DataRow row=table.NewRow();
        //    //            for (int i = 0; i < reader.FieldCount; i++)
        //    //            {
        //    //                row[i] = reader[i];
        //    //            }
        //    //            table.Rows.Add(row);
        //    //        }

        //    //        myDataGrid1.ItemsSource=table.DefaultView;
        //    //    }

        //    //}

        //    #endregion



        //    #region DataSet And SqlDataAdapter

        //    //using (conn=new SqlConnection())
        //    //{
        //    //    conn.ConnectionString = cs;
        //    //    conn.Open();
        //    //    set = new DataSet();

        //    //    da=new SqlDataAdapter("SELECT * FROM Authors;SELECT * FROM Books",conn);

        //    //    da.Fill(set, "mybook");

        //    //    myDataGrid1.ItemsSource = set.Tables[0].DefaultView;
        //    //    myDataGrid2.ItemsSource = set.Tables[1].DefaultView;


        //    //}


        //    using (conn = new SqlConnection())
        //    {
        //        da = new SqlDataAdapter();
        //        conn.ConnectionString = cs;
        //        conn.Open();
        //        set = new DataSet();

        //        SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);

        //        //command.Parameters.Add(new SqlParameter
        //        //{
        //        //    DbType = DbType.Int32,
        //        //    ParameterName = "@id",
        //        //    Value = 1
        //        //});

        //        da.SelectCommand = command;

        //        da.Fill(set, "firstAuthor");

        //        //    myDataGrid1.ItemsSource = set.Tables[0].DefaultView;




        //        //da = new SqlDataAdapter();
        //        //conn.ConnectionString = cs;
        //        //conn.Open();
        //        //set = new DataSet();

        //        command = new SqlCommand("UPDATE Authors SET Firstname=@firstName WHERE Id=@id", conn);

        //        command.Parameters.Add(new SqlParameter
        //        {
        //            DbType = DbType.Int32,
        //            ParameterName = "@id",
        //            Value = 1
        //        });

        //        command.Parameters.Add(new SqlParameter
        //        {
        //            SqlDbType = SqlDbType.NVarChar,
        //            ParameterName = "@firstName",
        //            Value = "DDDDD"
        //        });

        //        da.UpdateCommand = command;
        //        da.UpdateCommand.ExecuteNonQuery();

        //        da.Update(set, "firstAuthor");
        //        set.Clear();
        //        da.Fill(set, "firstAuthor");

        //        // myDataGrid1.ItemsSource = null;
        //        myDataGrid1.ItemsSource = set.Tables[0].DefaultView;


        //    }


        //    #endregion
        //}



        //---------------------------------------------------------------------------




        SqlConnection conn;
        DataSet set;
        SqlDataAdapter da;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowAll()
        {
            listView.ItemsSource = null;
            using (conn = new SqlConnection())
            {
                conn.ConnectionString = App.ConnectionString;
                conn.Open();
                set = new DataSet();

                da = new SqlDataAdapter("SELECT * FROM Authors", conn);

                da.Fill(set, "mybook");

                listView.ItemsSource = set.Tables[0].DefaultView;
                //myDataGrid2.ItemsSource = set.Tables[1].DefaultView;


            }
        }
        private void InsertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FirstnameTB.Text != String.Empty && LastnameTB.Text != String.Empty && IdTB.Text != String.Empty
                && FirstnameTB.Text != "Firstname" && LastnameTB.Text != "Lastname" && IdTB.Text != "Id")
            {
                using (conn = new SqlConnection())
                {
                    da = new SqlDataAdapter();
                    conn.ConnectionString = App.ConnectionString;
                    conn.Open();
                    set = new DataSet();

                    SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);
                    da.SelectCommand = command;
                    da.Fill(set, "firstAuthor");

                    command = new SqlCommand("INSERT INTO Authors(Id,Firstname,Lastname) VALUES(@id,@firstname,@lastname)", conn);

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@id",
                        Value = int.Parse(IdTB.Text)
                    });

                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@firstName",
                        Value = FirstnameTB.Text
                    });

                    command.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.NVarChar,
                        ParameterName = "@lastname",
                        Value = LastnameTB.Text
                    });
                    da.InsertCommand = command;
                    da.InsertCommand.ExecuteNonQuery();

                    da.Update(set, "firstAuthor");
                    set.Clear();
                    da.Fill(set, "firstAuthor");

                    // myDataGrid1.ItemsSource = null;
                    listView.ItemsSource = set.Tables[0].DefaultView;


                }
            }
            else
            {
                MessageBox.Show("Cannot Add Empty Data");
            }
        }

        private void ShowAllBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowAll();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            listView.ItemsSource = null;
        }
    }
}
