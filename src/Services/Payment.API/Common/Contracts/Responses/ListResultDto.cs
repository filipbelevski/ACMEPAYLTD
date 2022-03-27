using System.Collections.Generic;

namespace Payment.API.Common.Contracts.Responses
{
    public class ListResultDto<T>
    {
        public IEnumerable<T> List { get; set; }

        public int TotalCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
