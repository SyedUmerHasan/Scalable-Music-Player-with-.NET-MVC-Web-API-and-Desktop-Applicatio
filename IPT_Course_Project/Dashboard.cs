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
using System.Net;
using System.Collections.Specialized;

namespace IPT_Course_Project
{
    public partial class Dashboard : DarkForm
    {

        public int loggedInUser;

        public Dashboard()
        {
            InitializeComponent();
        }

        public Dashboard(int id)
        {
            InitializeComponent();
            try
            {

                string query = @"SELECT * FROM [User] WHERE user_id='" + id.ToString() +"'";
                DBHelper dbh = new DBHelper();
                DataTable dt = dbh.GetDataTable(query);

                label1.Text = "Welcome : "+dt.Rows[0]["username"].ToString();
                loggedInUser = id;
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
            //label1.Text = id.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WindowsLoginForm lf = new WindowsLoginForm();
            lf.Show();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //To where your opendialog box get starting location. My initial directory location is desktop.
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            //Your opendialog box title name.
            openFileDialog1.Title = "Select file to be upload.";
            //which type file format you want to upload in database. just add them.
            openFileDialog1.Filter = "Select Valid Mp3 File(*.mp3)|*.mp3";
            //FilterIndex property represents the index of the filter currently selected in the file dialog box.
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label3.Text = path;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload mp3 file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                if (filename == "")
                {
                    MessageBox.Show("Please select a valid document.");
                }
                else
                {
                    using (SqlConnection openCon = new SqlConnection(ConfigurationManager.ConnectionStrings["projectDB"].ConnectionString))
                    {
                        string saveSong = @"insert into [Song] (path,uploaded_by,uploaded_date,name)values('\\Storage\\" + filename + "',@uploader_id,@uploaded_date,@name)";

                        using (SqlCommand querySongStaff = new SqlCommand(saveSong))
                        {
                            querySongStaff.Connection = openCon;
                            //queryUserStaff.Parameters.Add("user_id", SqlDbType.Int).Value = user_id;
                            querySongStaff.Parameters.Add("@uploader_id", SqlDbType.Int).Value = loggedInUser;
                            querySongStaff.Parameters.Add("@uploaded_date", SqlDbType.DateTime).Value = DateTime.Now;
                            querySongStaff.Parameters.Add("@name", SqlDbType.VarChar).Value = filename;
                            openCon.Open();

                            int rows = querySongStaff.ExecuteNonQuery();
                            //MessageBox.Show(rows.ToString());
                        }
                    }

                    string URI = @"http://localhost:51021/dashboard/Songs/Create";
                    string myParameters = "";
                    string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    System.IO.File.Copy(openFileDialog1.FileName, path + "\\Storage\\" + filename);

                    try
                    {
                        using (WebClient oWeb = new WebClient())
                        {
                            NameValueCollection parameters = new NameValueCollection();
                            parameters.Add("value1", "123");
                            parameters.Add("value2", "xyz");
                            oWeb.QueryString = parameters;
                            var responseBytes = oWeb.UploadFile(URI, path + "\\Storage\\" + filename);
                            string response = Encoding.ASCII.GetString(responseBytes);
                            MessageBox.Show(response);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }



                    MessageBox.Show("Song uploaded.");
                    openFileDialog1.FileName = "";
                    label3.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            SongsListForm lf = new SongsListForm(loggedInUser);
            lf.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlaylistForm lf = new PlaylistForm(loggedInUser);
            lf.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard lf = new Dashboard(loggedInUser);
            lf.Show();
        }
    }
}
