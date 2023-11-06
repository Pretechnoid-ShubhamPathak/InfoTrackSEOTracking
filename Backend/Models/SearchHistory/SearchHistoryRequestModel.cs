namespace Backend.Models.SearchHistory;

using System.ComponentModel.DataAnnotations;

public class SearchHistoryRequestModel
{
    [Required]
    public string Keywords { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string Ranking { get; set; }
    [Required]
    public string SearchEngine { get; set; }
    public DateTime SearchedAt { get; set; } = DateTime.UtcNow;
}