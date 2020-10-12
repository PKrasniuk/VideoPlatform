using System.Collections.Generic;

namespace VideoPlatform.ExternalService.Models
{
    /// <summary>
    /// ExternalServiceInfo
    /// </summary>
    public class ExternalServiceInfo
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<DefaultRequestHeader> DefaultRequestHeaders { get; set; }
    }
}