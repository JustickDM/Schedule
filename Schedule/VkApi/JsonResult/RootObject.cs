using Newtonsoft.Json;

namespace Schedule.VkApi.JsonResult
{
	/// <summary>
	/// Представляет обертку для результата запроса
	/// </summary>
	public class RootObject<T>
	{
		/// <summary>
		/// Результат запроса
		/// </summary>
		[JsonProperty("response")]
		public T Response { get; set; }

		/// <summary>
		/// Ошибка
		/// </summary>
		[JsonProperty("error")]
		public ErrorJson Error { get; set; }

		/// <summary>
		/// Указывает, успешно ли выполнился запрос
		/// </summary>
		public bool IsSuccess => Error is null;
	}
}