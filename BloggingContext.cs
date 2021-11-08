using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogsConsole
{
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }


        public List<Blog> DisplayBlogs()
        {
            return Blogs.ToList();
        }

        public void AddBlog(Blog blog)
        {
            Blogs.Add(blog);
            SaveChanges();
        }

        public void AddPost(Post post)
        {
            Posts.Add(post);
            SaveChanges();
        }

        public List<Post> DisplayPosts()
        {
            return Posts.ToList();
        }

        public List<Post> FindPostsByBlogId(int id)
        {
            var blog = Blogs.Single(b => b.BlogId == id);

            var posts = new List<Post>(Posts.Where(p => p.BlogId == blog.BlogId));

            return posts;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(config["BloggingContext:ConnectionString"]);
        }
    }
}