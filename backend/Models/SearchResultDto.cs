using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vinylify.Backend.Models
{
    public class SearchResultDto
    {
        [JsonPropertyName("pagination")]
        public PaginationDto Pagination { get; set; } = new();

        [JsonPropertyName("results")]
        public List<SearchReleaseDto> Results { get; set; } = new();
    }

    public class PaginationDto
    {
        [JsonPropertyName("page")]        public int Page        { get; set; }
        [JsonPropertyName("pages")]       public int Pages       { get; set; }
        [JsonPropertyName("per_page")]    public int PerPage     { get; set; }
        [JsonPropertyName("items")]       public int Items       { get; set; }
    }

    public class SearchReleaseDto
    {
        [JsonPropertyName("id")]          public int Id          { get; set; }
        [JsonPropertyName("title")]       public string Title    { get; set; } = default!;
        [JsonPropertyName("thumb")]       public string Thumb    { get; set; } = default!;
        [JsonPropertyName("year")]        public int Year        { get; set; }
        [JsonPropertyName("uri")]         public string Uri      { get; set; } = default!;
        [JsonPropertyName("type")]        public string Type     { get; set; } = default!;
        [JsonPropertyName("community")]   public CommunityDto Community { get; set; } = new();
    }

    public class CommunityDto
    {
        [JsonPropertyName("rating")] public RatingDto Rating { get; set; } = new();
    }
    public class RatingDto
    {
        [JsonPropertyName("average")] public double Average { get; set; }
    }
}
