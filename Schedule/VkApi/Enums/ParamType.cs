using System.ComponentModel;

namespace Schedule.VkApi.Enums
{
	/// <summary>
	/// Тип параметра HTTP-запроса
	/// </summary>
	internal enum ParamType
	{
		/// <summary>
		/// Версия API ВКонтакте
		/// </summary>
		[Description("v")]
		Version,

		/// <summary>
		/// Ключ доступа
		/// </summary>
		[Description("access_token")]
		AccessToken,

		/// <summary>
		/// Идентификатор пользователя
		/// </summary>
		[Description("user_id")]
		UserId,

		/// <summary>
		/// Уникальный (в привязке к API_ID и ID отправителя) идентификатор, предназначенный для предотвращения повторной отправки одинакового сообщения. 
		/// Сохраняется вместе с сообщением и доступен в истории сообщений
		/// </summary>
		[Description("random_id")]
		RandomId,

		/// <summary>
		/// Идентификатор назначения
		/// </summary>
		[Description("peer_id")]
		PeerId,

		/// <summary>
		/// Короткий адрес пользователя (например, illarionov)
		/// </summary>
		[Description("domain")]
		Domain,

		/// <summary>
		/// Идентификатор беседы, к которой будет относиться сообщение
		/// </summary>
		[Description("chat_id")]
		ChatId,

		/// <summary>
		/// Идентификаторы получателей сообщения (при необходимости отправить сообщение сразу нескольким пользователям). 
		/// Доступно только для ключа доступа сообщества. Максимальное количество идентификаторов: 100
		/// </summary>
		[Description("user_ids")]
		UserIds,

		/// <summary>
		/// Текст личного сообщения. Обязательный параметр, если не задан параметр attachment
		/// </summary>
		[Description("message")]
		Message,
	}
}
