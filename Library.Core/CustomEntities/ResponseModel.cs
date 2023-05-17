using System.Collections.Generic;

namespace Library.Core.CustomEntities
{
    public class ResponseModel<T> : Pagination
    {
        public IEnumerable<T> Data { get; set; }
        public bool IsError { get; set; }
        public string Message { get; set; }
    }
}
