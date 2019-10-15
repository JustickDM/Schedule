using Newtonsoft.Json;

namespace Schedule.VkApi.JsonResult
{
	/// <summary>
	/// Ошибка
	/// </summary>
	public class ErrorJson
	{
		/// <summary>
		/// Код
		/// </summary>
		[JsonProperty("error_code")]
		public int Code { get; set; }

		/// <summary>
		/// Сообщение
		/// </summary>
		[JsonProperty("error_msg")]
		public string Message { get; set; }
	}
}