using Dapper;
using BLOG.Models;
using BLOG.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace BLOG.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IConfiguration _config;
        public BlogRepository(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                var ss = _config.GetConnectionString("blogConnectionString");
                return new SqlConnection(ss);
            }
        }
        public BlogModel GetBlogById(int id)
        {
            try
            {
                using (IDbConnection _conn = Connection)
                {
                    var param = new DynamicParameters();
                    param.Add("@id", id);
                    string sQuery = "SELECT id as Id, blog_title [BlogTitle],blog_Description [BlogDescription],createdOn FROM BLOG_Post WHERE isActive = 1 and id = @id";
                    _conn.Open();
                    var result = _conn.Query<BlogModel>(sQuery, param);
                    _conn.Close();
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int AddEditBlog(BlogModel model)
        {
            try
            {
                using (IDbConnection _conn = Connection)
                {
                    _conn.Open();
                    IDbTransaction dbTransaction = _conn.BeginTransaction();
                    var param = new DynamicParameters();
                    param.Add("@Id", model.Id);
                    param.Add("@BlogTitle", model.BlogTitle);
                    param.Add("@BlogDescription", model.BlogDescription);
                    param.Add("@CreatedBy", model.createdBy);

                    var result = _conn.Execute("proc_AddEditBlog", param, dbTransaction, 0, CommandType.StoredProcedure);
                    if (result > 0) { dbTransaction.Commit(); } else { dbTransaction.Rollback(); }
                    return (int)HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return (int)HttpStatusCode.BadRequest;
            }

        }
        public List<BlogModel> GetAllBlog()
        {
            try
            {
                using (IDbConnection _conn = Connection)
                {
                    string sQuery = "SELECT id as Id, blog_title [BlogTitle],blog_Description [BlogDescription],createdOn FROM BLOG_Post WHERE isActive = 1";
                    _conn.Open();
                    var result = _conn.Query<BlogModel>(sQuery, null);
                    _conn.Close();
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public int DeleteBlog(int id, int createdBy)
        {
            try
            {
                using (IDbConnection _conn = Connection)
                {
                    _conn.Open();
                    IDbTransaction dbTransaction = _conn.BeginTransaction();
                    var param = new DynamicParameters();
                    param.Add("@id", id);
                    param.Add("@createdBy", createdBy);
                    string query = @"update BLOG_Post set isActive = 0, updatedBy = @createdBy,updatedOn = getdate()  where id = @id";
                    var result = _conn.Execute(query, param, dbTransaction, 0, CommandType.Text);
                    if (result > 0) { dbTransaction.Commit(); } else { dbTransaction.Rollback(); }
                    return (int)HttpStatusCode.OK;
                }
            }
            catch (Exception)
            {
                return (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
