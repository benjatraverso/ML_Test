namespace Scanner
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Scanner.Tests;
    using System;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var test = new Test();
            if (test.RunAll())
            {
                Console.WriteLine("All tests ok");
            }
            else
            {
                var c = "test not ok add debugger breakpoint here";
                Console.WriteLine(c);
            }

            var host = new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseUrls("http://*:8080") // <-----
            .Build();

            host.Run();
            //CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
