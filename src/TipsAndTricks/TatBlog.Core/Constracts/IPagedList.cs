using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Constracts
{
    public interface IPagedList
    {
        int PageCount { get; set; }
        int TotalItemCount { get; set; }
        int PageIndex { get; }
        int PageNumber { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsLastPage { get; }
        bool IsFirstPage { get; }
        int FirstItemIndex { get; }
        int LastItemIndex { get; }

    }
    public interface IPagedList<out T> : IPagedList, IEnumerable<T>
    {
        //Lấy phần tử tại vị trí index
        T this[int index] { get; }

        //Đếm số lượng phần tử chứa trong trang
        int Count { get; }
    }
}
