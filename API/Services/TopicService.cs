using API.Services.Constracts;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public async Task<bool> AnyTopicWithCode(string code)
        {
            return await GetExistedTopicByCode(code) == null;
        }

        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            return await topicRepository.List().ToListAsync();
        }

        public Task<Topic?> GetExistedTopicByCode(string code)
        {
            return topicRepository.GetTopicByCode(code);
        }
    }
}
