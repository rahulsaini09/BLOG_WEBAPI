using System.Collections.Generic;
using BLOG.Models;
using BLOG.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BLOG.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogManagementController : ControllerBase
    {
        private readonly IBlogRepository _blogrepo;
        public BlogManagementController(IBlogRepository blogrepo)
        {
            _blogrepo = blogrepo;
        }

        [HttpPost("CreateBlog")]
        public int CreateBlog(BlogModel model)
        {
            return _blogrepo.AddEditBlog(model);
        }

        [HttpPost("UpdateBlog")]
        public int UpdateBlog(BlogModel model)
        {
            return _blogrepo.AddEditBlog(model);
        }

        [HttpGet]
        [Route("Id/{id}")]
        public BlogModel GetBlogById(int id)
        {
            return _blogrepo.GetBlogById(id);
        }

        [HttpPost("DeleteBlog")]
        public int DeleteBlog(BlogModel model)
        {
            return _blogrepo.DeleteBlog(model.Id, model.createdBy);
        }

        [HttpGet]
        [Route("GetAllBlog")]
        public List<BlogModel> GetAllBlog()
        {
            return _blogrepo.GetAllBlog();
        }
    }
}