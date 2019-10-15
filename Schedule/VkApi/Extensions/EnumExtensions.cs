using System;
using System.ComponentModel;
using System.Linq;

namespace Schedule.VkApi.Extensions
{
	internal static class EnumExtensions
	{
		/// <summary>
		/// Возвращает описание значения перечисления, записанного в атрибуте Description
		/// </summary>
		/// <param name="value">Значение перечисления</param>
		/// <returns>Описание значения перечисления</returns>
		public static string GetDescription(this Enum value)
		{
			var attribute = value
				.GetType()
				.GetField(value.ToString())
				.GetCustomAttributes(typeof(DescriptionAttribute), false)
				.SingleOrDefault() as DescriptionAttribute;
			return attribute?.Description ?? value.ToString();
		}
	}
}
