using System.ComponentModel;

namespace Schedule.VkApi.Enums
{
	internal enum BotCommandType
	{
		/// <summary>
		/// Понедельник
		/// </summary>
		[Description("Понедельник")]
		Monday = 1,

		/// <summary>
		/// Вторник
		/// </summary>
		[Description("Вторник")]
		Tuesday = 2,

		/// <summary>
		/// Среда
		/// </summary>
		[Description("Среда")]
		Wednesday = 3,

		/// <summary>
		/// Четверг
		/// </summary>
		[Description("Четверг")]
		Thursday = 4,

		/// <summary>
		/// Пятница
		/// </summary>
		[Description("Пятница")]
		Friday = 5,

		/// <summary>
		/// Суббота
		/// </summary>
		[Description("Суббота")]
		Saturday = 6,

		/// <summary>
		/// Воскресенье
		/// </summary>
		[Description("Воскресенье")]
		Sunday = 7,

		/// <summary>
		/// Текущая неделя
		/// </summary>
		[Description("Текущая неделя")]
		CurrentWeek = 8,

		/// <summary>
		/// Сегодня
		/// </summary>
		[Description("Сегодня")]
		Today = 9,

		/// <summary>
		/// Завтра
		/// </summary>
		[Description("Завтра")]
		Tomorrow = 10,

		/// <summary>
		/// Сейчас
		/// </summary>
		[Description("Сейчас")]
		Now,
	}
}
