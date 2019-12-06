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
    public partial class AddPlaylistForm : DarkForm
    {
        public int loggedInUser;
        public AddPlaylistForm()
        {
            InitializeComponent();
        }

        public AddPlaylistForm(int user)
        {
            loggedInUser = user;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Please Enter name of Playlist.");
            }
            else
            {
                string saveUser = "INSERT into [Playlist] (name,user_id) VALUES (@name,@user_id)";
                using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString))
                {
                    using (SqlCommand queryUserStaff = new SqlCommand(saveUser))
                    {
                        queryUserStaff.Connection = openCon;
                        queryUserStaff.Parameters.Add("@name", SqlDbType.VarChar, 30).Value = textBox1.Text;
                        queryUserStaff.Parameters.Add("@user_id", SqlDbType.Int).Value = loggedInUser;
                        openCon.Open();

                        int rows = queryUserStaff.ExecuteNonQuery();
                        MessageBox.Show("Playlist Susscefully Added.");
                        this.Hide();
                    }
                }
            }
        }
    }
}
