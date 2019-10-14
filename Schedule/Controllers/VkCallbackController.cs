using Newtonsoft.Json;

using System;
using System.IO;
using System.Net.Http;
using System.Web.Http;

using Schedule.Models.Entities;
using Schedule.Models.JsonObject;

namespace VkApiGroupStatisticServerMVC.Controllers
{
	public class VkCallbackController : ApiController
    {
        public HttpResponseMessage Post([FromBody]RootObject rootObject)
        {
            string response;

            switch (rootObject.Type)
            {
                case "confirmation":
                    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/VkApiSettings.json");
                    using (var sr = new StreamReader(path))
                    {
                        var fileContent = sr.ReadToEnd();
                        var settings = JsonConvert.DeserializeObject<VkApiSettings>(fileContent);
                        response = settings.ConfirmationString;
                    };

                    break;
                default:
                    using (var db = new DatabaseContext())
                    {
                        db.Events.Add(new Event()
                        {
                            Type = rootObject.Type,
                            GroupId = rootObject.GroupId,
                            Object = rootObject.Object?.ToString() ?? null,
                            Date = DateTime.Now.Ticks
                        });
                        db.SaveChanges();
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