using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Constracts;

namespace TatBlog.WinApp
{
    public class PagingParams : IPagingParams
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int SortColumn { get; set; } = "Id";
        public int SortOrder { get; set; } = "ASC";
    }
}
