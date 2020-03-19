using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobotsUI
{
    class Service
    {
        string baseAddress = "http://localhost:8080/";

    using (WebApp.Start<Startup>(baseAddress))
    {
    HttpClient client = new HttpClient();
    var response = client.GetAsync(baseAddress + "api/values").Result;
    Console.WriteLine(response);
    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
    //Console.ReadLine();
    form.ShowDialog();

    Console.WriteLine("Web Server is running.");
    //Console.WriteLine("Press any key to quit.");
    Console.ReadLine();
    }
    }
}
