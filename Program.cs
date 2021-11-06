using System;
using NLog.Web;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config")
            .GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                Console.WriteLine($"1. Display Blogs");
                Console.WriteLine($"2. Add Blog");
                Console.WriteLine($"3. Create Post");
                Console.WriteLine($"4. Display Posts");

                Int32.TryParse(Console.ReadLine(), out int inputNum);


                if (inputNum == 1)
                {
                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Blog {Name = name};

                    var db = new BloggingContext();
                    new Post();

                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);

                    // Display all Blogs from the database
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                else if (inputNum == 2)
                {
                }
                else if (inputNum == 3)
                {
                }
                else if (inputNum == 4)
                {
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}