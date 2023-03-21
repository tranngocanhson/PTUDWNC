﻿using System.Security.Cryptography;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;
using TatBlog.Services.Blogs;
using TatBlog.WinApp;

//Tạo đối tương DbContext để quản lý phiên làm việc
// với CSDL và trạng thái của các đối tượng
var context = new BlogDbContext();

var seeder = new DataSeeder(context);

//Tạo đối tượng BlogRepository
IBlogRepository blogRepo = new BlogRepository(context);

//Tìm 3 bài viết được xem/đọc nhiều nhất
var posts = await blogRepo.GetPopularArticlesAsync(3);

//Lấy danh sách chuyên mục
var categories = await blogRepo.GetCategoriesAsync();

//Tạo đối tượng chứa tham số phân trang
var pagingPrams = new PagingParams
{
    PageNumber = 1, // Lấy kết quả ở trang số 1
    PageSize = 5, // Lấy 5 mẫu tin
    SortColumn = "Name", //Sắp xếp theo 
    SortOrder = "DESC" //Theo chiều giảm dần
};
//Lấy danh sách từ khóa
var tagsList = await blogRepo.GetPagedTagsAsync(pagingPrams);

//Xuất ra màn hình
Console.WriteLine("{0,-5}{1,-50}{2,10}", "ID", "Name", "Count");
foreach(var item in tagsList)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}", item.Id, item.Name, item.PostCount);
}

//Xuất ra màn hình 
Console.WriteLine("{0,-5}{1,-50}{2,10}","ID","Name","Count");

foreach (var item in categories)
{
    Console.WriteLine("{0,-5}{1,-50}{2,10}",item.Id,item.Name,item.PostCount);
}

seeder.Initialize();

var authors =context.Authors.ToList();

//var posts = context.Posts
//    .Where(p => p.Published)
//    .OrderBy(p => p.Title)
//    .Select(p => new
//    {
//        Id = p.Id,
//        Title = p.Title,
//        ViewCount = p.ViewCount,
//        PostedDate = p.PostedDate,
//        Author = p.Author.FullName,
//        Category = p.Category.Name,
//    })
//    .ToList();

foreach (var post in posts)
{
    Console.WriteLine("ID               :{0}", post.Id);
    Console.WriteLine("Title            :{0}", post.Title);
    Console.WriteLine("View             :{0}", post.ViewCount);
    Console.WriteLine("Date             :{0:MM/dd/yyyy}", post.PostedDate);
    Console.WriteLine("Author           :{0}", post.Author);
    Console.WriteLine("Categor          :{0}", post.Category);
    Console.WriteLine("".PadRight(80, '-'));
}

Console.WriteLine("{0,4},{1,30},{2,-30},{3,12}", "ID", "FullName", "Email", "Joined Date");

foreach (var author in authors)
{
    Console.WriteLine("{0,4},{1,30},{2,-30},{3,12:MM/dd/yyyy}", author.Id, author.FullName, author.Email, author.JoinedDate);
}