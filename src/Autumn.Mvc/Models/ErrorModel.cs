using System;

namespace Autumn.Mvc.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Origin { get; set; }
        public string StackTrace { get; set; }

        public ErrorModel()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}