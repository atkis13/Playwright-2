using System.Text.Json.Serialization;

public class FakeApiEntities
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("image")]
    public string Image { get; set; }
    [JsonPropertyName("category")]
    public string Category { get; set; }
}