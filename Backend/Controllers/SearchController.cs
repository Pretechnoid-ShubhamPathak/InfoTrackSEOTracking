namespace Backend.Controllers;

using Microsoft.AspNetCore.Mvc;
using Backend.Authorization;
using Backend.Helpers;
using Backend.Businessobj;
using Backend.Models.SEO;
using Backend.Services;
using Backend.Entities;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase   
{
    private ISearchBO _searchBO;
    private ISearchHistoryManager _searchHistoryManager;
    

    public SearchController(ISearchBO searchBO, ISearchHistoryManager searchHistoryManager){
        _searchBO = searchBO;
        _searchHistoryManager = searchHistoryManager;
    }

    [AllowAnonymous]
    [HttpPost("SEOTracking")]
    public async Task<ActionResult<List<SearchResponseModel>>> Search(List<SearchRequestModel> models)
    {
        if(models.Count > 0){
            return Ok(await _searchBO.Search(models));
        }else{
            return NotFound(new SearchResponseModel());
        }
    }

    [HttpGet("getAllTracking")]
    public ActionResult<IEnumerable<SearchHistoryEntry>> GetAll()
    {
        return Ok(_searchHistoryManager.GetAll());
    }
}