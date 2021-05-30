using Newtonsoft.Json;

namespace VideoPlatform.Api.Models.ResponseModels
{
    /// <summary>
    /// UserRoleModel
    /// </summary>
    public class UserRoleModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>
        [JsonProperty("roleName")]
        public string RoleName { get; set; }
    }
}