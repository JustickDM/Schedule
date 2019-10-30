using Newtonsoft.Json;

using RestSharp;

using System.Collections.Generic;
using System.Threading.Tasks;

using Schedule.VkApi.Enums;
using Schedule.VkApi.Extensions;
using Schedule.VkApi.JsonResult;

namespace Schedule.VkApi
{
	public class VkApiClient
	{
		private const string BASE_URL = "https://api.vk.com/method/";
		private const string V = "5.102";

		public async Task<RootObject<long>> MessageSendAsync(
			string accessToken,
			long randomId,
			int? userId = null,
			int? peerId = null,
			string domain = null,
			int? chatId = null,
			IEnumerable<int> userIds = null,
			string message = null,
			double? latitude = null,
			double? longtitude = null,
			string attachment = null,
			int? replyTo = null, 
			IEnumerable<int> forward_messages = null, 
			int? sticker_id = null,
			int? group_id = null,
			string keyboard = null,
			string payload = null, 
			bool dont_parse_links = false, 
			bool disable_mentions = false)
		{
			var client = new RestClient(BASE_URL);

			var request = new RestRequest(MethodType.MessagesSend.GetDescription(), Method.POST, DataFormat.Json);

			request.AddParameter(ParamType.Version.GetDescription(), V);
			request.AddParameter(ParamType.AccessToken.GetDescription(), accessToken);
			request.AddParameter(ParamType.UserId.GetDescription(), userId);
			request.AddParameter(ParamType.RandomId.GetDescription(), randomId);
			request.AddParameter(ParamType.PeerId.GetDescription(), peerId);
			request.AddParameter(ParamType.Domain.GetDescription(), domain);
			request.AddParameter(ParamType.ChatId.GetDescription(), chatId);
			request.AddParameter(ParamType.UserIds.GetDescription(), userIds);
			request.AddParameter(ParamType.Message.GetDescription(), message);
			request.AddParameter(ParamType.Keyboard.GetDescription(), keyboard);

			var rootObject = await Task.Run(() =>
			{
				var response = client.Post(request);

				return JsonConvert.DeserializeObject<RootObject<long>>(response.Content);
			});

			return rootObject;
		}
	}
}