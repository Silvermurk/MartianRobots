using MartianRobotsClient;
using Microsoft.Owin.Hosting;
using System;

namespace MartianRobots
{
    /// <summary>
    /// Our main web api program.
    /// Starts server with prepared startup instance,
    /// Starts RobotsUI form, and awaits keypress to exit
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:8080/";
            var form = new RobotsUI();
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Web Server is running. Press any key to stop");
                form.ShowDialog();

                
                //Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}