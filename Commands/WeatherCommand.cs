using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace terrariacheat.Commands
{
	public class WeatherCommand : ModCommand
	{
		public override CommandType Type => CommandType.Chat;
		public override string Command => "weather";
		public override string Usage => "/weather <clear|rain|storm>";
		public override string Description => "Змінює погоду в грі";

		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if (args.Length == 0)
			{
				caller.Reply("Використання: /weather <clear|rain|storm>");
				return;
			}

			string weatherType = args[0].ToLower();

			switch (weatherType)
			{
				case "clear":
					Main.StopRain();
					Main.cloudAlpha = 0f;
					Main.maxRaining = 0f;
					Main.windSpeedCurrent = 0f;
					Main.windSpeedTarget = 0f;
					caller.Reply("Погоду змінено: Ясно ☀️");
					break;
				case "rain":
					Main.StartRain();
					Main.cloudAlpha = 0.6f;
					Main.maxRaining = 0.5f;
					Main.windSpeedTarget = 0.4f;
					caller.Reply("Погоду змінено: Дощ 🌧️");
					break;
				case "storm":
					Main.StartRain();
					Main.cloudAlpha = 0.9f;
					Main.maxRaining = 0.9f;
					Main.windSpeedTarget = 0.8f;
					caller.Reply("Погоду змінено: Гроза ⛈️ (Сильний вітер та максимальний дощ)");
					break;
				default:
					caller.Reply("Помилка: Допустимі варіанти: clear, rain, storm.");
					return;
			}

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}
}
