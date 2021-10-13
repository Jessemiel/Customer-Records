using BcsExamApp.Constants.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BcsExamApp.Model
{
    public class GenericResponse<T>
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        [JsonIgnore]
        public T Value { get; set; }
    }
}
