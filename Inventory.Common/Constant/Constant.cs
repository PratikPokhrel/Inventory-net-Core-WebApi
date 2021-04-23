using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Common.Constant
{
    public class Constants
    {
        public static class Paging
        {
            public const int MinPageSize = 0;
            public const int MinPageNumber = 1;
            public const int DefaultPageNumber = 1;
            public const int DefaultPageSize = 10;
        }
        public static class CommonParameterNames
        {
            public const string PageNumber = "pageNumber";
            public const string PageSize = "pageSize";
            public const string SearchText = "searchText";
            public const string SearchDate = "searchDate";
        }


    }
}
