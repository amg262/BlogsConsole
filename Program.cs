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
                    //var blog = new Blog {Name = name};


                    var db = new BloggingContext();

                    new Post();

                    foreach (var blog in db.DisplayBlogs())
                    {
                        Console.Write($"ID: {blog.BlogId}");
                        Console.Write($"Name: {blog.BlogId}");
                    }
                }
                else if (inputNum == 2)
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
                else if (inputNum == 3)
                {
                    Console.WriteLine("Blog ID: ");
                    Int32.TryParse(Console.ReadLine(), out int blogId);


                    Console.WriteLine("Post Title: ");
                    string postTitle = Console.ReadLine();


                    Console.WriteLine("Post Content: ");
                    string postContent = Console.ReadLine();

                    var blog = new Blog {BlogId = blogId};

                    var post = new Post()
                        {BlogId = blog.BlogId, Content = postContent, Title = postTitle};
                    var db = new BloggingContext();
                    new Post();

                    db.AddPost(post);
                }
                else if (inputNum == 4)
                {

                    Console.WriteLine("Blog ID: ");
                    Int32.TryParse(Console.ReadLine(), out int blogId);
                    var db = new BloggingContext();

                    new Post();

                    foreach (var post in db.DisplayPosts())
                    {
                        Console.Write($"Blog ID: {post.BlogId}");
                        Console.Write($"Post ID: {post.PostId}");
                        Console.Write($"Post Title: {post.Title}");
                        Console.Write($"Post Content: {post.Content}");
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

            logger.Info("Program ended");
        }
    }
}