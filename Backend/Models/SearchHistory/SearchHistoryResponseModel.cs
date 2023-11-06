namespace Backend.Models.SearchHistory;

using System.ComponentModel.DataAnnotations;

public class SearchHistoryResponseModel
{
    public int Id { get; set; }
    public string Keywords { get; set; }
    public string Url { get; set; }
    public string Ranking { get; set; }
    public string SearchEngine { get; set; }
    public DateTime SearchedAt { get; set; }
}