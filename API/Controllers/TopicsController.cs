using API.Services.Constracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;

        public TopicsController(ITopicService topicService)
        {
            this.topicService = topicService;
        }
    }
}
