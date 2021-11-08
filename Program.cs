using System;
using System.IO;
using System.Linq;
using NLog;
using NLog.Web;

namespace BlogsConsole
{
    internal class Program
    {
        // create static instance of Logger
        private static readonly Logger logger = NLogBuilder
            .ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config")
            .GetCurrentClassLogger();

        private static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                Console.WriteLine("1. Display Blogs");
                Console.WriteLine("2. Add Blog");
                Console.WriteLine("3. Create Post for a blog");
                Console.WriteLine("4. Display Posts for a blog");

                // This is an extra layer of user & exception handling that will not allow non-# input
                int.TryParse(Console.ReadLine(), out int inputNum);

                if (inputNum == 1)
                {
                    try
                    {
                        var db = new BloggingContext();
                        new Post();

                        foreach (var blog in db.DisplayBlogs())
                        {
                            Console.Write($"ID: {blog.BlogId}");
                            Console.Write($"Name: {blog.BlogId}");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        Console.WriteLine(e);
                        throw;
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

                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query) Console.WriteLine(item.Name);
                }
                else if (inputNum == 3)
                {
                    Console.WriteLine("BlogId: ");
                    
                    // This is an extra layer of user & exception handling that will not allow non-# input
                    int.TryParse(Console.ReadLine(), out int blogId);


                    Console.WriteLine("Title: ");
                    var postTitle = Console.ReadLine();


                    Console.WriteLine("Content: ");
                    var postContent = Console.ReadLine();

                    try
                    {
                        var blog = new Blog {BlogId = blogId};

                        var post = new Post {BlogId = blog.BlogId, Content = postContent, Title = postTitle};
                        var db = new BloggingContext();
                        new Post();

                        db.AddPost(post);
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        Console.WriteLine(e);
                        throw;
                    }
                }
                else if (inputNum == 4)
                {
                    Console.WriteLine("Enter BlogId to display posts from: ");
                    
                    // This is an extra layer of user & exception handling that will not allow non-# input
                    int.TryParse(Console.ReadLine(), out int blogId);

                    try
                    {
                        var db = new BloggingContext();

                        new Post();

                        db.FindPostsByBlogId(blogId);

                        var count = 0;
                        foreach (var post in db.FindPostsByBlogId(blogId))
                        {
                            if (count == 0) Console.WriteLine($"BlogId: {post.Blog.BlogId}  Name: {post.Blog.Name}");

                            Console.WriteLine($"ID: {post.PostId}");
                            Console.WriteLine($"Title: {post.Title}");
                            Console.WriteLine($"Content: {post.Content}");

                            count++;
                        }

                        Console.WriteLine($"Post(s): {count}");
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }

            logger.Info("Program ended");
        }
    }
}