using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Schedule.Models.JsonObject
{
    /// <summary>
	/// Представляет структуру события
	/// </summary>
    public class RootObject
    {
        /// <summary>
		/// Тип события
		/// </summary>
		[JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Объект, инициировавший событие
        /// </summary>
        [JsonProperty("object")]
        public JObject Object { get; set; }

        /// <summary>
        /// ID сообщества, в котором произошло событие
        /// </summary>
        [JsonProperty("group_id")]
        public int GroupId { get; set; }
    }
}