using System;
using System.Windows.Forms;

namespace MartianRobotsClient
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Not used, currently RobotsUI is called directly from service.
        /// Mostly because we need both UI and Service running at same time
        /// in same VS.
        /// Should ba launched separately in production
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RobotsUI());
        }
    }
}