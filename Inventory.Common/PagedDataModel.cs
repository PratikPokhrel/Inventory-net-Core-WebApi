﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Common
{
    public class PagedDataInquiryResponse<T>
    {
        private List<T> _items;
        public List<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }

    public static class PagedDataResponse<T> where T : class
    {
        public static PagedDataInquiryResponse<T> CreatedPagedDataResponse(List<T> list, int totalItemsCount, int totalPageCount, int page, int pageSize)
        {
            return new PagedDataInquiryResponse<T>
            {
                Items = list,
                TotalRecords = totalItemsCount,
                PageCount = totalPageCount,
                PageNumber = page,
                PageSize = pageSize
            };
        }
    }
}
