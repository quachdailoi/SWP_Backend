using Domain.Entities;

namespace API.Services.Constracts
{
    public interface ITopicService
    {
        Task<bool> AnyTopicWithCode(string code);
        Task<IEnumerable<Topic>> GetAllTopics();
        Task<Topic?> GetExistedTopicByCode(string code);
    }
}
