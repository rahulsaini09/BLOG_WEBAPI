using BLOG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        BlogModel GetBlogById(int id);
        int AddEditBlog(BlogModel model);
        List<BlogModel> GetAllBlog();
        int DeleteBlog(int id,int createdBy);
    }
}
