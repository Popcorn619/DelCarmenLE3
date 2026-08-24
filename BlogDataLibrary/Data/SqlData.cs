using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData
    {
        private const string connectionStringName = "SqlDB";
        private readonly ISqlDataAccess dbAccess;

        public SqlData(ISqlDataAccess dataAccess)
        {
            dbAccess = dataAccess;
        }

        public UserModel Authenticate(string username, string password)
        {
            var p = new
            {
                Username = username,
                Password = password
            };

            var user = dbAccess.LoadData<UserModel, dynamic>(
                "spUsers_Authenticate",
                p,
                connectionStringName,
                isStoredProcedure: true
            ).FirstOrDefault();

            return user;
        }

        public void Register(string username, string firstName, string lastName, string password)
        {
            dbAccess.SaveData<dynamic>(
                "spUsers_Register",
                new { username, firstName, lastName, password },
                connectionStringName,
                isStoredProcedure: true
            );
        }

        public void AddPost(PostModel post)
        {
            dbAccess.SaveData<dynamic>(
                "spPosts_Insert",
                new { post.UserId, post.Title, post.Body, post.DateCreated },
                connectionStringName,
                isStoredProcedure: true
            );
        }

        public List<ListPostModel> ListPosts()
        {
            return dbAccess.LoadData<ListPostModel, dynamic>(
                "spPosts_List",
                new { },
                connectionStringName,
                isStoredProcedure: true
            ).ToList();
        }


        public ListPostModel ShowPostDetails(int id)
        {
            return dbAccess.LoadData<ListPostModel, dynamic>(
                "spPosts_Detail",
                new { id },
                connectionStringName,
                isStoredProcedure: true
            ).FirstOrDefault();
        }
    }
}