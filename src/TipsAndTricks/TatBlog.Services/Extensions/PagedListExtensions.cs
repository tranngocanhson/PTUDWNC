﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Collections;
using TatBlog.Core.Constracts;
using System.Linq.Dynamic.Core;

namespace TatBlog.Services.Extensions
{
    public static class PagedListExtension
    {
        public static string GetOrderExpression(this IPagingParams pagingParams, string defaultColumn = "Id")
        {
            var column = string.IsNullOrWhiteSpace(pagingParams.SortColumn)
                    ? defaultColumn
                    : pagingParams.SortColumn;

            var order = "ASC".Equals(
                pagingParams.SortOrder, StringComparison.OrdinalIgnoreCase)
                    ? pagingParams.SortOrder : "DESC";
            return $"{column} {order}";
        }
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var totalCount = await source.CountAsync(cancellationToken);
            var items = await source
                .OrderBy(pagingParams.GetOrderExpression())
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync(cancellationToken);
            return new PagedList<T>(items, pagingParams.PageNumber, pagingParams.PageSize, totalCount);
        }
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNmber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC", CancellationToken cancellationToken = default)
        {
            var totalCount = await source.CountAsync(cancellationToken);
            var items = await source
                .OrderBy($"{sortColumn} {sortOrder}")
                .Skip((pageNmber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return new PagedList<T>(items, pageNmber, pageSize, totalCount);
        }

    }
}
