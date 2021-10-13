using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BcsExamApp.Model
{
    //json handling - Newtonsoft.Json 
    public class Customer
    {
        [JsonProperty("reservationId")]
        public string ReservationId { get; set; }

        [JsonProperty("guestName")]
        public string GuestName { get; set; }

        [JsonProperty("guestMobile")]
        public string GuestMobile { get; set; }

        [JsonProperty("arrived")]
        public string Arrived { get; set; }

        [JsonProperty("depart")]
        public string Depart { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("nights")]
        public string Nights { get; set; }

        [JsonProperty("areaName")]
        public string AreaName { get; set; }

        [JsonProperty("previousNPS")]
        public object PreviousNPS { get; set; }

        [JsonProperty("previousNPSComment")]
        public string PreviousNPSComment { get; set; }
    }
}
