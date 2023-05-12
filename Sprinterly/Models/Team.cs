﻿using System.Text.Json.Serialization;

namespace Sprinterly.Models
{
    public class Team
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
