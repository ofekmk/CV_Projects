using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SqlProject
{
    public class SqlHelper
    {
        static private string ConnectionString
        {
            get
            {
               
                return @"Data Source=.\SQLEXPRESS;AttachDbFilename=F:\305551814\MoviesProject\DB\App_Data\ProjectDB.mdf;Integrated Security=True;User Instance=True";
            }
        }
        public static int ExecuteNonQuery(string SqlString)
        {
            int rowsAffected = -100;
            SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
            SqlCommand command = new SqlCommand(SqlString, connection);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected;
        }

        public static DataTable GetDataTable(string sqlString, string tableName)
        {
            SqlDataAdapter dataAdpter = new SqlDataAdapter(sqlString, SqlHelper.ConnectionString);
            DataTable dt = new DataTable();
            dataAdpter.Fill(dt);
            dt.TableName = tableName;
            return dt;
        }

        public static int UpdateDataTable(DataTable dt)
        {

            int rowsAffected;

            string SqlString = "SELECT * FROM " + dt.TableName + " WHERE 1=0";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(SqlString, SqlHelper.ConnectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            rowsAffected = dataAdapter.Update(dt);
            return rowsAffected;
        }
        //static void Main(string[] args)
        //{
        //    string MovieID = Console.ReadLine();



        //----------------------------------------------------
        //DELETE:

        //string sqlString = "DELETE FROM MoviesTable WHERE MovieID= ('"+ MovieID +"')";
        //Console.WriteLine(ExecuteNonQuery(sqlString));
        //Console.ReadKey();

        //----------------------------------------------------

        //insert using full connected(ExecuteNonQuery):

        //Console.WriteLine("Enter ID");
        //string strUserID = Console.ReadLine();
        //int UserID = int.Parse(strUserID);
        //Console.WriteLine("Enter Fname");
        //string Fname = Console.ReadLine();
        //Console.WriteLine("Enter Lname");
        //string Lname = Console.ReadLine();
        //string SqlString = "insert into UserTable (UserID,Fname,Lname) values(" + UserID + ",'" + Fname + "','" + Lname + "')";
        //Console.WriteLine(SqlHelper.ExecuteNonQuery(SqlString));
        //Console.ReadKey();

        //---------------------------------------------------


        //Console.WriteLine("Enter User ID");
        //string strUserID = Console.ReadLine();
        //int UserID = int.Parse(strUserID);
        //Console.WriteLine("Choose a country to Update");
        //string Country = Console.ReadLine();
        //string SqlString = "UPDATE UserTable SET Country = '"+Country+"' WHERE UserID = '"+UserID+"' ";
        //Console.WriteLine(SqlHelper.ExecuteNonQuery(SqlString));
        //Console.ReadKey();


        //using SqlProject
        //    name...

        //    Students st1 = NewsStyleUriParser Students();
        //DataTable dt = st1.GetStudent(IDataAdapter);
        //if (dt.Rows.Count==0)
        //return false;
        //dt.Rows[0]["password"].ToString().equals(password))


        //public static DataSet getDS()
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dt = SqlHelper.GetDataTable("select * Students Where 0=1","tblStudent");
        //    ds.Tables.Add(dt);
        //     DataTable dt = SqlHelper.GetDataTable("select * tblSubject Where 0=1","tblSubject");
        //    ds.Tables.Add(dt);
        //     DataTable dt = SqlHelper.GetDataTable("select * tblGrades Where 0=1","tblGrades");
        //    dt.Tables.Add(dt);
        //    ds.Tables["Students"].Rows.Count;


    }

}













