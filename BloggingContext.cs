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
            return this.Blogs.ToList();
        }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
        }

        public List<Post> DisplayPosts()
        {
            return this.Posts.ToList();
        }

        public List<Post> FindPostsByBlogId(int id)
        {
            var blog = this.Blogs.Single(b => b.BlogId == id);

            List<Post> posts = new List<Post>(this.Posts.Where(p => p.BlogId == blog.BlogId));

            return posts;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            optionsBuilder.UseSqlServer(@config["BloggingContext:ConnectionString"]);
        }
    }
}