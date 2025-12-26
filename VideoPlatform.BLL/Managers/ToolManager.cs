using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class ToolManager(IToolsRepository toolsRepository) : IToolManager
{
    private readonly IToolsRepository _toolsRepository =
        toolsRepository ?? throw new ArgumentNullException(nameof(toolsRepository));
}