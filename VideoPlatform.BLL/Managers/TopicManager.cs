using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class TopicManager(ITopicsRepository topicsRepository) : ITopicManager
{
    private readonly ITopicsRepository _topicsRepository =
        topicsRepository ?? throw new ArgumentNullException(nameof(topicsRepository));
}