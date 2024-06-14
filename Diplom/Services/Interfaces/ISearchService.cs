using Diplom.Models.Entity;
using Diplom.Services.Implementations;

namespace Diplom.Services.Interfaces
{
    public interface ISearchService
    {
        Task<BaseResponse<IEnumerable<Game>>> Search(string searchStr);
        Task<BaseResponse<IEnumerable<Game>>> Tag(string searchStr);
    }
}
