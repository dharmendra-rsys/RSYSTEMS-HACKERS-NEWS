using HackersNewApp.DataAccess.Common;
using HackersNewApp.DataAccess.Models;
using HackersNewApp.DataAccess.Repository.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackersNewApp.DataAccess.Repository
{
    public class HackersRepository : IHackersRepository
    {    
        public async Task<List<int>> NewestStoryIdsAsync(string? searchValue)
        {
            List<int> stories = new List<int>();
            using (HttpClient httpClient = new HttpClient())
            {
                var response=  await httpClient.GetAsync($"{Constants.HackersApiBaseUrl}/newstories.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        var resultList= JsonConvert.DeserializeObject<List<int>>(result);
                        if(resultList != null)
                        {
                            stories = resultList;
                        }
                    }  
                }
            }
            return stories;
        }

        public async Task<HackersStory> GetStoryByIdAsyc(int storyId)
        {
            HackersStory story = new HackersStory();
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{Constants.HackersApiBaseUrl}/item/{storyId}.json");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        var storyObj = JsonConvert.DeserializeObject<HackersStory>(result);
                        if (storyObj != null)
                            story = storyObj;
                    }
                }
            }
            return story;
        }
    }
}
