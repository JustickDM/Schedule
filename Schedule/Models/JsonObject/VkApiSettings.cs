using Newtonsoft.Json;

namespace Schedule.Models.JsonObject
{
    public class VkApiSettings
    {
        [JsonProperty("confirmationString")]
        public string ConfirmationString { get; set; }
    }
}