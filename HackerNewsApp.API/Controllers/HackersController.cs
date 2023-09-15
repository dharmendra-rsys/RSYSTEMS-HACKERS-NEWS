using HackersNewApp.DataAccess.Models;
using HackersNewApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HackerNewsApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HackersController : ControllerBase
    {
        private readonly IHackersRepository _hackersRepository;
        private readonly IMemoryCache _memoryCache;

        public HackersController(IHackersRepository hackersRepository,
            IMemoryCache memoryCache)
        {
            //Injecting dependencies
            _hackersRepository  = hackersRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Get newest stories async
        /// GET: /api/Hackers/GetNewestStories
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns>List of HackersStory</returns>
        [HttpGet]
        public async Task<IActionResult> GetNewestStoriesAsync(string? searchValue)
        {
            try
            {
                List<HackersStory> stories = new List<HackersStory>();

                var newStoryIds = await _hackersRepository.NewestStoryIdsAsync(searchValue);

                var topStoryIdsTasks = newStoryIds.Take(100)?.Select(GetStoryByIdAsync);
                if (topStoryIdsTasks != null)
                {
                    stories = (await Task.WhenAll(topStoryIdsTasks)).ToList();
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        searchValue = searchValue.ToLower();
                        stories = stories.Where(x => x.Title?.ToLower().Contains(searchValue) == true).ToList();
                    }
                }
                return Ok(stories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Return story details by ID
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns>Return HackersStory</returns>
        [HttpGet]
        public async Task<HackersStory> GetStoryByIdAsync(int storyId)
        {
            try
            {
                return await _memoryCache.GetOrCreateAsync(storyId,
                 async cacheEntry =>
                 {
                     return await _hackersRepository.GetStoryByIdAsyc(storyId);
                 });
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
