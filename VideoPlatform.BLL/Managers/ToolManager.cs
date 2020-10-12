using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers
{
    public class ToolManager : IToolManager
    {
        private readonly IToolsRepository _toolsRepository;

        public ToolManager(IToolsRepository toolsRepository)
        {
            _toolsRepository = toolsRepository ?? throw new ArgumentNullException(nameof(toolsRepository));
        }
    }
}