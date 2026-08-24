using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace BlogTestUI
{
    class Program
    {
        static SqlData GetConnection()
        {
            string filePath = @"C:\Users\Josh DC\source\repos\DelCarmenLE2\BlogTestUI\appsettings.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filePath);

            IConfiguration config = builder.Build();
            ISqlDataAccess dbAccess = new SqlDataAccess(config);
            SqlData db = new SqlData(dbAccess);

            return db;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Program starting...");
            SqlData db = GetConnection();
            Console.WriteLine("Connected to database!\n");

            
            ShowPostDetails(db);

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        public static void Register(SqlData db)
        {
            Console.Write("Enter new username: ");
            var username = Console.ReadLine();

            Console.Write("Enter new password: ");
            var password = Console.ReadLine();

            Console.Write("Enter first name: ");
            var firstName = Console.ReadLine();

            Console.Write("Enter last name: ");
            var lastName = Console.ReadLine();

            db.Register(username, firstName, lastName, password);
            Console.WriteLine("Registration successful!");
        }

        private static UserModel GetCurrentUser(SqlData db)
        {
            Console.WriteLine("=== LOGIN ===");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = db.Authenticate(username, password);

            return user;
        }

        public static void Authenticate(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            if (user == null)
            {
                Console.WriteLine("Invalid credentials.");
            }
            else
            {
                Console.WriteLine($"Welcome, {user.UserName}");
            }
        }

        private static void AddPost(SqlData db)
        {
            UserModel user = GetCurrentUser(db);

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Write body: ");
            string body = Console.ReadLine();

            PostModel post = new PostModel
            {
                Title = title,
                Body = body,
                DateCreated = DateTime.Now,
                UserId = user.Id
            };

            db.AddPost(post);
            Console.WriteLine("Post added successfully!");
        }

        private static void ListPosts(SqlData db)
        {
            List<PostModel> posts = db.ListPosts();

            foreach (PostModel post in posts)
            {
                Console.WriteLine($"Post Id: {post.Id}, Title: {post.Title} by {post.UserName}");
                Console.WriteLine($"Date Created: {post.DateCreated:yyyy-MM-dd}");
                Console.WriteLine($"{post.Body.Substring(0, Math.Min(20, post.Body.Length))}...");
                Console.WriteLine();
            }
        }

        private static void ShowPostDetails(SqlData db)
        {
            Console.Write("Enter a post ID: ");
            int id = int.Parse(Console.ReadLine());

            PostModel post = db.ShowPostDetails(id);

            Console.WriteLine(post.Title);
            Console.WriteLine($"By {post.FirstName} {post.LastName} ({post.UserName})");
            Console.WriteLine();
            Console.WriteLine(post.Body);
            Console.WriteLine(post.DateCreated.ToString("MMMM d yyyy"));
        }
    }
}
