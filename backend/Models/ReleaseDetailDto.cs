// Pfad: backend/Models/ReleaseDetailDto.cs

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vinylify.Backend.Models
{
    /// <summary>
    /// Detailinformationen zu einem Discogs-Release.
    /// </summary>
    public class ReleaseDetailDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("thumb")]
        public string Thumb { get; set; } = default!;

        [JsonPropertyName("artists")]
        public List<ReleaseArtistDto> Artists { get; set; } = new();

        [JsonPropertyName("labels")]
        public List<ReleaseLabelDto> Labels { get; set; } = new();

        [JsonPropertyName("formats")]
        public List<ReleaseFormatDto> Formats { get; set; } = new();

        [JsonPropertyName("tracklist")]
        public List<ReleaseTrackDto> Tracklist { get; set; } = new();
    }

    public class ReleaseArtistDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
    }

    public class ReleaseLabelDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
    }

    public class ReleaseFormatDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
    }

    public class ReleaseTrackDto
    {
        [JsonPropertyName("position")]
        public string Position { get; set; } = default!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("duration")]
        public string Duration { get; set; } = default!;
    }
}
