using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Accounts.Login.Core.Models.Errors
{
    public class CustomProblemDetails
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("detail")]
        public string? Detail { get; set; }
        [JsonPropertyName("instance")]
        public string? Instance { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}