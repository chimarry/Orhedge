using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Models
{
    public class TemplateEmail
    {
        public string From { get; set; }
        public string To { get; set; }
        public string TemplateId { get; set; }

        // Any object that Newtonsoft Json is able to serialize(Class with JsonProperty attributes, anonymous types, etc.)
        public object TemplateData { get; set; }
    }
}
