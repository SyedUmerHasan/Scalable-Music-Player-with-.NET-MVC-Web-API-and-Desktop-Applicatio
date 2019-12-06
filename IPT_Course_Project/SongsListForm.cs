using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarkUI.Forms;
using System.Net.Http;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using System.IO;

namespace IPT_Course_Project

{
    public partial class SongsListForm : DarkForm
    {
        int loggedInUser;
        List<Song> GlobalSongs = new List<Song>();

        public SongsListForm()
        {
            InitializeComponent();
        }
        private async Task GetSongFromApiAsync()
        {
            String url = "http://localhost:51021/";
            var client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };
            HttpResponseMessage response = await client.GetAsync(@"Api/SongsApi");
            string result = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<Song> mySong = (List<Song>)javaScriptSerializer.Deserialize(result, typeof(List<Song>));

            //            MessageBox.Show(mySong[0].Song_Name.ToString());
            GlobalSongs = mySong;


        }

        public SongsListForm(int user)
        {
            loggedInUser = user;
            InitializeComponent();

            try
            {
                this.GetSongFromApiAsync();

                string query = @"SELECT * FROM [Song]";
                DBHelper dbh = new DBHelper();
                //DataTable dt = dbh.GetDataTable(query);
                MessageBox.Show("Welcome to My Music Player");
                DataTable dt = ToDataTable(GlobalSongs);

                int x_incrementer = 1, y_incrementer = 100;
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No Songs Found.");
                }
                else
                {
                    foreach (DataRow dtRow in dt.Rows)
                    {
                        
                        foreach (DataColumn dc in dt.Columns)
                        {
                            var Song_Name = "";
                            if (dc.ToString().Equals("Song_Name"))
                            {
                                Song_Name = dtRow[dc].ToString();
                            }

                            if (dc.ToString().Equals("Song_Image_Url"))
                            {
                                var field1 = dtRow[dc].ToString();
                                PictureBox pb1 = new PictureBox();
                                pb1.ImageLocation = "http://localhost:51021/" + field1;
                                pb1.Size = new System.Drawing.Size(100, 100);
                                pb1.SizeMode = PictureBoxSizeMode.CenterImage;
                                pb1.SizeMode = PictureBoxSizeMode.StretchImage;
                                pb1.BorderStyle = BorderStyle.Fixed3D;
                                pb1.Height = 70;
                                pb1.Width = 70;
                                pb1.BackColor = Color.Bisque;
                                pb1.ForeColor = Color.Blue;
                                pb1.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 100;
                                pb1.Name = "Image";
                                pb1.Font = new Font("Georgia", 16);
                                Controls.Add(pb1);

                            }
                            if (dc.ToString().Equals("Song_Url"))
                            {
                                var field1 = dtRow[dc].ToString();

                                char[] spearator = { '/' };
                                var seperateArray = field1.Split(spearator);
                                var Music_FileName = seperateArray[seperateArray.Count() - 1];

                                Button dynamicButton = new Button();
                                dynamicButton.Height = 50;
                                dynamicButton.Width = 500;
                                dynamicButton.BackColor = Color.Bisque;
                                dynamicButton.ForeColor = Color.Blue;
                                dynamicButton.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 550;
                                dynamicButton.Text = Music_FileName;
                                dynamicButton.Name = "DynamicButton";
                                dynamicButton.Font = new Font("Georgia", 16);
                                dynamicButton.Click += delegate(object sender, EventArgs e) { DynamicButton_Click(sender, e, field1); };
                                Controls.Add(dynamicButton);

                                Button dynamicButton1 = new Button();
                                dynamicButton1.Height = 50;
                                dynamicButton1.Width = 100;
                                dynamicButton1.BackColor = Color.Bisque;
                                dynamicButton1.ForeColor = Color.Red;
                                dynamicButton1.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 100;
                                dynamicButton1.Text = "Stop";
                                dynamicButton1.Name = "DynamicButton1";
                                dynamicButton1.Font = new Font("Georgia", 16);
                                dynamicButton1.Click += delegate(object sender, EventArgs e) { DynamicButton1_Click(sender, e); };
                                Controls.Add(dynamicButton1);


                                Button dynamicButton2 = new Button();
                                dynamicButton2.Height = 50;
                                dynamicButton2.Width = 100;
                                dynamicButton2.BackColor = Color.Bisque;
                                dynamicButton2.ForeColor = Color.Green;
                                dynamicButton2.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 100;
                                dynamicButton2.Text = "Pause";
                                dynamicButton2.Name = "DynamicButton2";
                                dynamicButton2.Font = new Font("Georgia", 16);
                                dynamicButton2.Click += delegate(object sender, EventArgs e) { DynamicButton2_Click(sender, e); };
                                Controls.Add(dynamicButton2);


                                Button dynamicButton3 = new Button();
                                dynamicButton3.Height = 50;
                                dynamicButton3.Width = 100;
                                dynamicButton3.BackColor = Color.Bisque;
                                dynamicButton3.ForeColor = Color.Blue;
                                dynamicButton3.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 100;
                                dynamicButton3.Text = "Resume";
                                dynamicButton3.Name = "DynamicButton3";
                                dynamicButton3.Font = new Font("Georgia", 16);
                                dynamicButton3.Click += delegate(object sender, EventArgs e) { DynamicButton3_Click(sender, e); };
                                Controls.Add(dynamicButton3);


                                Button dynamicButton4 = new Button();
                                dynamicButton4.Height = 50;
                                dynamicButton4.Width = 150;
                                dynamicButton4.BackColor = Color.Bisque;
                                dynamicButton4.ForeColor = Color.Brown;
                                dynamicButton4.Location = new Point(x_incrementer, y_incrementer);
                                x_incrementer += 150;
                                y_incrementer += 50;
                                dynamicButton4.Text = "Fast  Forward";
                                dynamicButton4.Name = "DynamicButton4";
                                dynamicButton4.Font = new Font("Georgia", 16);
                                dynamicButton4.Click += delegate(object sender, EventArgs e) { DynamicButton4_Click(sender, e); };
                                Controls.Add(dynamicButton4);
                                x_incrementer -= 1100;
                                y_incrementer += 100;
                            }
                            else
                            {
                                if (dc.ToString().Equals("uploaded_by"))
                                {

                                    var field1 = dtRow[dc].ToString();
                                    TextBox a = new TextBox();
                                    query = @"SELECT * FROM [User] WHERE user_id='" + field1 + "'";
                                    dbh = new DBHelper();
                                    DataTable dt1 = dbh.GetDataTable(query);
                                    a.Text = "Uploaded by: " + dt1.Rows[0]["username"];
                                    y_incrementer -= 50;
                                    a.Height = 40;
                                    a.Width = 400;
                                    a.Font = new Font("Georgia", 12);
                                    a.Location = new Point(x_incrementer, y_incrementer);
                                    x_incrementer -= 1000;
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
        

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }

        private void DynamicButton_Click(object sender, EventArgs e,string s)
        {
            try
            {

                string remoteUri = "http://localhost:51021";
                string fileName = s, myStringWebResource = null;
                myStringWebResource = remoteUri + fileName;
                char[] spearator = { '/' };
                var seperateArray = fileName.Split(spearator);
                var Music_FileName = seperateArray[seperateArray.Count() - 1];
                //                MessageBox.Show(Music_FileName);
                var File_Save_Path = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString() + @"\Storage\" + Music_FileName + ".mp3";
                if (!File.Exists(File_Save_Path))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadFileAsync(new Uri(myStringWebResource), File_Save_Path);
                }
                Globals.wplayer.URL = File_Save_Path;
                Globals.wplayer.controls.play();
                //System.Media.SoundPlayer player = new System.Media.SoundPlayer();

                //player.SoundLocation = "abc.mp3";
                //player.Play();

            }

            catch (Exception f)
            {
                MessageBox.Show(f.ToString()) ;
            }

        }
        private static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void DynamicButton1_Click(object sender, EventArgs e)
        {
            try
            {



                Globals.wplayer.controls.stop();

            }

            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

        }

        private void DynamicButton2_Click(object sender, EventArgs e)
        {
            try
            {


                Globals.time = Globals.wplayer.controls.currentPosition;
                Globals.wplayer.controls.pause();

            }

            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

        }

        private void DynamicButton3_Click(object sender, EventArgs e)
        {
            try
            {


                Globals.wplayer.controls.currentPosition = Globals.time;
                Globals.wplayer.controls.play();

            }

            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

        }

        private void DynamicButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.wplayer.controls.fastForward();


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

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            SongsListForm lf = new SongsListForm(loggedInUser);
            lf.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MySongsListForm lf = new MySongsListForm(loggedInUser);
            lf.Show();
        }

        private void SongsListForm_Load(object sender, EventArgs e)
        {

        }
    }
    public partial class Song
    {

        public int Song_Id { get; set; }

        public int Artist_Id { get; set; }

        public int Album_Id { get; set; }

        public int Genre_Id { get; set; }


        public string Song_Name { get; set; }


        public string Song_Image_Url { get; set; }
        public string Song_Playtime { get; set; }

        public int Song_Price { get; set; }

        public string Song_Url { get; set; }
        

    }

}
