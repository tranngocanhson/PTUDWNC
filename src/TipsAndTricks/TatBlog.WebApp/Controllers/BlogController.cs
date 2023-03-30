using Microsoft.AspNetCore.Mvc;
using TatBlog.Core.DTO;
using TatBlog.Services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;
        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyword = null,
            [FromQuery(Name = "p")] int pageNumber =1,
            [FromQuery(Name = "ps")] int pageSize =10)
        {
            //tạo đối tượng truy vấn
            var postQuery = new PostQuery()
            {
                //Chỉ lấy bài viết có trạng thái Published
                PublishedOnly = true,

                //Tìm bài viết theo từ khóa
                Keyword = keyword,
            };

            //Truy vấn các bài viết theo điều kiện đã tạo
            var postList = await _blogRepository.GetPagedPostAsync(postQuery, pageNumber, pageSize);
            //Lưu lại điều kiện truy vấn để hiển thị trong 
            ViewBag.PostQuery = postQuery;

            return View(postList);
        }
        public IActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            return View();
        }
        public IActionResult About() => View();

        public IActionResult Contact() => View();

        public IActionResult Rss() => Content("Nội dung sẽ được cập nhập");

    }
}
