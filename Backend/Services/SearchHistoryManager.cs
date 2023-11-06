namespace Backend.Services;

using AutoMapper;
using Backend.Entities;
using Backend.Helpers;
using Backend.Models.SearchHistory;
using System.ComponentModel;

public interface ISearchHistoryManager
{
    
    bool AddSearchEntry(SearchHistoryRequestModel shmodel);
    bool AddSearchEntries(IEnumerable<SearchHistoryRequestModel> shmodels);
    IEnumerable<SearchHistoryEntry> GetAll();
    void Delete(int id);
}

public class SearchHistoryManager : ISearchHistoryManager
{
    private DataContext _context;
    private readonly IMapper _mapper;

    public SearchHistoryManager(
        DataContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<SearchHistoryEntry> GetAll()
    {
        return _context.SearchHistoryManager;
    }

    public bool AddSearchEntries(IEnumerable<SearchHistoryRequestModel> shmodels)
    {
        DateTime currentdateTime = DateTime.UtcNow;
        foreach(SearchHistoryRequestModel shmodel in shmodels){
            shmodel.SearchedAt = currentdateTime;
            // map model
            var shEntry = _mapper.Map<SearchHistoryEntry>(shmodel);
            _context.SearchHistoryManager.Add(shEntry);
        }
        _context.SaveChanges();
        return true;
    }

    public bool AddSearchEntry(SearchHistoryRequestModel shmodel)
    {
        shmodel.SearchedAt = DateTime.UtcNow;
        // map model
        var shEntry = _mapper.Map<SearchHistoryEntry>(shmodel);
        _context.SearchHistoryManager.Add(shEntry);
        _context.SaveChanges();
        return true;
    }    

    public void Delete(int id)
    {
        var shEntry = getSearchHistory(id);
        _context.SearchHistoryManager.Remove(shEntry);
        _context.SaveChanges();
    }

    #region  helper_methods
    private SearchHistoryEntry getSearchHistory(int id)
    {
        var shEntry = _context.SearchHistoryManager.Find(id);
        if (shEntry == null) throw new KeyNotFoundException("Search History not found");
        return shEntry;
    }

    #endregion
}