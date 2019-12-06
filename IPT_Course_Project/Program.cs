using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPT_Course_Project
{
    static class Program
    {
        //I am comment
        //I am second comment
        // Basit please check this is my commit and pull from Azure devops
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new  WindowsLoginForm());
        }
    }
}
