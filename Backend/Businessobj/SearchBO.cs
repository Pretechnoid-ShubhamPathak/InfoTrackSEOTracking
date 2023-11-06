namespace Backend.Businessobj;

using Backend.Helpers;
using Backend.Models.SearchHistory;
using Backend.Models.SEO;
using Backend.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Backend.Entities.StringEnum;

public interface ISearchBO
{
    Task<List<SearchResponseModel>> Search(List<SearchRequestModel> models);
    Task<string> GoogleSearch(string keywords, string url);
    Task<string> BingSearch(string keywords, string url);
}
public class SearchBO : ISearchBO{
    private readonly ISearchHistoryManager _searchHistoryManager;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public SearchBO(ISearchHistoryManager searchHistoryManager, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _searchHistoryManager = searchHistoryManager;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<SearchResponseModel>> Search(List<SearchRequestModel> models)
    {
        List<SearchResponseModel> lstSearchResponse = new List<SearchResponseModel>();
        Console.WriteLine(" model checking " + models.Count());
        foreach(SearchRequestModel model in models){
            SearchResponseModel responseModel = new SearchResponseModel();
            responseModel.Keywords = model.Keywords;
            responseModel.Url = model.Url;
            if(model.SearchEngine.ToLower() == SearchEngine.Google.ToLower()){
                responseModel.SearchEngine = SearchEngine.Google;
                responseModel.Ranking =  await GoogleSearch(model.Keywords,model.Url);
                lstSearchResponse.Add(responseModel);
            }else if(model.SearchEngine.ToLower() == SearchEngine.Bing.ToLower()){
                responseModel.SearchEngine = SearchEngine.Bing;
                responseModel.Ranking =  await BingSearch(model.Keywords,model.Url);
                lstSearchResponse.Add(responseModel);
            }
            else if(model.SearchEngine.ToLower() == SearchEngine.Yahoo.ToLower()){
                responseModel.SearchEngine = SearchEngine.Yahoo;
                responseModel.Ranking =  await YahooSearch(model.Keywords,model.Url);
                lstSearchResponse.Add(responseModel);
            }
            else{
                responseModel.Ranking = "0";
                responseModel.SearchEngine = "Not Accepted";
                lstSearchResponse.Add(responseModel);
            }
        }

        return lstSearchResponse;
    }

    public async Task<string> GoogleSearch(string keywords, string url)
    {
        SearchHistoryRequestModel shRequestModel = new SearchHistoryRequestModel();
        shRequestModel.Keywords = keywords;
        shRequestModel.Url = keywords;
        shRequestModel.SearchEngine = SearchEngine.Google;
        
        try
        {
            string googleUrl = $"http://www.google.co.uk/search?num=100&q={Uri.EscapeDataString(keywords)}";
            var client = _httpClientFactory.CreateClient();

            // Make a request to Google
            var response = await client.GetStringAsync(googleUrl);
            //Console.WriteLine(response);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            // string filePath = "result.html";
            // System.IO.File.WriteAllText(filePath, response);

            var searchResultLinks = htmlDocument.DocumentNode
                .SelectNodes("//div[@class='egMi0 kCrYT']//a[@href]");

            if (searchResultLinks != null)
            {   
                var ranks = new List<int>();
                Console.WriteLine(searchResultLinks.Count);
                
                int position = 1;
                foreach (var link in searchResultLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    //Console.WriteLine(href);
                    if (href.Contains(url))
                    {
                        Console.WriteLine($" IN Google URL found at position {position}");
                        ranks.Add(position);
                    }

                    position++;
                }

                if (ranks.Count > 0)
                {
                    shRequestModel.Ranking = string.Join(",", ranks);
                    bool istrue = _searchHistoryManager.AddSearchEntry(shRequestModel);
                    // URL found in the search results, return the list of ranks
                    return string.Join(",", ranks);
                }
                else
                {
                    // URL not found in the search results
                    return "URL not found";
                }
            }
            return "0";
        }
        catch (Exception ex)
        {
            throw new AppException($"Error: {ex.StackTrace}");
        }
    }

    public async Task<string> BingSearch(string keywords, string url)
    {
        SearchHistoryRequestModel shRequestModel = new SearchHistoryRequestModel();
        shRequestModel.Keywords = keywords;
        shRequestModel.Url = keywords;
        shRequestModel.SearchEngine = SearchEngine.Bing;

        try
        {
            string bingUrl = $"https://www.bing.com/search?num=100&q={Uri.EscapeDataString(keywords)}";
            var client = _httpClientFactory.CreateClient();

            // Make a request to Bing
            var response = await client.GetStringAsync(bingUrl);
            //Console.WriteLine(response);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            // string filePath = "result.html";

            // System.IO.File.WriteAllText(filePath, response);

            var searchResultLinks = htmlDocument.DocumentNode
                .SelectNodes("//div[@class='tpcn']//a[@href]");

            if (searchResultLinks != null)
            {   
                var ranks = new List<int>();
                Console.WriteLine(searchResultLinks.Count);
                int position1 = 1;
                foreach (var link in searchResultLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    //Console.WriteLine(href);
                    if (href.Contains(url))
                    {
                        Console.WriteLine($"IN Bing URL found at position {position1}");
                        ranks.Add(position1);
                    }

                    position1++;
                }

                if (ranks.Count > 0)
                {
                    if(ranks.Count == 1)
                    shRequestModel.Ranking = ranks.ToString();
                    else
                    shRequestModel.Ranking = string.Join(",", ranks);
                    _searchHistoryManager.AddSearchEntry(shRequestModel);
                    // URL found in the search results, return the list of ranks
                    return string.Join(",", ranks);
                }
                else
                {
                    // URL not found in the search results
                    return "URL not found";
                }
            }
            return "0";
        }
        catch (Exception ex)
        {
            throw new AppException($"Error: {ex.Message}");
        }
    }

    public async Task<string> YahooSearch(string keywords, string url)
    {
        SearchHistoryRequestModel shRequestModel = new SearchHistoryRequestModel();
        shRequestModel.Keywords = keywords;
        shRequestModel.Url = keywords;
        shRequestModel.SearchEngine = SearchEngine.Yahoo;

        try
        {
            string yahooUrl = $"https://search.yahoo.com/search?q={Uri.EscapeDataString(keywords)}";
            var client = _httpClientFactory.CreateClient();

            // Make a request to Yahoo
            var response = await client.GetStringAsync(yahooUrl);
            //Console.WriteLine(response);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(response);

            //string filePath = "result.html";

            //System.IO.File.WriteAllText(filePath, response);

            var searchResultLinks = htmlDocument.DocumentNode
                .SelectNodes("//h3[@class='title']//a[@href]");

            if (searchResultLinks != null)
            {   
                var ranks = new List<int>();
                Console.WriteLine(searchResultLinks.Count);
                int position1 = 1;
                foreach (var link in searchResultLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    //Console.WriteLine(href);
                    if (href.Contains(url))
                    {
                        Console.WriteLine($"IN Yahoo URL found at position {position1}");
                        ranks.Add(position1);
                    }

                    position1++;
                }

                if (ranks.Count > 0)
                {
                    if(ranks.Count == 1)
                    shRequestModel.Ranking = ranks.ToString();
                    else
                    shRequestModel.Ranking = string.Join(",", ranks);
                    _searchHistoryManager.AddSearchEntry(shRequestModel);
                    // URL found in the search results, return the list of ranks
                    return string.Join(",", ranks);
                }
                else
                {
                    // URL not found in the search results
                    return "URL not found";
                }
            }
            return "0";
        }
        catch (Exception ex)
        {
            throw new AppException($"Error: {ex.Message}");
        }
    }

}
