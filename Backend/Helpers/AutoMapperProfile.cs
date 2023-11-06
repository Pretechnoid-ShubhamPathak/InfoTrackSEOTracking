namespace Backend.Helpers;

using AutoMapper;
using Backend.Entities;
using Backend.Models.SearchHistory;
using Backend.Models.Users;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region SearchHistory
        CreateMap<SearchHistoryRequestModel, SearchHistoryEntry>();

        CreateMap<SearchHistoryResponseModel, SearchHistoryEntry>();
        
        #endregion
        #region User Mapping
        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
        #endregion
    }
}