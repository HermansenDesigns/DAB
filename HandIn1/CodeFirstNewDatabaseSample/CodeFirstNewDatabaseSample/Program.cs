using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstNewDatabaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BlogContext())
            {
                Console.WriteLine("Enter a name for a new blog:");
                var name = Console.ReadLine();
                Console.WriteLine();

                var blog = new Blog {Name = name};
                db.Blogs.Add(blog);
                db.SaveChanges();

                Console.WriteLine("Enter a name for a new Organization:");
                var nameOrg = Console.ReadLine();
                Console.WriteLine();

                var organization = new Organization {OrganizationName = nameOrg};
                db.Organizations.Add(organization);
                db.SaveChanges();

                Console.WriteLine("Enter a name for a new User:");
                var nameUser = Console.ReadLine();
                Console.WriteLine();

                var user = new User { Username = nameUser, Organization = organization};
                db.Users.Add(user);
                db.SaveChanges();

                var queryBlogs = from b in db.Blogs
                    orderby b.Name
                    select b;

                var queryUsers = from u in db.Users
                    orderby u.Username
                    select u;

                var queryOrganization = from o in db.Organizations
                    orderby o.OrganizationName
                    select o;

                Console.WriteLine("Displaying all blogs: ");
                foreach (var blogItem in queryBlogs)
                {
                    Console.WriteLine(blogItem.Name);
                }
                Console.WriteLine();

                Console.WriteLine("Displaying all users: ");
                foreach (var userItem in queryUsers)
                {
                    Console.WriteLine(userItem.Username);
                    Console.WriteLine($"-- organization = {userItem.Organization.OrganizationName}");
                }
                Console.WriteLine();

                Console.WriteLine("Displaying all organizations: ");
                foreach (var organizationItem in queryOrganization)
                {
                    Console.WriteLine(organizationItem.OrganizationName);
                }
                Console.WriteLine();
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlodId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }

        public virtual Organization Organization { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CountryCode { get; set; }
        public List<Organization> OrgsInCountry { get; set; }
    }

    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }

        public List<Country> Homelands { get; set; }
    }

    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasColumnName("display_name");
        }
    }
}
