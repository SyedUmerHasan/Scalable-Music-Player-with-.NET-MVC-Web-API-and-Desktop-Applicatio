using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;

namespace IPT_Course_Project
{
    public partial class PlaylistForm : DarkForm
    {
        int loggedInUser;
        public PlaylistForm()
        {
            InitializeComponent();
        }

        public PlaylistForm(int user)
        {
            loggedInUser = user;
            InitializeComponent();
            try
            {

                string query = @"SELECT * FROM [Playlist] WHERE user_id='" + loggedInUser + "'"; ;
                DBHelper dbh = new DBHelper();
                DataTable dt = dbh.GetDataTable(query);
                int x_incrementer = 1, y_incrementer = 60;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Playlist Found.");
                }
                else
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        // On all tables' columns
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ToString().Equals("name"))
                            {
                                var field1 = dtRow[dc].ToString();

                                Button dynamicButton = new Button();
                                dynamicButton.Height = 50;
                                dynamicButton.Width = 500;
                                dynamicButton.BackColor = Color.Red;
                                dynamicButton.ForeColor = Color.Blue;
                                dynamicButton.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 550;
                                y_incrementer += 50;
                                dynamicButton.Text = field1;
                                dynamicButton.Name = "DynamicButton";
                                dynamicButton.Font = new Font("Georgia", 16);
                                dynamicButton.Click += delegate(object sender, EventArgs e) { DynamicButton_Click(sender, e, field1); };
                                Controls.Add(dynamicButton);
                            }
                            else
                            {

                            }
                        }
                    }

                }
                

            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        private void DynamicButton_Click(object sender, EventArgs e, string s)
        {
            try
            {


                MessageBox.Show("Dynamic Button is clicked.");

            }

            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard rf = new Dashboard(loggedInUser);
            rf.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowsLoginForm lf = new WindowsLoginForm();
            lf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddPlaylistForm lf = new AddPlaylistForm(loggedInUser);
            lf.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlaylistForm lf = new PlaylistForm(loggedInUser);
            lf.Show();
        }

    }
}
