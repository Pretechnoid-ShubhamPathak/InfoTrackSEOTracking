namespace Backend.Models.SEO;

using System.ComponentModel.DataAnnotations;

public class SearchResponseModel
{
    [Required]
    public string Keywords { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string Ranking { get; set; }
    [Required]
    public string SearchEngine { get; set; }
}