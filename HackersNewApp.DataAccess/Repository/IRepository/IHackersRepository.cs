using HackersNewApp.DataAccess.Models;

namespace HackersNewApp.DataAccess.Repository.IRepository
{
    public interface IHackersRepository
    {
        Task<List<int>> NewestStoryIdsAsync(string? searchValue);
        Task<HackersStory> GetStoryByIdAsyc(int storyId);
    }
}
