using Newtonsoft.Json;

namespace Schedule.Models.JsonObject
{
    public class VkApiSettings
    {
		/// <summary>
		/// Строка, которую нужно отправить для подтверждения сервера
		/// </summary>
        [JsonProperty("confirmation_string")]
        public string ConfirmationString { get; set; }

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
	}
}