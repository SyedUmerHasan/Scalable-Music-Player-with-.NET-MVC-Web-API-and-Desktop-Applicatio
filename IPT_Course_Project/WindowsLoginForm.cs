using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using DarkUI.Forms;

namespace IPT_Course_Project
{
    public partial class WindowsLoginForm : DarkForm
    {
        public WindowsLoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection sqlcon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\basit\Source\Repos\IPT_Course_Project\DB\project_db.mdf;Integrated Security=True;Connect Timeout=30");
                //SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString);
                //sqlcon.Open();
                //MessageBox.Show("Done and Dusted");
                //sqlcon.Close();

                string query = @"SELECT COUNT(*) FROM [User] WHERE email='" + textBox1.Text + "' AND password='" + textBox2.Text + "'";
                DBHelper dbh = new DBHelper();
                DataTable dt = dbh.GetDataTable(query);

                if (dt.Rows[0][0].ToString() == "1")
                {
                    this.Hide();
                    query = @"SELECT user_id FROM [User] WHERE email='" + textBox1.Text + "'";
                    dbh = new DBHelper();
                    dt = dbh.GetDataTable(query);
                    foreach (DataRow row in dt.Rows)
                    {
                        
                        new Dashboard((int)row["user_id"]).Show();
                    }
                }
                else
                    MessageBox.Show("Invalid username or password");
            }
            catch (Exception f)
            {
                textBox2.Text = f.ToString();
                MessageBox.Show(f.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowsRegistrationForm rf = new WindowsRegistrationForm();
            rf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }

    // static class to hold global variables, etc.
    static class Globals
    {
        // global int
        public static WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
        public static double time;

    }

    public class DBHelper
    {
        SqlConnection connectionObj;
        public DBHelper()
        {
            connectionObj = new SqlConnection();
            connectionObj.ConnectionString = ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString;
        }

        private void ConnectionOpen()
        {
            connectionObj.Open();
        }

        private void ConnectionClose()
        {
            connectionObj.Close();
        }

        public DataTable GetDataTable(string query, DataTable param = null)
        {
            try
            {

                DataTable dt = new DataTable("Result");
                ConnectionOpen();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionObj);
                if (param != null)
                {
                    foreach (DataRow dr in param.Rows)
                    {

                        adapter.SelectCommand.Parameters.AddWithValue("@" + dr["Key"].ToString(), dr["Value"].ToString());
                    }
                }
                adapter.Fill(dt);
                ConnectionClose();
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }

        public DataTable GetDataTableSP(string SPName, DataTable param = null)
        {
            try
            {
                DataTable dt = new DataTable("Result");
                ConnectionOpen();
                SqlCommand command = new SqlCommand();
                command.Connection = connectionObj;
                command.CommandText = SPName;
                command.CommandType = CommandType.StoredProcedure;
                if (param != null)
                {
                    foreach (DataRow dr in param.Rows)
                    {
                        command.Parameters.AddWithValue("@" + dr["Key"].ToString(), dr["Value"].ToString());
                    }
                }

                SqlDataReader rdr = command.ExecuteReader();

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    if (!dt.Columns.Contains(rdr.GetName(i)))
                        dt.Columns.Add(rdr.GetName(i));
                }
                while (rdr.Read())
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                        dr[dc.ColumnName] = rdr[dc.ColumnName];

                    dt.Rows.Add(dr);
                }
                ConnectionClose();

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }
    }
}