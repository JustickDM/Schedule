using System;

namespace Schedule.VkApi.Extensions
{
	internal static class RandomExnetsions
	{
		/// <summary>
		/// Возвращает сгенерированное число
		/// </summary>
		/// <param name="rnd">Рандом</param>
		/// <returns>Число</returns>
		public static long NextInt64(this Random rnd)
		{
			var buffer = new byte[sizeof(long)];
			rnd.NextBytes(buffer);
			return BitConverter.ToInt64(buffer, 0);
		}
	}
}