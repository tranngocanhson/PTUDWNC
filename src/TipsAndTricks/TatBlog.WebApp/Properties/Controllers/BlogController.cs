using Microsoft.AspNetCore.Mvc;

namespace TatBlog.WebApp.Properties.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            return View();
        }
        public IActionResult About() => View();

        public IActionResult Contact() => View();
        public IActionResult Rss() => Content("Nội dung sẽ được cập nhập");

        public async Task<IActionResult> Index(
            [FromQuery(Name = "k")] string keyqord = null,
             [FromQuery(Name = "p")] int pageNumber = 1,
              [FromQuery(Name = "ps")] int pageSize = 10)
        {
            var postQuery = new PostQuery()
                PublishedOnly = true,
                Keyword = keyword
        };
        var postList = await _blogReponsitory
            .GetPaged
    }
}
