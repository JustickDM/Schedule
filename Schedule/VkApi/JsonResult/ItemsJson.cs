using Newtonsoft.Json;

using System.Collections.Generic;

namespace Schedule.VkApi.JsonResult
{
	/// <summary>
	/// Объект, содержащий число результатов и массив возвращаемых элементов
	/// </summary>
	internal class ItemsJson<T>
	{
		/// <summary>
		/// Количество элементов
		/// </summary>
		[JsonProperty("count")]
		public int Count { get; set; }

		/// <summary>
		/// Элементы запроса
		/// </summary>
		[JsonProperty("items")]
		public List<T> Items { get; set; }
	}
}