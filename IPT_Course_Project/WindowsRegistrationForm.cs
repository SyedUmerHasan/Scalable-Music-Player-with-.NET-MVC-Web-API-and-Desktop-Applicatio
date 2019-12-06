using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using DarkUI.Forms;

namespace IPT_Course_Project
{
    public partial class WindowsRegistrationForm : DarkForm
    {
        public WindowsRegistrationForm()
        {
            InitializeComponent();
        }

        private void WindowsRegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                

                using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString))
                {
                    string email, gender, password,username;
                    //int user_id = 9;
                    username = textBox4.Text;
                    email = textBox1.Text;
                    password = textBox2.Text;
                    gender = textBox3.Text;
                    string saveUser = "INSERT into [User] (username,email,password,gender) VALUES (@username,@email,@password,@gender)";

                    using (SqlCommand queryUserStaff = new SqlCommand(saveUser))
                    {
                        queryUserStaff.Connection = openCon;
                        //queryUserStaff.Parameters.Add("user_id", SqlDbType.Int).Value = user_id;
                        queryUserStaff.Parameters.Add("@email", SqlDbType.VarChar, 30).Value = email;
                        queryUserStaff.Parameters.Add("@username", SqlDbType.VarChar, 30).Value = username;
                        queryUserStaff.Parameters.Add("@gender", SqlDbType.VarChar, 30).Value = gender;
                        queryUserStaff.Parameters.Add("@password", SqlDbType.VarChar, 30).Value = password;
                        openCon.Open();

                        int rows=queryUserStaff.ExecuteNonQuery();
                        MessageBox.Show("User Susscefully Registered.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
            }
            
            //try
            //{
            //    string constring = ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString;

            //    /* Declaring Connection Variable */
            //    SqlConnection con = new SqlConnection(constring);

            //    /* Checking Connection is Opend or not If its not open the Opens */
            //    if (con.State != ConnectionState.Open)
            //        con.Open();

            //    /* Calling Stored Procedure as SqlCommand */
            //    SqlCommand cmd = new SqlCommand("regis", con);
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    /* Passing Input Parameters with Command */
            //    cmd.Parameters.AddWithValue("@email", email);
            //    cmd.Parameters["@email"].Direction = ParameterDirection.Input;

            //    cmd.Parameters.AddWithValue("@gender", gender);
            //    cmd.Parameters["@gender"].Direction = ParameterDirection.Input;

            //    cmd.Parameters.AddWithValue("@password", password);
            //    cmd.Parameters["@password"].Direction = ParameterDirection.Input;

            //    /* Executing Stored Procedure */
            //    cmd.ExecuteNonQuery();
            //    con.Close();
            //    MessageBox.Show("Data Inserted Succesfully");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            EmailCheck();
            
        }

        //checking whether email already exist
        public void EmailCheck()
        {
            string constring = ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("Select * from [User] where email= @email", con);
            cmd.Parameters.AddWithValue("@email", this.textBox1.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Email= " + dr[1].ToString() + " Already exist");
                    textBox1.Text = "";
                    break;
                }
            }
            //return textBox1.Text  ;
        }

        //checking whether username already exist
        public void UsernameCheck()
        {
            string constring = ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("Select * from [User] where username= @username", con);
            cmd.Parameters.AddWithValue("@username", this.textBox4.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    MessageBox.Show("Username= " + dr[4].ToString() + " Already exist");
                    textBox4.Text = "";
                    break;
                }
            }
            //return textBox1.Text  ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowsLoginForm lf = new WindowsLoginForm();
            lf.Show();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            UsernameCheck();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowsRegistrationForm rf = new WindowsRegistrationForm();
            rf.Show();
        }
    }
}
