using System;

namespace Covid.Models.Common
{
    public class ServiceResult<T>
    {
        public T Value { get; set; }
        public Exception Exception { get; set; }
    }
}
