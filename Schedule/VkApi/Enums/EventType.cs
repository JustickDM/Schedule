using System.ComponentModel;

namespace Schedule.VkApi.Enums
{
	internal enum EventType
	{
		[Description("confirmation")]
		Confirmation,

		[Description("message_new")]
		MessageNew,
	}
}
