using System;

namespace Autumn.Mvc.Models
{
    public class ErrorModel
    {
        /// <summary>
        /// error message
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// error date
        /// </summary>
        public DateTime Timestamp { get; set; }
        
        /// <summary>
        /// error origin
        /// </summary>
        public string Origin { get; set; }
        
        /// <summary>
        /// error stack trace
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public ErrorModel()
        {
            Timestamp = DateTime.UtcNow;
        }
    }
}