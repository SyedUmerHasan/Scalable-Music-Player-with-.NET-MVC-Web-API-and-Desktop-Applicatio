using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using DarkUI.Forms;

namespace IPT_Course_Project
{
    public partial class MySongsListForm : DarkForm
    {
        int loggedInUser;
        public MySongsListForm()
        {
            InitializeComponent();
        }

        public MySongsListForm(int user)
        {
            loggedInUser = user;
            InitializeComponent();

            try
            {

                string query = @"SELECT * FROM [Song] WHERE uploaded_by='" + loggedInUser + "'";
                DBHelper dbh = new DBHelper();
                DataTable dt = dbh.GetDataTable(query);
                int x_incrementer = 1, y_incrementer = 60;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Songs Found.");
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
                                if (dc.ToString().Equals("uploaded_by"))
                                {

                                    var field1 = dtRow[dc].ToString();
                                    TextBox a = new TextBox();
                                    query = @"SELECT * FROM [User] WHERE user_id='" + loggedInUser + "'";
                                    dbh = new DBHelper();
                                    DataTable dt1 = dbh.GetDataTable(query);
                                    a.Text = "Uploaded by: " + dt1.Rows[0]["username"];
                                    y_incrementer -= 50;
                                    a.Height = 40;
                                    a.Width = 400;
                                    a.Font = new Font("Georgia", 12);
                                    a.Location = new Point(x_incrementer, y_incrementer);
                                    x_incrementer -= 550;
                                    y_incrementer += 50;
                                    a.Enabled = false;
                                    Controls.Add(a);
                                }
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


                
                string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + @"\Storage\" + s;
                //MessageBox.Show(directory);
                Globals.wplayer.URL = directory;
                Globals.wplayer.controls.play();

                //System.Media.SoundPlayer player = new System.Media.SoundPlayer();

                //player.SoundLocation = "abc.mp3";
                //player.Play();

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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MySongsListForm lf = new MySongsListForm(loggedInUser);
            lf.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SongsListForm lf = new SongsListForm(loggedInUser);
            lf.Show();
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

    }
}
