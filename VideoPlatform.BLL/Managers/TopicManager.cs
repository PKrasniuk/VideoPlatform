using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class TopicManager : ITopicManager
    {
        private readonly ITopicsRepository _topicsRepository;

        public TopicManager(ITopicsRepository topicsRepository)
        {
            _topicsRepository = topicsRepository ?? throw new ArgumentNullException(nameof(topicsRepository));
        }
    }
}