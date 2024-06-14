using Diplom.Models.Authorization;
using Diplom.Models.Entity;
using Diplom.Services.Interfaces;
using Diplom.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Services.Implementations
{
    public class SearchService:ISearchService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        private readonly IBaseRepository<Tag> _tagRepository;
        private readonly ILogger<GameService> _logger;

        public SearchService(IBaseRepository<Game> gameRepository,IBaseRepository<Tag> tagRepository,ILogger<GameService> logger)
        {
            _tagRepository = tagRepository;
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<IEnumerable<Game>>> Search(string searchStr)
        {
            try
            {
                var response =  _gameRepository.GetAll().Where(x => x.Name.Contains(searchStr));

                if(response == null)
                {
                    return new BaseResponse<IEnumerable<Game>>()
                    {
                        StatusCode = StatusCode.GameNotFound,
                        Data = null
                    };
                }
                
                return new BaseResponse<IEnumerable<Game>>()
                {

                    StatusCode = StatusCode.OK,
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };

            }
        }

        public async Task<BaseResponse<IEnumerable<Game>>> Tag(string tag)
        {
            try
            {
                var response = await _tagRepository.GetAll().Include(x => x.Games).FirstOrDefaultAsync(x => x.TagId == tag);

                return new BaseResponse<IEnumerable<Game>>
                {
                    StatusCode = StatusCode.OK,
                    Data = response.Games
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }

    }
}
