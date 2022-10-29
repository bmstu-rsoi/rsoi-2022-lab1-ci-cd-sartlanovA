using System;
using System.Text.Json.Serialization;

namespace rsoi_lr1.Dto
{
    public class PersonDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("work")]
        public string Job { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }

     
    }
}
