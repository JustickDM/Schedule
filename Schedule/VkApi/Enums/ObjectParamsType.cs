using System.ComponentModel;

namespace Schedule.VkApi.Enums
{
	internal enum ObjectParamsType
	{
		[Description("date")]
		Date,

		[Description("from_id")]
		FromId,

		[Description("id")]
		Id,

		[Description("out")]
		Out,

		[Description("peer_id")]
		PeerId,

		[Description("text")]
		Text,

		[Description("conversation_message_id")]
		ConversationMessageId,

		[Description("fwd_messages")]
		FwdMessages,

		[Description("important")]
		Important,

		[Description("random_id")]
		RandomId,

		[Description("attachments")]
		Attachments,

		[Description("is_hidden")]
		IsHidden,

		[Description("group_id")]
		GroupId,
	}
}
