using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Models
{
    public class SendEmailData
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        /// <summary>
        /// Either text/plain (during development) or text/html
        /// </summary>
        public string ContentType { get; set; }
    }
}
