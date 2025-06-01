using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vinylify.Backend.Models
{
    public class DiscogsCollectionResponse
    {
        [JsonPropertyName("releases")]
        public List<ReleaseWrapper> Releases { get; set; } = new();
    }

    public class ReleaseWrapper
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("basic_information")]
        public BasicInformation BasicInformation { get; set; } = new();
    }

    public class BasicInformation
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("artists")]
        public List<Artist> Artists { get; set; } = new();

        [JsonPropertyName("thumb")]
        public string Thumb { get; set; } = string.Empty;
    }

    public class Artist
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
