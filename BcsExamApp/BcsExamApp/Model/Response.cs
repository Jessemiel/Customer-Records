using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BcsExamApp.Model
{
    public class Response
    {
        [JsonProperty("ResId")]
        public string ResId { get; set; }

        [JsonProperty("UserEmail")]
        public string UserEmail { get; set; }
    }
}
