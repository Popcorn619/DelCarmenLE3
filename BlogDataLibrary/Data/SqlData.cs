using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDB";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        public UserModel Authenticate(string username, string password)
        {
            UserModel result = _db.LoadData<UserModel, dynamic>(
                "spUsers_Authenticate",
                new { username, password },
                connectionStringName,
                true
            ).FirstOrDefault();

            return result;
        }

        public void Register(string username, string firstName, string lastName, string password)
        {
            _db.SaveData<dynamic>(
                "spUsers_Register",
                new { username, firstName, lastName, password },
                connectionStringName,
                true
            );
        }

        public void AddPost(PostModel post)
        {
            _db.SaveData<dynamic>(
                "spPosts_Insert",
                new { post.UserId, post.Title, post.Body, post.DateCreated },
                connectionStringName,
                true
            );
        }

        public List<PostModel> ListPosts()
        {
            return _db.LoadData<PostModel, dynamic>(
                "dbo.spPosts_List",
                new { },
                connectionStringName,
                true
            ).ToList();
        }

        public PostModel ShowPostDetails(int id)
        {
            return _db.LoadData<PostModel, dynamic>(
                "dbo.spPosts_Detail",
                new { id },
                connectionStringName,
                true
            ).FirstOrDefault();
        }
    }
}