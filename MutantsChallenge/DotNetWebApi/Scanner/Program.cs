namespace Scanner
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Scanner.Tests;
    using System;

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

            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
