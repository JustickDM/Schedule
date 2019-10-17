using Newtonsoft.Json;

using Schedule.Models.Entities;
using Schedule.Models.JsonObject;
using Schedule.VkApi;
using Schedule.VkApi.Bot;
using Schedule.VkApi.Enums;
using Schedule.VkApi.Extensions;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;

namespace VkApiGroupStatisticServerMVC.Controllers
{
	public class VkCallbackController : ApiController
    {
		private string _vkApiSettingsPath = HostingEnvironment.MapPath("~/App_Data/VkApiSettings.json");
		private VkApiClient _vkApiClient = new VkApiClient();

        public async Task<HttpResponseMessage> Post([FromBody]RootObject rootObject)
        {
            string response;
			VkApiSettings vkApiSettings;

			using(var sr = new StreamReader(_vkApiSettingsPath))
			{
				var fileContent = sr.ReadToEnd();
				vkApiSettings = JsonConvert.DeserializeObject<VkApiSettings>(fileContent);
			};

			switch (rootObject.Type)
            {
                case "confirmation":
					response = vkApiSettings.ConfirmationString;
					break;
                default:
                    using (var db = new DatabaseContext())
                    {
                        db.Events.Add(new Event()
                        {
                            Type = rootObject.Type,
                            GroupId = rootObject.GroupId,
                            Object = rootObject.Object?.ToString() ?? null,
                            Date = rootObject.Object.ContainsKey(ObjectParamsType.FromId.GetDescription()) ? 
								   rootObject.Object.Value<long>(ObjectParamsType.FromId.GetDescription()) :
								   DateTime.Now.Ticks
                        });
                        db.SaveChanges();

						var rnd = new Random();
						var randomId = rnd.NextInt64();
						var userId = rootObject.Object.Value<int>(ObjectParamsType.FromId.GetDescription());
						var userText = rootObject.Object.ContainsKey(ObjectParamsType.Text.GetDescription()) ?
							           rootObject.Object.Value<string>(ObjectParamsType.Text.GetDescription()) : 
								       null;

						if(!string.IsNullOrWhiteSpace(userText))
						{
							var scheduleBot = new ScheduleBot(userId);
							var message = scheduleBot.Work(userText);
							
							var result = await _vkApiClient.MessageSendAsync(vkApiSettings.AccessToken, randomId, userId, message: message);
						}
                    }

                    response = "ok";
                    break;
            }

            return new HttpResponseMessage()
            {
                Content = new StringContent(response, System.Text.Encoding.ASCII)
            };
        }
	}
}