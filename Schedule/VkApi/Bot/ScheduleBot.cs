using HtmlAgilityPack;
using Schedule.Models.Entities;
using Schedule.VkApi.Enums;
using Schedule.VkApi.Extensions;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Schedule.VkApi.Bot
{
	public class ScheduleBot
	{
		private static Dictionary<string, BotCommandType> _commands = new Dictionary<string, BotCommandType>
		{
			{"пн", BotCommandType.Monday},
			{"вт", BotCommandType.Tuesday},
			{"ср", BotCommandType.Wednesday},
			{"чт", BotCommandType.Thursday},
			{"пт", BotCommandType.Friday},
			{"сб", BotCommandType.Saturday},
			{"вс", BotCommandType.Sunday},
			{"текущая" , BotCommandType.CurrentWeek}
		};
		private static int _userId;
		private static int _messageId;

		public ScheduleBot(int userId, int messageId)
		{
			_userId = userId;
			_messageId = messageId;
		}

		public string Work(string command)
		{
			command = command.ToLowerInvariant().Trim();

			var sb = new StringBuilder();
			var result = string.Empty;

			if(_messageId == 1)
			{
				result = Start();
			}
			else
			{
				User user;

				using(var db = new DatabaseContext())
				{
					user = db.Users.FirstOrDefault(u => u.UserId == _userId);
				}

				if(user != null)
				{
					var isContainsKey = _commands.ContainsKey(command);

					if(isContainsKey)
					{
						var commandType = _commands[command];

						var nodeCollection = GetNodes(user.Faculty, user.Course.ToString(), user.Group);
						var currentDataTable = ParseTable(nodeCollection);

						switch(commandType)
						{
							case BotCommandType.Monday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Tuesday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Wednesday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Thursday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Friday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Saturday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.Sunday:
								result = GetDayOfWeekSchedule(currentDataTable, commandType);
								break;
							case BotCommandType.CurrentWeek:
								result = GetCurrentWeekSchedule(currentDataTable, commandType);
								break;
						}
					}
				}
				else
				{
					if(command.Contains("регистрация"))
					{
						result = Registration(command);
					}
					else
					{
						result = $"Давай ближе к делу, я не люблю общаться:)";
					}
				}
			}

			sb.AppendLine(result);

			return sb.ToString();
		}

		private string Start()
		{
			var sb = new StringBuilder();

			sb.AppendLine($"Здравствуй, я бот для расписания нашего университета:)");
			sb.AppendLine($"Для дальнейшего взаимодействия требуется зарегистрироваться - напиши слово \"Регистрация\" (Без ковычек), а затем укажи название факультета, номер курса и название группы");
			sb.AppendLine($"Например: Регистрация fitu, 1, 42m");

			return sb.ToString();
		}

		private string Registration(string command)
		{
			var sb = new StringBuilder();
			var commandDescription = "Регистрация".ToLowerInvariant();
			var userInfo = command.Replace(commandDescription, string.Empty).Trim().Replace(" ", string.Empty).Split(',');

			if(userInfo.Length == 3)
			{
				var faculty = userInfo[0];
				var course = userInfo[1];
				var group = userInfo[2];

				var nodeCollection = GetNodes(faculty, course, group);

				if(nodeCollection != null && nodeCollection.Count > 0)
				{
					var user = new User()
					{
						UserId = _userId,
						Faculty = faculty,
						Course = int.Parse(course),
						Group = group,
					};

					using(var db = new DatabaseContext())
					{
						db.Users.Add(user);
						db.SaveChanges();
					}

					sb.AppendLine($"Регистрация прошла успешно, держи список активных команд:)");
					sb.AppendLine($"Получить расписание по дням недели: Пн, Вт, Ср, Чт, Пт, Сб, Вс, Текущая");
				}
				else
				{
					sb.AppendLine($"Не могу найти нужное расписание, проверь правильность данных:)");
					sb.AppendLine($"Хотя...Может опять сайт с расписанием прилег отдохнуть:(");
					sb.AppendLine($"Но ты все равно проверь данные, на всякий случай:)");
				}
			}
			else
			{
				sb.AppendLine($"Введи правильно все данные:)");
			}

			return sb.ToString();
		}

		private string GetDayOfWeekSchedule(DataTable dataTable, BotCommandType commandType)
		{
			var sb = new StringBuilder();
			var columnOfDay = (int)commandType;

			if(columnOfDay < (int)BotCommandType.Sunday)
			{
				var title = commandType.GetDescription();

				sb.AppendLine($"{title}");

				for(var j = 0; j < dataTable.Rows.Count; j++)
				{
					var time = dataTable.Rows[j][0].ToString().Insert(5, "-");
					var lesson = dataTable.Rows[j][columnOfDay].ToString();

					sb.AppendLine($"{time} {lesson}");
				}
			}
			else
			{
				sb.AppendLine($"{BotCommandType.Sunday.GetDescription()} - единственный день для отдыха:)");
			}

			return sb.ToString();
		}

		private string GetCurrentWeekSchedule(DataTable dataTable, BotCommandType commandType)
		{
			var sb = new StringBuilder();
			var title = commandType.GetDescription();

			sb.AppendLine($"{title}");

			for(var i = 1; i < dataTable.Columns.Count; i++)
			{
				var shortCaption = dataTable.Columns[i].Caption;

				sb.AppendLine($"{shortCaption}");

				for(var j = 0; j < dataTable.Rows.Count; j++)
				{
					var time = dataTable.Rows[j][0].ToString().Insert(5, "-");
					var lesson = dataTable.Rows[j][i].ToString();

					sb.AppendLine($"{time} {lesson}");
				}
			}

			return sb.ToString();
		}

		private HtmlNodeCollection GetNodes(string faculty, string course, string group)
		{
			var encoding = Encoding.GetEncoding("windows-1251");
			var web = new HtmlWeb() { OverrideEncoding = encoding };
			var htmlDocument = web.Load($"http://schedule.npi-tu.ru/schedule/{faculty}/{course}/{group}");
			var nodeCollection = htmlDocument.DocumentNode.SelectNodes("//table/tr");

			//var table = html.GetElementbyId("table_week_active");

			return nodeCollection;
		}

		private DataTable ParseTable(HtmlNodeCollection nodes)
		{
			var dataTable = new DataTable("dataTable");

			for(var i = 0; i < nodes.Count / 2; i++)
			{
				var node = nodes[i];
				var childNodes = node.ChildNodes;
				var dataRow = dataTable.NewRow();

				for(var j = 0; j < childNodes.Count; j++)
				{
					var htmlChildNode = childNodes[j];
					var text = htmlChildNode.InnerText.Trim().Replace("\n", string.Empty).Replace("\t", string.Empty);

					if(i == 0)
					{
						dataTable.Columns.Add(text);
					}
					else
					{
						dataRow[j] = text;
					}
				}

				dataTable.Rows.Add(dataRow);
			}

			return NormaliseTable(dataTable);
		}

		private DataTable NormaliseTable(DataTable dataTable)
		{
			var removeRow = dataTable.Rows[0];
			dataTable.Rows.Remove(removeRow);

			var removeColumn = dataTable.Columns[0];
			dataTable.Columns.Remove(removeColumn);

			return dataTable;
		}
	}
}